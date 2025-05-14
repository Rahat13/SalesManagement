using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Infrastructure.Persistence;

namespace SalesManagement.Application.Customers.Queries
{
    public record GetCustomerTotalPurchasesQuery(int CustomerId) : IRequest<decimal>;

    public class GetCustomerTotalPurchasesHandler : IRequestHandler<GetCustomerTotalPurchasesQuery, decimal>
    {
        private readonly AppDbContext _context;

        public GetCustomerTotalPurchasesHandler(AppDbContext context) => _context = context;

        public async Task<decimal> Handle(GetCustomerTotalPurchasesQuery request, CancellationToken ct)
        {
            return await _context.Sales
                .Where(s => s.CustomerId == request.CustomerId)
                .SumAsync(s => s.TotalPrice, ct);
        }
    }

}
