using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{

    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list", Name = "GetProductsList_Action")]
        public async Task<IActionResult> ListProducts([ModelBinder(typeof(LinqModelBinder))] ListProductsQueryCommand command)
        {
            var products = await _mediator.Send(command);
            return Ok(products);
        }
    }
}
