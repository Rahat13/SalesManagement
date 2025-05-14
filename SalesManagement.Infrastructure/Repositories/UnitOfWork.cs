using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Persistence;

namespace SalesManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Customer> Customers { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Sale> Sales { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new GenericRepository<Customer>(_context);
            Products = new GenericRepository<Product>(_context);
            Sales = new GenericRepository<Sale>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    }
}
