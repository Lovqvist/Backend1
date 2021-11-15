using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1_2.Model
{
    public class SignUp
    {
        [Required]
        [StringLength(255, ErrorMessage = "Must be at least {2} caracters long", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be at least {2} caracters long", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "Must be a valid and a complexed password")]
        public string Password { get; set; }
    }
}
