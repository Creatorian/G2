using Gnome.Application.G2.Query.AddProduct;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.G2.Query.UpdateProduct;
using Gnome.Application.G2.Query.DeleteProduct;
using Gnome.Application.G2.Query.GetProduct;
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

        [HttpGet("{id}", Name = "GetProductById_Action")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var command = new GetProductByIdCommand { Id = id };
            var product = await _mediator.Send(command);
            
            if (product == null)
                return NotFound();
                
            return Ok(product);
        }

        [HttpPost("add", Name = "AddProduct_Action")]
        public async Task<IActionResult> AddProduct([ModelBinder(typeof(LinqModelBinder))] AddProductCommand command)
        {
            var newProductId = await _mediator.Send(command);
            return Ok(new { Id = newProductId });
        }

        [HttpPut("update", Name = "UpdateProduct_Action")]
        public async Task<IActionResult> UpdateProduct([ModelBinder(typeof(LinqModelBinder))] UpdateProductCommand command)
        {
            var updatedProductId = await _mediator.Send(command);
            return Ok(new { Id = updatedProductId });
        }

        [HttpDelete("{id}", Name = "DeleteProduct_Action")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
                
            return Ok(new { Success = true });
        }
    }
}
