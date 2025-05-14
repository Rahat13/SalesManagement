using MediatR;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Persistence;

namespace SalesManagement.Application.Sales.Commands
{
    public record CreateSaleCommand(int CustomerId, int ProductId, int Quantity) : IRequest<int>;

    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateSaleHandler(AppDbContext context) => _context = context;

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken ct)
        {
            var product = await _context.Products.FindAsync(new object[] { request.ProductId }, ct);
            if (product == null) throw new Exception("Product not found");

            var sale = new Sale
            {
                CustomerId = request.CustomerId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TotalPrice = product.Price * request.Quantity,
                SaleDate = DateTime.UtcNow
            };

            await _context.Sales.AddAsync(sale, ct);
            await _context.SaveChangesAsync(ct);
            return sale.Id;
        }
    }
}
