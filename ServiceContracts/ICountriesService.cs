using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Business logic for manupulating Country entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a Country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>Returns the Country object after adding it</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// get all countries from the list
        /// </summary>
        /// <returns>All Countries from the list</returns>
        List<CountryResponse> GetAllCountries();


        /// <summary>
        /// Returns a country object
        /// </summary>
        /// <param name="countryID">CountryID (guid) to search</param>
        /// <returns>Matching country as CountryResponse object</returns>
        CountryResponse? GetCountryByCountryId(Guid? countryId);
    }
}
