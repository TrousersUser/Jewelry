
using System.ComponentModel.DataAnnotations;

namespace Jewelery.Models
{
    public class Orders
    {
        [Key]

        public int OrderID { get; set; }
        public string? OrderedBy { get; set; } 
        public string? OrderDescription { get; set; }
        
        public string? OrderStatus { get; set; }
        
    }
}
