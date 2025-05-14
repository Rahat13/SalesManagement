using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Infrastructure.Persistence;

namespace SalesManagement.Application.Sales.Queries
{
    public record GetSalesQuery(DateTime? FromDate, DateTime? ToDate, int? CustomerId, int? ProductId)
    : IRequest<List<SaleDto>>;

    public class SaleDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
    }

    public class GetSalesHandler : IRequestHandler<GetSalesQuery, List<SaleDto>>
    {
        private readonly AppDbContext _context;

        public GetSalesHandler(AppDbContext context) => _context = context;

        public async Task<List<SaleDto>> Handle(GetSalesQuery request, CancellationToken ct)
        {
            var query = _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .AsQueryable();

            if (request.FromDate.HasValue)
                query = query.Where(s => s.SaleDate >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(s => s.SaleDate <= request.ToDate.Value);

            if (request.CustomerId.HasValue)
                query = query.Where(s => s.CustomerId == request.CustomerId.Value);

            if (request.ProductId.HasValue)
                query = query.Where(s => s.ProductId == request.ProductId.Value);

            return await query.Select(s => new SaleDto
            {
                Id = s.Id,
                CustomerName = s.Customer.Name,
                ProductName = s.Product.Name,
                Quantity = s.Quantity,
                TotalPrice = s.TotalPrice,
                SaleDate = s.SaleDate
            }).ToListAsync(ct);
        }
    }
}
