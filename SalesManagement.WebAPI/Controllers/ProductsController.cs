using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Products.Queries;

namespace SalesManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }
    }
}
