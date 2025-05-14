using MediatR;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Repositories;

namespace SalesManagement.Application.Customers.Commands
{
    public record CreateCustomerCommand(string Name, string Email, string Phone) : IRequest<int>;

    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IUnitOfWork _uow;

        public CreateCustomerHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };
            await _uow.Customers.AddAsync(customer);
            await _uow.CompleteAsync();
            return customer.Id;
        }
    }

}
