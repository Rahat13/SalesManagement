using System.ComponentModel.DataAnnotations;

namespace SalesManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
