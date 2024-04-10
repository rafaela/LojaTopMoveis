using Loja.Model;
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
                sale.DateDelivery = null;
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

        public async Task<ServiceResponse<VendasResponse>> GetByID(Guid id)
        {
            ServiceResponse<VendasResponse> serviceResponse = new ServiceResponse<VendasResponse>();
            try {
                var l = await _context.Sales.Include(a => a.ProductsSale).Include(a => a.Client).Include(a => a.Address).AsQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();

                VendasResponse vr = new VendasResponse();
                vr.Id = l.Id;
                vr.Name = l.Name;
                vr.DateSale = l.DateSale.ToString("dd/MM/yyyy");
                vr.PaymentMethod = Enumeradores.GetDescription(l.PaymentMethod);
                vr.PaymentStatus = Enumeradores.GetDescription(l.PaymentStatus);
                vr.DateDelivery = l.DateDelivery != null ? l.DateDelivery.Value.ToString("dd/MM/yyyy") : "";
                vr.DeliveryStatus = Enumeradores.GetDescription(l.DeliveryStatus);
                vr.ValorTotal = l.ValorTotal;
                vr.Address = l.Address;

                vr.Products = new List<Product>();
                foreach (var p in l.ProductsSale)
                {
                    var prod = _context.Products.Include(a => a.Photos).Where(a => a.Id == p.ProductId).FirstOrDefault();
                    if (prod != null)
                    {
                        prod.Amount = p.Amount;
                    }
                    vr.Products.Add(prod);
                }

                serviceResponse.Data = vr;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<VendasResponse>>> Get(ServiceParameter<VendasResponse> sp)
        {
            ServiceResponse<List<VendasResponse>> serviceResponse = new ServiceResponse<List<VendasResponse>>();

            try
            {
                var query = _context.Sales.Include(a => a.ProductsSale).Include(a => a.Client).Include(a => a.Address).AsQueryable();
                if (sp.Data != null && sp.Data.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Contains(sp.Data.Name));
                }

                if (sp.Data != null && sp.Data.EPaymentStatus != null && sp.Data.EPaymentStatus != 0)
                {
                    query = query.Where(a => a.PaymentStatus == sp.Data.EPaymentStatus);
                }

                if (sp.Data != null && sp.Data.EDeliveryStatus != null && sp.Data.EDeliveryStatus != 0)
                {
                    query = query.Where(a => a.DeliveryStatus == sp.Data.EDeliveryStatus);
                }

                serviceResponse.Total = query.Count();

                query = query.OrderBy(a => a.CreationDate);

                if (sp.Take > 0)
                {
                    query = query.Skip(sp.Skip).Take(sp.Take);
                }
                var lista = await query.ToListAsync();
                List<VendasResponse> vendas = new List<VendasResponse>();
                foreach (var l in lista)
                {
                    VendasResponse vr = new VendasResponse();
                    vr.Id = l.Id;
                    vr.Name = l.Name;
                    vr.DateSale = l.DateSale.ToString("dd/MM/yyyy");
                    vr.PaymentMethod = Enumeradores.GetDescription(l.PaymentMethod);
                    vr.PaymentStatus = Enumeradores.GetDescription(l.PaymentStatus);
                    vr.DateDelivery = l.DateDelivery != null ? l.DateDelivery.Value.ToString("dd/MM/yyyy") : "";
                    vr.DeliveryStatus = Enumeradores.GetDescription(l.DeliveryStatus);
                    vr.ValorTotal = l.ValorTotal;

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

        public async Task<ServiceResponse<VendasResponse>> ChangeStatusPayment(Guid id)
        {
            ServiceResponse<VendasResponse> serviceResponse = new ServiceResponse<VendasResponse>();
            try
            {
                var sale = await _context.Sales.AsQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();

                if (sale != null)
                {
                    if (sale.PaymentStatus == PaymentStatus.Pending)
                    {
                        sale.PaymentStatus = PaymentStatus.Paid;
                    }

                    await _context.SaveChangesAsync();
                }

                VendasResponse venda = new VendasResponse();
                venda.PaymentStatus = Enumeradores.GetDescription(sale.PaymentStatus);
                serviceResponse.Sucess = true;
                serviceResponse.Data = venda;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

       

        public async Task<ServiceResponse<List<VendasResponse>>> GetDataSale(Guid id)
        {
            ServiceResponse<List<VendasResponse>> serviceResponse = new ServiceResponse<List<VendasResponse>>();
            try
            {
                var query = _context.Sales.Include(a => a.ProductsSale).Include(a => a.Client).Include(a => a.Address)
                        .Where(a => a.ClientId == id).AsQueryable().OrderByDescending(a => a.DateSale);

                var lista = await query.ToListAsync();
                List<VendasResponse> vendas = new List<VendasResponse>();
                foreach (var l in lista) {
                    VendasResponse vr = new VendasResponse();
                    vr.Id = l.Id; 
                    vr.Name = l.Name;
                    vr.DateSale = l.DateSale.ToString("dd/MM/yyyy");
                    vr.PaymentMethod = Enumeradores.GetDescription(l.PaymentMethod);
                    vr.PaymentStatus = Enumeradores.GetDescription(l.PaymentStatus);
                    vr.DateDelivery = l.DateDelivery != null ? l.DateDelivery.Value.ToString("dd/MM/yyyy") : "";
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

        public async Task<ServiceResponse<VendasResponse>> ChangeStatusDelivery(Guid id)
        {
            ServiceResponse<VendasResponse> serviceResponse = new ServiceResponse<VendasResponse>();
            try
            {
                var sale = await _context.Sales.AsQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();

                if(sale != null)
                {
                    if(sale.DeliveryStatus == DeliveryStatus.Pending)
                    {
                        sale.DeliveryStatus = DeliveryStatus.SeparateProducts;
                    }
                    else if (sale.DeliveryStatus == DeliveryStatus.SeparateProducts)
                    {
                        sale.DeliveryStatus = DeliveryStatus.OutForDelivery;
                    }
                    else if (sale.DeliveryStatus == DeliveryStatus.OutForDelivery)
                    {
                        sale.DeliveryStatus = DeliveryStatus.Delivered;
                        sale.DateDelivery = DateTime.Now;
                    }
                    else
                    {
                        sale.DeliveryStatus = DeliveryStatus.Returned;
                    }

                    await _context.SaveChangesAsync();
                }

                VendasResponse venda = new VendasResponse();
                venda.DeliveryStatus = Enumeradores.GetDescription(sale.DeliveryStatus);
                serviceResponse.Data = venda;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }


        public  async Task<ServiceResponse<VendasResponse>> CancelPayment(Guid id)
        {
            ServiceResponse<VendasResponse> serviceResponse = new ServiceResponse<VendasResponse>();
            try
            {
                var sale = await _context.Sales.AsQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();

                if (sale != null)
                {
                    sale.PaymentStatus = PaymentStatus.Canceled;
                    sale.DeliveryStatus = DeliveryStatus.Returned;

                    await _context.SaveChangesAsync();
                }

                VendasResponse venda = new VendasResponse();
                venda.PaymentStatus = Enumeradores.GetDescription(sale.PaymentStatus);
                serviceResponse.Sucess = true;
                serviceResponse.Data = venda;

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
