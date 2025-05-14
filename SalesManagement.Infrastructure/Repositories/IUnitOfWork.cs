using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Customers { get; }
        IRepository<Product> Products { get; }
        IRepository<Sale> Sales { get; }
        Task<int> CompleteAsync();
    }
}
