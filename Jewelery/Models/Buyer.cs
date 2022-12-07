using System;
using System.ComponentModel.DataAnnotations;

namespace Jewelery.Models
{
    public class Buyer
    {
        [Key]

        public int Buyer_ID { get; set; }
        public string? Buyer_Name { get; set; }
        public DateTime Buyer_DateOfRegistry { get; set; }
    }
}
