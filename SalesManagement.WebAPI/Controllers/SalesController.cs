using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Sales.Commands;
using SalesManagement.Application.Sales.Queries;

namespace SalesManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SalesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return Ok(new { saleId = id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate,
                                                [FromQuery] int? customerId, [FromQuery] int? productId)
        {
            var result = await _mediator.Send(new GetSalesQuery(fromDate, toDate, customerId, productId));
            return Ok(result);
        }
    }
}
