using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Person Domain model class
    /// </summary>
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        [Required(ErrorMessage ="Person name can't be blank")]
        public string? PersonName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

       
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

    }
}
