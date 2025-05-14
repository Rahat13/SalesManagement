using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Customers.Commands;
using SalesManagement.Application.Customers.Queries;

namespace SalesManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetAll), new { id }, cmd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();
            var success = await _mediator.Send(cmd);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteCustomerCommand(id));
            return success ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }

        [HttpGet("{id}/total-purchases")]
        public async Task<IActionResult> GetTotalPurchase(int id)
        {
            var total = await _mediator.Send(new GetCustomerTotalPurchasesQuery(id));
            return Ok(new { customerId = id, totalPurchase = total });
        }
    }

}
