using Gnome.Application.G2.Query.AddVariant;
using Gnome.Application.G2.Query.ListVariants;
using Gnome.Application.G2.Query.UpdateVariant;
using Gnome.Application.G2.Query.DeleteVariant;
using Gnome.Application.G2.Query.GetVariant;
using Gnome.Application.Shared;
using Gnome.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{
    [ApiController]
    [Route("variant")]
    public class VariantController : ControllerBase
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

        [HttpGet("{id}", Name = "GetVariantById_Action")]
        public async Task<IActionResult> GetVariantById(int id)
        {
            var command = new GetVariantByIdCommand { Id = id };
            var variant = await _mediator.Send(command);
            
            if (variant == null)
                return NotFound();
                
            return Ok(variant);
        }

        [HttpPost("add", Name = "AddVariant_Action")]
        public async Task<IActionResult> AddVariant([ModelBinder(typeof(LinqModelBinder))] AddVariantCommand command)
        {
            var newVariantId = await _mediator.Send(command);
            return Ok(new { Id = newVariantId });
        }

        [HttpPut("update", Name = "UpdateVariant_Action")]
        public async Task<IActionResult> UpdateVariant([ModelBinder(typeof(LinqModelBinder))] UpdateVariantCommand command)
        {
            var updatedVariantId = await _mediator.Send(command);
            return Ok(new { Id = updatedVariantId });
        }

        [HttpDelete("{id}", Name = "DeleteVariant_Action")]
        public async Task<IActionResult> DeleteVariant(int id)
        {
            var command = new DeleteVariantCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
                
            return Ok(new { Success = true });
        }
    }
}
