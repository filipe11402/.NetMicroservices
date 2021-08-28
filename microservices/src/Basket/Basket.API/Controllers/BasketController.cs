using Basket.Infrastructure.Abstract;
using Basket.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this._basketRepository = basketRepository;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBasket([FromQuery] string userName)
        {
            var userCart = await this._basketRepository.GetBasket(userName);

            return StatusCode(StatusCodes.Status200OK, userCart);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            var response = await this._basketRepository.UpdateCart(shoppingCart);

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteBasket([FromQuery] string userName) 
        {
            var response = await this._basketRepository.DeleteBasket(userName);

            if (!response) 
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
