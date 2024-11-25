﻿using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        //When CountryAddRequest is null -> ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //arrange
            CountryAddRequest? response = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //act
                _countriesService.AddCountry(response);

            });

        }

        //When the CountryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }


        //When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA" };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }


        //When you supply proper country name, it should insert (add) the country to the existing list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Japan" };

            //Act
            CountryResponse response = _countriesService.AddCountry(request);

            //Assert
            Assert.True(response.CountryId != Guid.Empty);
        }
        #endregion

        #region GetAllCountries

        [Fact]
        //The list of countries should be empty by default (before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();

            //Assert
            Assert.Empty(actual_country_response_list);
        }

        //[Fact]
        //public void GetAllCountries_AddFewCountries()
        //{
        //    //Arrange
        //    List<CountryAddRequest> country_request_list = new List<CountryAddRequest>() {
        //                                                   new CountryAddRequest() { CountryName = "USA" },
        //                                                   new CountryAddRequest() { CountryName = "UK" }
        //                                                   };

        //    //Act
        //    List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();

        //    foreach (CountryAddRequest country_request in country_request_list)
        //    {
        //        countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
        //    }

        //    List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

        //    //read each element from countries_list_from_add_country
        //    foreach (CountryResponse expected_country in countries_list_from_add_country)
        //    {
        //        Assert.Contains(expected_country, actualCountryResponseList);
        //    }
        //}
        #endregion

        #region GetCountryByCountryId
        //if CountryId is NULL, should return NULL as CountryResponse
        [Fact]
        public void GetCountryByCountryId_NullCountryId()
        {
            //arrange
            Guid? countryId = null;

            //act
            CountryResponse? countryResponseFromService = _countriesService.GetCountryByCountryId(countryId);

            //assert
            Assert.Null(countryResponseFromService);
        }

        //if supply valid CountryId, should return matching CountryDetails
        [Fact]
        public void GetCountryByCountryId_ValidCountryId()
        {
            //arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest() { CountryName = "Bangladesh" };
            CountryResponse? countryResponseFromAdd = _countriesService.AddCountry(countryAddRequest);

            //act
            CountryResponse? countryResponseFromGet = _countriesService.GetCountryByCountryId(countryResponseFromAdd.CountryId);

            //assert
            Assert.Equal(countryResponseFromAdd, countryResponseFromGet);

        }

        #endregion




    }
}
