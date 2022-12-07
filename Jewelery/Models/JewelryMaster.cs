
using System;
using System.ComponentModel.DataAnnotations;

namespace Jewelery.Models
{
    public class JewelryMaster
    {
        [Key]
        public int Master_ID { get; set; }
        public string? Master_Name { get; set; }
        public string? Master_Status { get; set; }
        public string? Master_IerarchPost { get; set; }
        public DateTime Master_DateOfRegistry { get; set; }
    }
}
