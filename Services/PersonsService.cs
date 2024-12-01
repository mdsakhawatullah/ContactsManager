using Entities;
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
            throw new NotImplementedException();
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
    }
}
