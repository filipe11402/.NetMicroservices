using Basket.Infrastructure.Entities;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Abstract
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateCart(ShoppingCart shoppingCart);
        Task<bool> DeleteBasket(string userName);
    }
}
