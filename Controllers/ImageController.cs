using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BrainerHubTask.Interface;
using BrainerHubTask.Model;
using Microsoft.AspNetCore.Authorization;

namespace BrainerHubTask.Controllers
{
    [Route("api/products/{productId}/images")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // GET: api/products/{productId}/images
        [HttpGet]
        public IActionResult GetImagesForProduct(int productId)
        {
            var images = _imageService.GetImagesForProduct(productId);
            return Ok(images);
        }

        // GET: api/products/{productId}/images/{imageId}
        [HttpGet("{imageId}")]
        public IActionResult GetImageById(int productId, int imageId)
        {
            var image = _imageService.GetImageById(imageId);

            if (image == null)
                return NotFound();

            return Ok(image);
        }

        // POST: api/products/{productId}/images
        [HttpPost]
        public IActionResult AddImageToProduct(int productId, [FromBody] Image image)
        {
            if (image == null)
                return BadRequest();

            try
            {
                var addedImage = _imageService.AddImageToProduct(productId, image);
                return CreatedAtAction("GetImageById", new { productId = productId, imageId = addedImage.ImageId }, addedImage);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/products/{productId}/images/{imageId}
        [HttpPut("{imageId}")]
        public IActionResult UpdateImage(int productId, int imageId, [FromBody] Image updatedImage)
        {
            if (updatedImage == null)
                return BadRequest();

            try
            {
                var image = _imageService.UpdateImage(imageId, updatedImage);
                return Ok(image);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/products/{productId}/images/{imageId}
        [HttpDelete("{imageId}")]
        public IActionResult DeleteImage(int productId, int imageId)
        {
            try
            {
                _imageService.DeleteImage(imageId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
