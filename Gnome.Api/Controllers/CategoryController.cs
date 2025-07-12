using Gnome.Application.G2.Query.ListCategories;
using Gnome.Application.G2.Query.AddCategory;
using Gnome.Application.G2.Query.UpdateCategory;
using Gnome.Application.G2.Query.DeleteCategory;
using Gnome.Application.G2.Query.GetCategory;
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
    /// Controller for managing board game categories
    /// </summary>
    [ApiController]
    [Route("category")]
    [SwaggerTag("Category management operations")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a paginated list of categories with filtering and sorting options
        /// </summary>
        /// <param name="command">Query parameters for filtering, pagination, and sorting</param>
        /// <returns>Paginated list of categories with metadata</returns>
        /// <response code="200">Successfully retrieved the category list.</response>
        /// <response code="400">Invalid query parameters.</response>
        [HttpGet("list", Name = "GetCategoriesList_Action")]
        [SwaggerOperation(Summary = "Get categories list", Description = "Retrieves a paginated list of categories with optional filtering by name, date range, and sorting options")]
        [SwaggerResponse(200, "Categories retrieved successfully", typeof(CategoryListResponse))]
        [SwaggerResponse(400, "Invalid query parameters")]
        public async Task<IActionResult> ListCategories([ModelBinder(typeof(LinqModelBinder))] ListCategoriesQueryCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Retrieves a specific category by its slug
        /// </summary>
        /// <param name="slug">The unique slug identifier of the category</param>
        /// <returns>Category details including products count</returns>
        /// <response code="200">Category found and returned successfully.</response>
        /// <response code="404">Category not found.</response>
        [HttpGet("{slug}", Name = "GetCategoryBySlug_Action")]
        [SwaggerOperation(Summary = "Get category by slug", Description = "Retrieves detailed information about a specific category including its products count using its slug")]
        [SwaggerResponse(200, "Category found", typeof(CategoryResponse))]
        [SwaggerResponse(404, "Category not found")]
        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            try
            {
                var command = new GetCategoryBySlugCommand { Slug = slug };
                var category = await _mediator.Send(command);
                
                if (category == null)
                    return NotFound(new { error = "Category not found" });
                    
                return Ok(category);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Creates a new category (Admin only)
        /// </summary>
        /// <param name="command">Category creation data including name and slug</param>
        /// <returns>ID of the newly created category</returns>
        /// <response code="201">Category created successfully.</response>
        /// <response code="400">Invalid category data.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="409">Category with same slug or name already exists.</response>
        [HttpPost("add", Name = "AddCategory_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add new category", Description = "Creates a new board game category. Requires admin authentication.")]
        [SwaggerResponse(201, "Category created successfully", typeof(CategoryCreatedResponse))]
        [SwaggerResponse(400, "Invalid category data")]
        [SwaggerResponse(401, "Unauthorized - Admin access required")]
        [SwaggerResponse(409, "Category with same slug or name already exists")]
        public async Task<IActionResult> AddCategory([ModelBinder(typeof(LinqModelBinder))] AddCategoryCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return StatusCode(201, new { id = result });
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Updates an existing category (Admin only)
        /// </summary>
        /// <param name="command">Category update data including ID and modified fields</param>
        /// <returns>ID of the updated category</returns>
        /// <response code="200">Category updated successfully.</response>
        /// <response code="400">Invalid category data.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Category not found.</response>
        /// <response code="409">Category with same slug or name already exists.</response>
        [HttpPut("update", Name = "UpdateCategory_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update category", Description = "Updates an existing board game category. Requires admin authentication.")]
        [SwaggerResponse(200, "Category updated successfully", typeof(CategoryUpdatedResponse))]
        [SwaggerResponse(400, "Invalid category data")]
        [SwaggerResponse(401, "Unauthorized - Admin access required")]
        [SwaggerResponse(404, "Category not found")]
        [SwaggerResponse(409, "Category with same slug or name already exists")]
        public async Task<IActionResult> UpdateCategory([ModelBinder(typeof(LinqModelBinder))] UpdateCategoryCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { id = result });
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Deletes a category by ID (Admin only)
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete</param>
        /// <returns>Confirmation of successful deletion</returns>
        /// <response code="200">Category deleted successfully.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Category not found.</response>
        [HttpDelete("{id}", Name = "DeleteCategory_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete category", Description = "Permanently deletes a board game category. Requires admin authentication.")]
        [SwaggerResponse(200, "Category deleted successfully", typeof(CategoryDeletedResponse))]
        [SwaggerResponse(401, "Unauthorized - Admin access required")]
        [SwaggerResponse(404, "Category not found")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var command = new DeleteCategoryCommand { Id = id };
                var result = await _mediator.Send(command);
                
                if (!result)
                    return NotFound(new { error = "Category not found" });
                    
                return NoContent();
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }
    }
}
