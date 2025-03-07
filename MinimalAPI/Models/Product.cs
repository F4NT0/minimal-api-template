using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class Product 
    {
        public Product() { }

        [Key]
        public long Id { get; set; }
        
        [StringLength(50)]
        [Required]
        public required string Name { get; set; }

        [Range(0, double.MaxValue)]
        [Required]
        public double Price { get; set; }
    }
}
