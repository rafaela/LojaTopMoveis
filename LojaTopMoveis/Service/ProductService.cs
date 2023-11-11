﻿using Loja.Model;
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
                _context.Add(product);
                await _context.SaveChangesAsync();
                serviceResponse.Data = null;
                serviceResponse.Message = "Produto cadastrado";
                serviceResponse.Sucess = true;

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
                    serviceResponse.Message = "Produto atualizado";
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
    }
}
