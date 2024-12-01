using ServiceContracts.DTO;
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
        
    }
}
