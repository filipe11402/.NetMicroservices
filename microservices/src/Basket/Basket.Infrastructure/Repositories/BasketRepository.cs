using Basket.Infrastructure.Abstract;
using Basket.Infrastructure.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this._redisCache = redisCache;
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            await this._redisCache.RemoveAsync(userName);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var userCart = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(userCart)) 
            {
                var newCart = new ShoppingCart(userName);

                return newCart;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(userCart);
        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                return null;
            }

            await this._redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));

            return await GetBasket(shoppingCart.UserName);
        }
    }
}
