using ProductgRpcService.Entities;
using System;

namespace ProductgRpcService.Repositories
{
    public interface IProductOfferService
    {
        public Task<List<Producto>> GetProductListAsync();
        public Task<Producto> GetProductByIdAsync(int Id);
        public Task<Producto> AddProductAsync(Producto producto);
        public Task<Producto> UpdateProductAsync(Producto producto);
        public Task<bool> DeleteProductAsync(int Id);
    }
}
