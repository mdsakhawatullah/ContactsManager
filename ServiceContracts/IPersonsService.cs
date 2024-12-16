using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// represesnt Business Logic for manupulating Person Entity
    /// </summary>
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a new Person object in the list of Person List 
        /// </summary>
        /// <param name="personAddRequest">Person entity to add</param>
        /// <returns>Returns PersonResponse type</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// return All person
        /// </summary>
        /// <returns>Returns PersonResponses to get</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Returns Person object based on the given PersonId
        /// </summary>
        /// <param name="personId">personId to search</param>
        /// <returns>areturns matching Person object</returns>
        PersonResponse? GetPersonByPersonId(Guid? personId);

        /// <summary>
        /// return all matching objects
        /// </summary>
        /// <param name="searchBy">search field to search</param>
        /// <param name="searchString">search field to search</param>
        /// <returns>returns all matching objects based on search</returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);


        /// <summary>
        /// returns all sorted list
        /// </summary>
        /// <param name="allPersons">PersonResponse type</param>
        /// <param name="sortBy">perameter to be sorted</param>
        /// <param name="sortOrder">asc or desc order</param>
        /// <returns>returns PersonResponse sorted list based on perameter</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
        
    }
}
