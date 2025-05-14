using MediatR;
using SalesManagement.Infrastructure.Repositories;

namespace SalesManagement.Application.Customers.Commands
{
    public record UpdateCustomerCommand(int Id, string Name, string Email, string Phone) : IRequest<bool>;

    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public UpdateCustomerHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _uow.Customers.GetByIdAsync(request.Id);
            if (customer == null) return false;

            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Phone = request.Phone;

            _uow.Customers.Update(customer);
            await _uow.CompleteAsync();
            return true;
        }
    }

}
