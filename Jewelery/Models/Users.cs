using System;
using System.ComponentModel.DataAnnotations;

namespace Jewelery.Models
{
    public class Users
    {
        [Key]

        public int ID { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Permissions { get; set; }
        public bool? RegistryStatus { get; set; }
        public DateTime DateOfRegistry { get; set; }
        
    }
}
