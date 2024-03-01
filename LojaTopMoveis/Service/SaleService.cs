﻿using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Methods;
using LojaTopMoveis.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Topmoveis.Data;
using Topmoveis.Enums;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class SaleService : ISale
    {
        private readonly LojaContext _context;

        public SaleService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Sale>> Create(Sale sale)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();

            try
            {
                sale.Name = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim();
                sale.DateSale = DateTime.Now;
                sale.PaymentStatus = Topmoveis.Enums.PaymentStatus.Pending;
                sale.DeliveryStatus = Topmoveis.Enums.DeliveryStatus.Pending;
                foreach(var p in sale.ProductsSale)
                {
                    var prod = _context.Products.Where(a => a.Id == p.ProductId).FirstOrDefault();
                    prod.Amount -= p.Amount;
                }
                _context.Add(sale);

                _context.ProductsSales.AddRange(sale.ProductsSale);
                await _context.SaveChangesAsync();

                serviceResponse.Data = sale;
                serviceResponse.Message = "Venda cadastrada";
                serviceResponse.Sucess = true;


            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Sale>> Delete(Guid id)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();

            try
            {
                Sale? sale = await _context.Sales.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);


                _context.Sales.Remove(sale);

                await _context.SaveChangesAsync();
                serviceResponse.Message = "Categoria removida";
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Sale>> GetByID(Guid id)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();
            try
            {
                Sale? sale = await _context.Sales.Include(a => a.ProductsSale).FirstOrDefaultAsync(a => a.Id == id);

                if (sale == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = sale;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Sale>>> Get(ServiceParameter<Sale> sp)
        {
            ServiceResponse<List<Sale>> serviceResponse = new ServiceResponse<List<Sale>>();

            try
            {
                var query = _context.Sales.Include(a => a.ProductsSale).AsQueryable();
                if (sp.Data != null && sp.Data.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Contains(sp.Data.Name));
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

        public async Task<ServiceResponse<Sale>> Update(Sale sale)
        {
            ServiceResponse<Sale> serviceResponse = new ServiceResponse<Sale>();

            try
            {
                Sale? sale1 = await _context.Sales.AsNoTracking().FirstOrDefaultAsync(a => a.Id == sale.Id);



                if (sale1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Categoria não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    sale.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Sales.Update(sale);
                    await _context.SaveChangesAsync();

                    serviceResponse.Message = "Categoria atualizada";
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

        public Task<ServiceResponse<Sale>> ChangeStatusSale(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Sale>> ChangeStatusSale(Sale sale)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<VendasResponse>>> GetDataSale(Guid id)
        {
            ServiceResponse<List<VendasResponse>> serviceResponse = new ServiceResponse<List<VendasResponse>>();
            try
            {
                var query = _context.Sales.Include(a => a.ProductsSale).Include(a => a.Client)
                        .Where(a => a.ClientId == id).AsQueryable();

                var lista = await query.ToListAsync();
                List<VendasResponse> vendas = new List<VendasResponse>();
                foreach (var l in lista) {
                    VendasResponse vr = new VendasResponse();
                    vr.Id = l.Id; 
                    vr.Name = l.Name;
                    vr.DateSale = l.DateSale.ToString("dd/MM/yyyy");
                    vr.PaymentMethod = Enumeradores.GetDescription(l.PaymentMethod);
                    vr.PaymentStatus = Enumeradores.GetDescription(l.PaymentStatus);
                    vr.DateDelivery = l.DateDelivery.ToString("dd/MM/yyyy");
                    vr.DeliveryStatus = Enumeradores.GetDescription(l.DeliveryStatus);
                    vr.ValorTotal = l.ValorTotal;

                    vr.Products = new List<Product>();
                    foreach(var p in l.ProductsSale)
                    {
                        var prod = _context.Products.Include(a => a.Photos).Where(a => a.Id == p.ProductId).FirstOrDefault();
                        if (prod != null)
                        {
                            prod.Amount = p.Amount;
                        }
                        vr.Products.Add(prod);
                    }

                    vendas.Add(vr);
                }

                serviceResponse.Data = vendas;

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
