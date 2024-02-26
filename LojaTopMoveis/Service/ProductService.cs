using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class ProductService : IProduct
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
                    var subs = _context.SubcategoriesProducts.Where(a => a.ProductId == product.Id).ToList();
                    if(subs.Count > 0)
                    {
                        _context.SubcategoriesProducts.RemoveRange(subs);
                    }
                    await _context.SaveChangesAsync();

                    var photos = _context.Photos.Where(a => a.ProductId == product.Id).ToList();
                    if (photos.Count > 0)
                    {
                        _context.Photos.RemoveRange(photos);
                    }
                    await _context.SaveChangesAsync();
                    _context.Products.Remove(product);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Produto removido";
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

        public async Task<ServiceResponse<Product>> GetByID(Guid id)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();
            try
            {
                Product? product = await _context.Products.Include(a => a.Photos).Include(a => a.Category).Include(a => a.SubcategoriesProducts).FirstOrDefaultAsync(a => a.Id == id);

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

        public async Task<ServiceResponse<List<Product>>> Get(ServiceParameter<Product> sp)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                var query = _context.Products.Include(a => a.Category).Include(a => a.Photos)
                           .Include(a => a.SubcategoriesProducts).AsQueryable();

                if (sp.Data != null && sp.Data.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Contains(sp.Data.Name));
                }
                if (sp.Data != null && sp.Data.Inactive == true)
                {
                    query = query.Where(a => a.Inactive);
                }
                else
                {
                    query = query.Where(a => !a.Inactive);
                }
                if (sp.Data != null && sp.Data.FeaturedProduct == true)
                {
                    query = query.Where(a => a.FeaturedProduct);
                }

                serviceResponse.Total = query.Count();

                query = query.OrderBy(a => a.Name);

                if (sp.Take > 0)
                {
                    query = query.Skip(sp.Skip).Take(sp.Take);
                }
                serviceResponse.Data = await query.ToListAsync();
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

        public async Task<ServiceResponse<List<Product>>> GetByCategory(Guid id)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                var query = _context.Products.Include(a => a.Category).Include(a => a.Photos)
                           .Include(a => a.SubcategoriesProducts).Where(a => a.CategoryID == id).AsQueryable();

                serviceResponse.Data = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> GetBySubcategory(Guid id)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
            try
            {
                var subs = _context.SubcategoriesProducts.Where(a => a.SubcategoryId == id).AsQueryable();
    

                foreach(var sub in subs)
                {
                    var query = _context.Products.Include(a => a.Category).Include(a => a.Photos)
                           .Include(a => a.SubcategoriesProducts).Where(a => a.CategoryID == sub.ProductId).AsQueryable();
                    var itens = await query.ToListAsync();
                    serviceResponse.Data.AddRange(itens);
                }

                
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> GetFeatured()
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                var query = _context.Products.Include(a => a.Category).Include(a => a.Photos)
                           .Include(a => a.SubcategoriesProducts).Where(a => a.FeaturedProduct).AsQueryable();

                serviceResponse.Data = await query.ToListAsync();
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
