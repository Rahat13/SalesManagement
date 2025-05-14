using MediatR;
using SalesManagement.Infrastructure.Repositories;

namespace SalesManagement.Application.Customers.Commands
{
    public record DeleteCustomerCommand(int Id) : IRequest<bool>;

    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public DeleteCustomerHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _uow.Customers.GetByIdAsync(request.Id);
            if (customer == null) return false;

            _uow.Customers.Delete(customer);
            await _uow.CompleteAsync();
            return true;
        }
    }

}
