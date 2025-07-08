using Gnome.Application.G2.Query.AddProductImage;
using Gnome.Application.G2.Query.DeleteImage;
using Gnome.Domain.Interfaces;
using Gnome.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{
    /// <summary>
    /// Controller for managing product images
    /// </summary>
    [ApiController]
    [Route("image")]
    [SwaggerTag("Product image management operations")]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMediator _mediator;

        public ImageController(IImageRepository imageRepository, IMediator mediator)
        {
            _imageRepository = imageRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Adds images to an existing product (Admin only)
        /// </summary>
        /// <param name="command">Image upload data including product ID and image files</param>
        /// <returns>Confirmation of successful image upload</returns>
        /// <response code="200">Images uploaded successfully.</response>
        /// <response code="400">Invalid image data.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Product not found.</response>
        [HttpPost("add", Name = "AddImages_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add images to product", Description = "Uploads and adds images to an existing product. Requires admin authentication.")]
        public async Task<IActionResult> AddImages([ModelBinder(typeof(LinqModelBinder))] AddProductImageCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (!result)
                return BadRequest("Failed to upload images or product not found");
                
            return Ok(new { Success = true });
        }

        /// <summary>
        /// Sets a product image as the primary image (Admin only)
        /// </summary>
        /// <param name="imageId">The image ID to set as primary</param>
        /// <returns>Confirmation of successful operation</returns>
        /// <response code="200">Primary image set successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Image not found.</response>
        [HttpPut("set-primary/{imageId}", Name = "SetPrimaryImage_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Set primary image", Description = "Sets a product image as the primary image. Requires admin authentication.")]
        public async Task<IActionResult> SetPrimaryImage(int imageId)
        {
            var result = await _imageRepository.SetPrimaryImageAsync(imageId);
            
            if (!result)
                return NotFound();
                
            return Ok(new { Success = true });
        }

        /// <summary>
        /// Deletes a product image (Admin only)
        /// </summary>
        /// <param name="id">The image ID to delete</param>
        /// <returns>Confirmation of successful deletion</returns>
        /// <response code="200">Image deleted successfully.</response>
        /// <response code="401">Unauthorized - Admin access required.</response>
        /// <response code="404">Image not found.</response>
        [HttpDelete("{id}", Name = "DeleteImage_Action")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete product image", Description = "Permanently deletes a product image. Requires admin authentication.")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var command = new DeleteImageCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
                
            return Ok(new { Success = true });
        }
    }
} 