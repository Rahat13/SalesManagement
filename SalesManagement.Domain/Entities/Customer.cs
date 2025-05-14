using System.ComponentModel.DataAnnotations;

namespace SalesManagement.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
