using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class PaymentService : ILoja<Payment>
    {
        private readonly LojaContext _context;

        public PaymentService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Payment>> Create(Payment payment)
        {
            ServiceResponse<Payment> serviceResponse = new ServiceResponse<Payment>();

            try
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();


                serviceResponse.Data = null;
                serviceResponse.Message = "Forma de pagamento cadastrada";
                serviceResponse.Sucess = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Payment>> Delete(Guid id)
        {
            ServiceResponse<Payment> serviceResponse = new ServiceResponse<Payment>();

            try
            {
                Payment? payment = await _context.Payments.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();

                if (payment == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Forma de pagamento não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Payments.Remove(payment);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Forma de pagamento removida";
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

        public async Task<ServiceResponse<Payment>> GetByID(Guid id)
        {
            ServiceResponse<Payment> serviceResponse = new ServiceResponse<Payment>();
            try
            {
                Payment? payment = await _context.Payments.FirstOrDefaultAsync(a => a.Id == id);

                
                serviceResponse.Data = payment;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Payment>>> Get(ServiceParameter<Payment> sp)
        {
            ServiceResponse<List<Payment>> serviceResponse = new ServiceResponse<List<Payment>>();

            try
            {
                var query = _context.Payments.AsQueryable();
                if (sp.Data != null && sp.Data.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Contains(sp.Data.Name));
                }
                if (sp.Data != null && sp.Data.Inactive == true)
                {
                    query = query.Where(a => a.Inactive.HasValue && (bool)a.Inactive);
                }
                else
                {
                    query = query.Where(a => !a.Inactive.HasValue || !(bool)a.Inactive);
                }

                serviceResponse.Total = query.Count();

                query = query.OrderBy(a => a.Name);
                
                if(sp.Take > 0)
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

       
        public async Task<ServiceResponse<Payment>> Update(Payment payment)
        {
            ServiceResponse<Payment> serviceResponse = new ServiceResponse<Payment>();

            try
            {
                Payment? payment1 = await _context.Payments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == payment.Id);
                
                payment.ChangeDate = DateTime.Now.ToLocalTime();
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();
                    
                serviceResponse.Message = "Forma de pagamento atualizada";
                serviceResponse.Sucess = true;
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
