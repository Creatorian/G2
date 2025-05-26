using Gnome.Application.G2.Query.ListCategories;
using Gnome.Application.G2.Query.ListProducts;
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
    }
}
