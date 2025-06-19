using Gnome.Application.G2.Query.AddVariant;
using Gnome.Application.G2.Query.ListCategories;
using Gnome.Application.G2.Query.ListVariants;
using Gnome.Application.Shared;
using Gnome.Domain.Interfaces;
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
        private readonly ICloudinaryService _cloudinaryService;

        public VariantController(IMediator mediator, ICloudinaryService cloudinaryService)
        {
            _mediator = mediator;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet("list", Name = "GetVariantsList_Action")]
        public async Task<IActionResult> ListVariants([ModelBinder(typeof(LinqModelBinder))] ListVariantsQueryCommand command)
        {
            var variants = await _mediator.Send(command);
            return Ok(variants);
        }

        [HttpPost("add", Name = "AddVariant_Action")]
        public async Task<IActionResult> AddVariant([FromForm] AddVariantCommand command)
        {
            var newVariantId = await _mediator.Send(command);
            return Ok(new { Id = newVariantId });
        }
    }
}
