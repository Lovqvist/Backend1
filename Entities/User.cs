using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend1_2.Entities
{
    [Index(nameof(Email), Name = "UQ__Users__A9D10534DE8FD7BF", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
