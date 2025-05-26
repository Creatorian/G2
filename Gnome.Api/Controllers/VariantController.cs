using Gnome.Application.G2.Query.ListCategories;
using Gnome.Application.G2.Query.ListVariants;
using Gnome.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{
    [ApiController]
    [Route("variant")]
    public class VariantController : Controller
    {

        private readonly IMediator _mediator;

        public VariantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list", Name = "GetVariantsList_Action")]
        public async Task<IActionResult> ListVariants([ModelBinder(typeof(LinqModelBinder))] ListVariantsQueryCommand command)
        {
            var variants = await _mediator.Send(command);
            return Ok(variants);
        }
    }
}
