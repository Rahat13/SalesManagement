using MediatR;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Repositories;

namespace SalesManagement.Application.Customers.Queries
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<Customer>>;

    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllCustomersHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken ct)
        {
            return await _uow.Customers.GetAllAsync();
        }
    }
}
