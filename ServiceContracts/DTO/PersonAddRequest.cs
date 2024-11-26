using Entities;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonAddRequest
    {
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts PersonAddRequest to Person object
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person() {
                      PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth,
                      Gender = Gender.ToString(), Address = Address, CountryID = CountryID,
                      ReceiveNewsLetters = ReceiveNewsLetters
                     };
        }
    }
}
