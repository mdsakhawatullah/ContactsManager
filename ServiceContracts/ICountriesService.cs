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
    }
}
