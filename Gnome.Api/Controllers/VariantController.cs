using Gnome.Application.G2.Query.AddVariant;
using Gnome.Application.G2.Query.ListVariants;
using Gnome.Application.G2.Query.UpdateVariant;
using Gnome.Application.G2.Query.DeleteVariant;
using Gnome.Application.G2.Query.GetVariant;
using Gnome.Application.Shared;
using Gnome.Domain.Interfaces;
using Gnome.Api.Models.SwaggerResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Gnome.Api.Controllers
{
    /// <summary>
    /// Controller for managing product variants
    /// </summary>
    [ApiController]
    [Route("variant")]
    [SwaggerTag("Variant management operations")]
    public class VariantController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<VariantController> _logger;

        public VariantController(IMediator mediator, ICloudinaryService cloudinaryService, ILogger<VariantController> logger)
        {
            _mediator = mediator;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a paginated list of variants with filtering and sorting options
        /// </summary>
        /// <param name="command">Query parameters for filtering, pagination, and sorting</param>
        /// <returns>Paginated list of variants with metadata</returns>
        /// <response code="200">Successfully retrieved the variant list.</response>
        /// <response code="400">Invalid query parameters.</response>
        [HttpGet("list", Name = "GetVariantsList_Action")]
        [SwaggerOperation(Summary = "Get variants list", Description = "Retrieves a paginated list of variants with optional filtering by name, date range, and sorting options")]
        [SwaggerResponse(200, "Variants retrieved successfully", typeof(VariantListResponse))]
        [SwaggerResponse(400, "Invalid query parameters")]
        public async Task<IActionResult> ListVariants([ModelBinder(typeof(LinqModelBinder))] ListVariantsQueryCommand command)
        {
            var variants = await _mediator.Send(command);
            return Ok(variants);
        }

        /// <summary>
        /// Retrieves a specific variant by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the variant</param>
        /// <returns>Variant details including product information</returns>
        /// <response code="200">Variant found and returned successfully.</response>
        /// <response code="404">Variant not found.</response>
        [HttpGet("{id}", Name = "GetVariantById_Action")]
        [SwaggerOperation(Summary = "Get variant by ID", Description = "Retrieves detailed information about a specific variant including its product details")]
        [SwaggerResponse(200, "Variant found", typeof(VariantResponse))]
        [SwaggerResponse(404, "Variant not found")]
        public async Task<IActionResult> GetVariantById(int id)
        {
            var command = new GetVariantByIdCommand { Id = id };
            var variant = await _mediator.Send(command);
            
            if (variant == null)
                return NotFound();
                
            return Ok(variant);
        }

        /// <summary>
        /// Creates a new variant with optional image upload (Admin only)
        /// </summary>
        /// <param name="command">Variant creation data including name, price, stock, and optional image</param>
        /// <returns>ID of the newly created variant</returns>
        /// <response code="200">Variant created successfully.</response>
        /// <response code="400">Invalid variant data.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        [HttpPost("add", Name = "AddVariant_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add new variant", Description = "Creates a new product variant with optional image upload to Cloudinary. Requires admin authentication.")]
        [SwaggerResponse(200, "Variant created successfully", typeof(VariantCreatedResponse))]
        [SwaggerResponse(400, "Invalid variant data")]
        [SwaggerResponse(401, "Unauthorized - Admin access required")]
        public async Task<IActionResult> AddVariant([ModelBinder(typeof(LinqModelBinder))] AddVariantCommand command)
        {
            if (command.Image != null)
            {
                _logger.LogInformation("File received in controller: {FileName}, Size: {FileSize}, ContentType: {ContentType}", 
                    command.Image.FileName, command.Image.Length, command.Image.ContentType);
            }
            else
            {
                _logger.LogWarning("No file received in controller - Image property is null");
                
                if (Request.HasFormContentType && Request.Form.Files.Count > 0)
                {
                    _logger.LogInformation("Request contains {FileCount} files:", Request.Form.Files.Count);
                    foreach (var file in Request.Form.Files)
                    {
                        _logger.LogInformation("  - {FileName}, Size: {FileSize}, ContentType: {ContentType}", 
                            file.Name, file.Length, file.ContentType);
                    }
                }
                else
                {
                    _logger.LogWarning("Request does not contain any files");
                }
            }

            var newVariantId = await _mediator.Send(command);
            return Ok(new { Id = newVariantId });
        }

        /// <summary>
        /// Updates an existing variant with optional image upload (Admin only)
        /// </summary>
        /// <param name="command">Variant update data including ID and modified fields</param>
        /// <returns>ID of the updated variant</returns>
        /// <response code="200">Variant updated successfully.</response>
        /// <response code="400">Invalid variant data.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Variant not found.</response>
        [HttpPut("update", Name = "UpdateVariant_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update variant", Description = "Updates an existing product variant with optional image upload. Requires admin authentication.")]
        [SwaggerResponse(200, "Variant updated successfully", typeof(VariantUpdatedResponse))]
        [SwaggerResponse(400, "Invalid variant data")]
        [SwaggerResponse(401, "Unauthorized - Admin access required")]
        [SwaggerResponse(404, "Variant not found")]
        public async Task<IActionResult> UpdateVariant([ModelBinder(typeof(LinqModelBinder))] UpdateVariantCommand command)
        {
            var updatedVariantId = await _mediator.Send(command);
            return Ok(new { Id = updatedVariantId });
        }

        /// <summary>
        /// Deletes a variant by ID (Admin only)
        /// </summary>
        /// <param name="id">The unique identifier of the variant to delete</param>
        /// <returns>Confirmation of successful deletion</returns>
        /// <response code="200">Variant deleted successfully.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Variant not found.</response>
        [HttpDelete("{id}", Name = "DeleteVariant_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete variant", Description = "Permanently deletes a product variant. Requires admin authentication.")]
        [SwaggerResponse(200, "Variant deleted successfully", typeof(VariantDeletedResponse))]
        [SwaggerResponse(401, "Unauthorized - Admin access required")]
        [SwaggerResponse(404, "Variant not found")]
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
