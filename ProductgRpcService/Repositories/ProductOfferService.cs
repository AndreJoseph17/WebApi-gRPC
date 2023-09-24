using Microsoft.EntityFrameworkCore;
using ProductgRpcService.Data;
using ProductgRpcService.Entities;

namespace ProductgRpcService.Repositories
{
    public class ProductOfferService : IProductOfferService
    {
        private readonly DbContextClass _dbContext;

        public ProductOfferService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Producto>> GetProductListAsync()
        {
            try
            {
                var response = await _dbContext.Producto.ToListAsync();
                return response;
            }
            catch(Exception ex) { 
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public async Task<Producto> GetProductByIdAsync(int Id)
        {
            try
            {
                var response = await _dbContext.Producto.Where(x => x.Id == Id).FirstOrDefaultAsync();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public async Task<Producto> AddProductAsync(Producto producto)
        {
            try
            {
                var result = _dbContext.Producto.Add(producto);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            
        }

        public async Task<Producto> UpdateProductAsync(Producto producto)
        {
            try
            {
                var result = _dbContext.Producto.Update(producto);
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch ( Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            
        }
        public async Task<bool> DeleteProductAsync(int Id)
        {
            try
            {
                var filteredData = _dbContext.Producto.Where(x => x.Id == Id).FirstOrDefault();
                var result = _dbContext.Remove(filteredData);
                await _dbContext.SaveChangesAsync();
                return result != null ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
            
        }
    }
}
