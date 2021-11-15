using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1_2.Model
{
    public class GetUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        private string Password { get; set; }
    }
}
