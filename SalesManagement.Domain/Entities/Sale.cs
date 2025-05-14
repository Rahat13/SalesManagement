namespace SalesManagement.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
