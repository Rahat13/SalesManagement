using MediatR;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Repositories;

namespace SalesManagement.Application.Products.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<Product>>;

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllProductsHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken ct)
        {
            return await _uow.Products.GetAllAsync();
        }
    }
}
