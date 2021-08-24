using Catalog.Infrastructure.Entities;
using Catalog.Infrastructure.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        //this isnt the right wway, should use CQRS patter + mediatr to create more loosely coupled code
        private readonly IProductRepository _repository;

        public CatalogController(IProductRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [Route("products/list")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            var productList = await this._repository.GetProducts();

            if (productList == null) 
            {
                return StatusCode(StatusCodes.Status204NoContent, productList);
            }

            return StatusCode(StatusCodes.Status200OK, productList);
        }

        [HttpGet]
        [Route("products/list/category/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var productList = await this._repository.GetProductByCategory(category);

            if (productList == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, productList);
            }

            return StatusCode(StatusCodes.Status200OK, productList);
        }

        [HttpGet]
        [Route("products/list/name/{name}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var productList = await this._repository.GetProductByName(name);

            if (productList == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, productList);
            }

            return StatusCode(StatusCodes.Status200OK, productList);
        }

        [HttpGet]
        [Route("products/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts(string id)
        {
            var product = await this._repository.GetProduct(id);

            if (product == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpPost]
        [Route("products/create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var creationResponse = await this._repository.CreateProduct(product);

            if (!creationResponse)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status201Created, new { Id = product.Id, productName = product.Name});
        }

        [HttpPut]
        [Route("products/update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Product) ,StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var updateResponse = await this._repository.UpdateProduct(product);

            if (!updateResponse)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, product);
        }

        [HttpDelete]
        [Route("products/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromQuery] string id)
        {
            var deleteResponse = await this._repository.DeleteProduct(id);

            if (!deleteResponse)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
