using Gnome.Application.G2.Query.ListCategories;
using Gnome.Application.G2.Query.AddCategory;
using Gnome.Application.G2.Query.UpdateCategory;
using Gnome.Application.G2.Query.DeleteCategory;
using Gnome.Application.G2.Query.GetCategory;
using Gnome.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{

    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list", Name = "GetCategoriesList_Action")]
        public async Task<IActionResult> ListCategories([ModelBinder(typeof(LinqModelBinder))] ListCategoriesQueryCommand command)
        {
            var categories = await _mediator.Send(command);
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById_Action")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var command = new GetCategoryByIdCommand { Id = id };
            var category = await _mediator.Send(command);
            
            if (category == null)
                return NotFound();
                
            return Ok(category);
        }

        [HttpPost("add", Name = "AddCategory_Action")]
        public async Task<IActionResult> AddCategory([ModelBinder(typeof(LinqModelBinder))] AddCategoryCommand command)
        {
            var newCategoryId = await _mediator.Send(command);
            return Ok(new { Id = newCategoryId });
        }

        [HttpPut("update", Name = "UpdateCategory_Action")]
        public async Task<IActionResult> UpdateCategory([ModelBinder(typeof(LinqModelBinder))] UpdateCategoryCommand command)
        {
            var updatedCategoryId = await _mediator.Send(command);
            return Ok(new { Id = updatedCategoryId });
        }

        [HttpDelete("{id}", Name = "DeleteCategory_Action")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
                
            return Ok(new { Success = true });
        }
    }
}
