using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class ProductService : ILoja<Product>
    {
        private readonly LojaContext _context;

        public ProductService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> Create(Product product)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();

            try
            {
                var photosList = product.Photos;
                product.Photos = null;
                _context.Add(product);
                await _context.SaveChangesAsync();

                SubcategoryProductsService subcategoryService = new SubcategoryProductsService(_context);
                var cad = subcategoryService.Create(product.SubcategoriesProducts);

                if (!cad.Result)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Erro ao cadastrar/atualizar subcategoria do produto";
                    serviceResponse.Sucess = false;

                }


                PhotosService photoService = new PhotosService(_context);
                photosList?.ForEach(a => a.ProductId = product.Id);

                var cadastro = photoService.Create(photosList);
                if (!cadastro)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Erro ao cadastrar as imagens dos produtos";
                    serviceResponse.Sucess = false;
                }
                if(cadastro && cad.Result)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Produto cadastrado";
                    serviceResponse.Sucess = true;
                }
                
                

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> Delete(Guid id)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();

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
                    serviceResponse.Message = "Produto removido";
                    serviceResponse.Sucess = false;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> GetByID(Guid id)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();
            try
            {
                Product? product = await _context.Products.Include(a => a.Photos).Include(a => a.SubcategoriesProducts).FirstOrDefaultAsync(a => a.Id == id);

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

        public async Task<ServiceResponse<List<Product>>> Get()
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                serviceResponse.Data = await _context.Products.Include(a => a.Category).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Product>>> GetFilter(Product product)
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

        public async Task<ServiceResponse<Product>> Inactivate(Guid id)
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
                else
                {
                    product.Inactive = true;
                    product.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Produto inativado";
                    serviceResponse.Sucess = true;

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> Update(Product product)
        {
                ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();

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

                    SubcategoryProductsService subcategoryService = new SubcategoryProductsService(_context);
                    var cad = subcategoryService.Create(product.SubcategoriesProducts);

                    if (!cad.Result)
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Erro ao cadastrar/atualizar subcategoria do produto";
                        serviceResponse.Sucess = false;

                    }

                    PhotosService photoService = new PhotosService(_context);
                    product.Photos?.ForEach(a => a.ProductId = product.Id);

                    var cadastro = photoService.Create(product.Photos);
                    if (!cadastro)
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Erro ao cadastrar/atualizar as imagens dos produtos";
                        serviceResponse.Sucess = false;
                    }
                    if(cadastro && cad.Result)
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Produto atualizado";
                        serviceResponse.Sucess = true;
                    }
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
