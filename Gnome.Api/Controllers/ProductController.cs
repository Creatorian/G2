using Gnome.Application.G2.Query.AddProduct;
using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.G2.Query.UpdateProduct;
using Gnome.Application.G2.Query.DeleteProduct;
using Gnome.Application.G2.Query.GetProduct;
using Gnome.Application.Shared;
using Gnome.Api.Models.SwaggerResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{
    /// <summary>
    /// Controller for managing board game products
    /// </summary>
    [ApiController]
    [Route("product")]
    [SwaggerTag("Product management operations")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a paginated list of products with filtering and sorting options
        /// </summary>
        /// <param name="command">Query parameters for filtering, pagination, and sorting</param>
        /// <returns>Paginated list of products with metadata</returns>
        /// <response code="200">Successfully retrieved the product list.</response>
        /// <response code="400">Invalid query parameters.</response>
        [HttpGet("list", Name = "GetProductsList_Action")]
        [SwaggerOperation(Summary = "Get products list", Description = "Retrieves a paginated list of products with optional filtering and sorting")]
        public async Task<IActionResult> ListProducts([ModelBinder(typeof(LinqModelBinder))] ListProductsQueryCommand command)
        {
            var products = await _mediator.Send(command);
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a specific product by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <returns>Product details including categories and images</returns>
        /// <response code="200">Product found and returned successfully.</response>
        /// <response code="404">Product not found.</response>
        [HttpGet("{id}", Name = "GetProductById_Action")]
        [SwaggerOperation(Summary = "Get product by ID", Description = "Retrieves detailed information about a specific product")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var command = new GetProductByIdCommand { Id = id };
            var product = await _mediator.Send(command);
            
            if (product == null)
                return NotFound();
                
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product (Admin only)
        /// </summary>
        /// <param name="command">Product creation data including name, description, and category associations</param>
        /// <returns>ID of the newly created product</returns>
        /// <response code="200">Product created successfully.</response>
        /// <response code="400">Invalid product data.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        [HttpPost("add", Name = "AddProduct_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add new product", Description = "Creates a new product. Requires admin authentication.")]
        public async Task<IActionResult> AddProduct([ModelBinder(typeof(LinqModelBinder))] AddProductCommand command)
        {
            var newProductId = await _mediator.Send(command);
            return Ok(new { Id = newProductId });
        }

        /// <summary>
        /// Updates an existing product (Admin only)
        /// </summary>
        /// <param name="command">Product update data including ID and modified fields</param>
        /// <returns>ID of the updated product</returns>
        /// <response code="200">Product updated successfully.</response>
        /// <response code="400">Invalid product data.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Product not found.</response>
        [HttpPut("update", Name = "UpdateProduct_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update product", Description = "Updates an existing product. Requires admin authentication.")]
        public async Task<IActionResult> UpdateProduct([ModelBinder(typeof(LinqModelBinder))] UpdateProductCommand command)
        {
            var updatedProductId = await _mediator.Send(command);
            return Ok(new { Id = updatedProductId });
        }

        /// <summary>
        /// Deletes a product by ID (Admin only)
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <returns>Confirmation of successful deletion</returns>
        /// <response code="200">Product deleted successfully.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Product not found.</response>
        [HttpDelete("{id}", Name = "DeleteProduct_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete product", Description = "Permanently deletes a product. Requires admin authentication.")]
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
