﻿using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();

        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResonse = person.ToPersonResponse();
            personResonse.Country = _countriesService.GetCountryByCountryId(person.CountryID)?.CountryName;

            return personResonse;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
           if(personAddRequest == null)
           {
                throw new ArgumentNullException(nameof(personAddRequest));
           }

            //Model validation
            ValidationHelper.ModelValidation(personAddRequest);

            //Convert personAddRequest to Person
            Person person = personAddRequest.ToPerson();

            //generate new PersonId
            person.PersonID = Guid.NewGuid();

            //add Person to the List
            _persons.Add(person);


            return ConvertPersonToPersonResponse(person);

        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(p => p.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonId(Guid? personId)
        {
            if (personId == null)
                return null;
            Person? person = _persons.FirstOrDefault(p => p.PersonID == personId);

            if (person == null)
                return null;
            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> getAllPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = getAllPersons;

            if (string.IsNullOrEmpty(searchBy) || (string.IsNullOrEmpty(searchString)))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(Person.PersonName):
                    matchingPersons = getAllPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PersonName) ?
                    temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Email):
                    matchingPersons = getAllPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    matchingPersons = getAllPersons.Where(temp =>
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    matchingPersons = getAllPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.CountryID):
                    matchingPersons = getAllPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
                     matchingPersons = getAllPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: matchingPersons = getAllPersons;
                    break;

            }
            return matchingPersons;

        }
    }
}
