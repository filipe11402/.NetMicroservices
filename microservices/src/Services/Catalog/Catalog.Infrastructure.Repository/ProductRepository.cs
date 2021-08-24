using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Entities;
using Catalog.Infrastructure.Repository.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            await this._dbContext.Products.InsertOneAsync(product);

            return true;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var productToDelete = await this._dbContext.Products.FindOneAndDeleteAsync(p => p.Id == id);

            if (productToDelete == null)
            {
                return false;
            }

            return true;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await this._dbContext.Products.Find(p => p.Id == id)
                                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            return await this._dbContext.Products.Find(p => p.Category == category)
                                                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await this._dbContext.Products.Find(p => p.Name == name)
                                                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await this._dbContext.Products.Find(prop => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await this._dbContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id,
                                                                              replacement: product);

            if (updateResult.ModifiedCount == 0 && !updateResult.IsAcknowledged) 
            {
                return false;
            }

            return true;
        }
    }
}
