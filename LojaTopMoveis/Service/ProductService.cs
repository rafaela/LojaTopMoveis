using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class ProductService : IProductInterface
    {
        private readonly LojaContext _context;

        public ProductService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> CreateProducts(Product product)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Products.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> DeleteProduct(Guid id)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                Product? product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                if (product == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Produto não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Products.Remove(product);

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Products.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> GetProductByID(Guid id)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();
            try
            {
                Product? product = await _context.Products.FirstOrDefaultAsync(a => a.Id == id);

                if (product == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Produto não encontrado";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = product;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                serviceResponse.Data = await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> InactivateProduct(Guid id)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                Product? product = await _context.Products.FirstOrDefaultAsync(a => a.Id == id);

                if (product == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Produto não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    product.Inactive = true;
                    product.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Products.ToListAsync();
                }

                

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> UpdateProduct(Product product)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                Product? product1 = await _context.Products.AsNoTracking().FirstOrDefaultAsync(a => a.Id == product.Id);

                if (product1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Produto não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    product.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Products.Update(product);

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Products.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }
    }
}
