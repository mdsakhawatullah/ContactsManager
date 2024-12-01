using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using Services;

namespace Test
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;

        public PersonsServiceTest()
        {
            _personService = new PersonsService();
            _countriesService = new CountriesService();
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


    }
}
