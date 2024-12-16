using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using Services;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Test
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
           
            _countriesService = new CountriesService();
            _personService = new PersonsService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson
        // NULL value as PersonAddRequest, should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //arrange
            PersonAddRequest? personAddRequest = null;

            //assert
            Assert.Throws<ArgumentNullException>(() =>
            //act
            {
                _personService.AddPerson(personAddRequest);
            }); 
        }

        //When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }

        //when supply proper Person details, should add in the Person List
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { 
                PersonName = "Person name...", Email = "person@example.com",
                Address = "sample address", CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true };

            //act
            PersonResponse personAddFromAddPerson = _personService.AddPerson(personAddRequest);
            List<PersonResponse> personsList = _personService.GetAllPersons();

            //assert
            Assert.True(personAddFromAddPerson.PersonID != Guid.Empty);
            Assert.Contains(personAddFromAddPerson, personsList);

        }


        #endregion


        #region GetPersonByPersonId

        /// <summary>
        /// if supply NULL value , it should return NULL response
        /// </summary>
        [Fact]
        public void GetPersonByPersonId_NullPersonId()
        {
            //arrange
            Guid? personId = null;

            //act
            PersonResponse? personFromGet = _personService.GetPersonByPersonId(personId);

            //assert
            Assert.Null(personFromGet);

        }

        /// <summary>
        /// if supply proper PersnId, it should return Person details
        /// </summary>

        [Fact]
        public void GetPersonByPersonId_WithPersonId()
        {
            //arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Bangladesh" };
            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest person_request = new PersonAddRequest() { 
                PersonName = "person name...", Email = "email@sample.com", Address = "address", 
                CountryID = countryResponse.CountryId, DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male, ReceiveNewsLetters = false };

            PersonResponse person_response_from_add = _personService.AddPerson(person_request);
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonId(person_response_from_add.PersonID);


            //assert
            Assert.Equal(person_response_from_add, person_response_from_get);


        }
        #endregion


        #region GetAllPersons

        /// <summary>
        /// Initially, GetAllPersons() should return an empty list
        /// </summary>

        [Fact]
        public void GetAllPerosns_EmptyList()
        {
            //act
            List<PersonResponse> listFromGet = _personService.GetAllPersons();

            //assert
            Assert.Empty(listFromGet);

        }


        /// <summary>
        /// if we supply few persons, it should return Persons List
        /// </summary>
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { 
                PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male,
                Address = "address of smith", CountryID = country_response_1.CountryId,
                DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest() { 
                PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female,
                Address = "address of mary", CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest() {
                PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male,
                Address = "address of rahman", CountryID = country_response_2.CountryId,
                DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            _testOutputHelper.WriteLine("Expexted:");

            foreach(var item in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            //Act
            List<PersonResponse> persons_list_from_get = _personService.GetAllPersons();

            _testOutputHelper.WriteLine("Actual:");

            foreach(var item in persons_list_from_get)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }
        }
    }


    #endregion

    #region GetFilteredPersons


    #endregion

    #region GetSortedPersons

    #endregion

    #region UpdatePerson
    #endregion

    #region DeletePerson

    #endregion


}

