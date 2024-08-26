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

                var colorList = product.Colors;
                product.Colors = null;

                

                _context.Add(product);
                await _context.SaveChangesAsync();


                var categories = product.SubcategoriesProducts;
                product.SubcategoriesProducts = null;

                categories?.ForEach(a => a.ProductId = product.Id);
                if (categories.Count > 0)
                {
                    SubcategoryProductsService subcategoryService = new SubcategoryProductsService(_context);
                    var cad = await subcategoryService.Create(categories);
                    if (!cad)
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Erro ao cadastrar as subcategorias";
                        serviceResponse.Sucess = false;
                    }
                    else
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Produto cadastrado";
                        serviceResponse.Sucess = true;
                    }
                }
                PhotosService photoService = new PhotosService(_context);
                photosList?.ForEach(a => a.ProductId = product.Id);

                var cadastro = await photoService.Create(photosList);

                ColorService colorService = new ColorService(_context);
                colorList?.ForEach(a => a.ProductId = product.Id);

                var cadastroCor = await colorService.Create(colorList);
                if (!cadastro)
                {
                    _ = Delete(product.Id);
                    
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Erro ao cadastrar as imagens dos produtos";
                    serviceResponse.Sucess = false;
                }
                else if (!cadastroCor)
                {

                    _ = Delete(product.Id);
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Erro ao cadastrar as cores dos produtos";
                    serviceResponse.Sucess = false;
                }
                else
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
                Product? product = _context.Products.AsNoTracking().FirstOrDefault(a => a.Id == id);


                var vendas = _context.ProductsSales.Where(a => a.ProductId == id).ToList();

                if(vendas.Count > 0)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Produto não pode ser removido, tente torná-lo inativo";
                    serviceResponse.Sucess = false;
                    return serviceResponse;
                }

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
                    _context.SaveChanges();

                    var photos = _context.Photos.Where(a => a.ProductId == product.Id).ToList();
                    if (photos.Count > 0)
                    {
                        _context.Photos.RemoveRange(photos);
                    }
                    _context.SaveChanges();

                    var colors = _context.Colors.Where(a => a.ProductId == product.Id).ToList();
                    if (colors.Count > 0)
                    {
                        _context.Colors.RemoveRange(colors);
                    }
                    _context.SaveChanges();

                    _context.Products.Remove(product);

                    _context.SaveChanges();
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
                Product? product =  _context.Products.Include(a => a.Photos).Include(a => a.Category).
                    Include(a => a.SubcategoriesProducts).Include(a => a.Colors).FirstOrDefault(a => a.Id == id);

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
                var query = _context.Products.Include(a => a.Category).Include(a => a.Photos).AsQueryable();

                if (sp.Data != null && sp.Data.Name != null && sp.Data.Name != "")
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
                serviceResponse.Data = query.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAdmin(ServiceParameter<Product> sp)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                var query = _context.Products.Include(a => a.Category).AsQueryable();

                if (sp.Data != null && sp.Data.Name != null && sp.Data.Name != "")
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
                serviceResponse.Data = query.ToList();
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
                Product? product1 =  _context.Products.AsNoTracking().FirstOrDefault(a => a.Id == product.Id);

                if (product1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Produto não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    
                    if(product.SubcategoriesProducts.Count > 0)
                    {
                        SubcategoryProductsService subcategoryService = new SubcategoryProductsService(_context);
                        var cad = subcategoryService.Create(product.SubcategoriesProducts);

                        if (!cad.Result)
                        {
                            serviceResponse.Data = null;
                            serviceResponse.Message = "Erro ao cadastrar/atualizar subcategoria do produto";
                            serviceResponse.Sucess = false;

                        }
                        else
                        {
                            serviceResponse.Data = null;
                            serviceResponse.Message = "Produto atualizado";
                            serviceResponse.Sucess = true;
                        }
                    }
                    else
                    {
                        var subProducts = _context.SubcategoriesProducts.Where(a => a.ProductId == product.Id).ToList();
                        _context.SubcategoriesProducts.RemoveRange(subProducts);

                    }

                    _context.SaveChanges();


                    PhotosService photoService = new PhotosService(_context);
                    product.Photos?.ForEach(a => a.ProductId = product.Id);

                    var cadastro = photoService.Create(product.Photos);

                    ColorService colorService = new ColorService(_context);
                    product.Colors?.ForEach(a => a.ProductId = product.Id);

                    var cadastroColor = colorService.Create(product.Colors);

                    product.ChangeDate = DateTime.Now.ToLocalTime();

                    product1 = product;

                    product1.Photos = null;
                    product1.Colors = null;
                    _context.Products.Update(product1);

                    _context.SaveChanges();

                    if (!cadastro.Result)
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Erro ao cadastrar/atualizar as imagens do produto";
                        serviceResponse.Sucess = false;
                    }
                    else if (!cadastroColor.Result)
                    {
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Erro ao cadastrar/atualizar as cores do produto";
                        serviceResponse.Sucess = false;
                    }
                    else
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
                           .Include(a => a.SubcategoriesProducts).Include(a => a.Colors).Where(a => a.CategoryID == id 
                            && !a.Inactive).AsQueryable();

                serviceResponse.Data = query.ToList();
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
                var subs = _context.SubcategoriesProducts.Where(a => a.SubcategoryId == id).AsQueryable().ToList();


                serviceResponse.Data = new List<Product>();
                foreach(var sub in subs)
                {
                    var query = _context.Products.Include(a => a.Category).Include(a => a.Photos)
                           .Include(a => a.SubcategoriesProducts).Where(a => a.Id == sub.ProductId).AsQueryable();
                    var itens = query.ToList();
                    if(itens.Count > 0)
                    {
                        serviceResponse.Data.AddRange(itens);
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

        public async Task<ServiceResponse<List<Product>>> GetFeatured()
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            try
            {
                var query = _context.Products.Include(a => a.Category).Include(a => a.Photos)
                           .Include(a => a.SubcategoriesProducts).Where(a => a.FeaturedProduct).AsQueryable();

                serviceResponse.Data = query.ToList();
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
