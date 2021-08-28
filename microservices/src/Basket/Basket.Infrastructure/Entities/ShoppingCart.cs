using System.Collections.Generic;

namespace Basket.Infrastructure.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }

        public ShoppingCart()
        {
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in this.Items)
                {
                    totalPrice += item.Price;
                }

                return totalPrice;
            }
        }
    }
}
