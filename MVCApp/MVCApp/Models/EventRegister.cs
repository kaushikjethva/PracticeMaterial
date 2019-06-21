using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class EventRegister : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int Id { get; set; }

        public int EventId { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be empty")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Company cannot be empty")]        
        public string Company { get; set; }

        [Required(ErrorMessage = "Contact No. cannot be empty")]
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [Display(Name = "EmailId")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }               
        
    }
}
