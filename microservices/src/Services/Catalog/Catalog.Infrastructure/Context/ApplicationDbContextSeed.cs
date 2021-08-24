using Catalog.Infrastructure.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Context
{
    public class ApplicationDbContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection) 
        {
            bool exists = productCollection.Find(p => true).Any();

            if (!exists) 
            {
                productCollection.InsertManyAsync(GetStartingProducts());
            }


        }

        private static IEnumerable<Product> GetStartingProducts() 
        {
            return new List<Product>
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Iphone X",
                    Summary = "this is a summary",
                    Category = "Smart Phone",
                    Description = "this is iphone X description",
                    Price = 950.00M,
                    ImageFile = "product-1.png"
                }
            };
        }
    }
}
