using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BrainerHubTask.Interface;
using BrainerHubTask.Model;
using Microsoft.AspNetCore.Authorization;
using BrainerHubTask.Services;

namespace BrainerHubTask.Controllers
{     
    [Route("api/products")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;



        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetProducts(string searchKeyword = null, int page = 1, int pageSize = 10)
        {
            var products = _productService.GetProducts(searchKeyword, page, pageSize);
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        // POST: api/products
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            // Get the user's ID from the authentication token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest("Invalid or missing UserId claim in the token.");
            }

            // Check if the user exists
            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Associate the product with the user
            product.UserId = userId;

            var createdProduct = _productService.CreateProduct(product);

            return CreatedAtAction("GetProductById", new { id = createdProduct.ProductId }, createdProduct);
        }



        // PUT: api/products/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest();

            try
            {
                var product = _productService.UpdateProduct(id, updatedProduct);
                return Ok(product);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

       
        
    }
}
