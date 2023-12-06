using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class EmployeeService : ILoja<Employee>
    {
        private readonly LojaContext _context;

        public EmployeeService(LojaContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Employee>> Create(Employee employee)
        {
            ServiceResponse<Employee> serviceResponse = new ServiceResponse<Employee>();

            try
            {
                var user = _context.Employees.Where(a => a.Email == employee.Email).FirstOrDefault();
                if(user != null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "E-mail já cadastrado";
                    serviceResponse.Sucess = false;
                }
                else
                {

                    if (employee != null && employee?.BirthDate != null)
                    {
                        var dia = employee?.BirthDate?.Substring(0, 2);
                        var mes = employee?.BirthDate?.Substring(2, 2);
                        var ano = employee?.BirthDate?.Substring(4, 4);

                        var data = dia + "/" + mes + "/" + ano;

                        employee.BirthDate = data;
                    }

                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Funcionário cadastrado";
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

        public async Task<ServiceResponse<Employee>> Delete(Guid id)
        {
            ServiceResponse<Employee> serviceResponse = new ServiceResponse<Employee>();

            try
            {
                Employee? employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Funcionário não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Employees.Remove(employee);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Funcionário removido";
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

        public async Task<ServiceResponse<Employee>> GetByID(Guid id)
        {
            ServiceResponse<Employee> serviceResponse = new ServiceResponse<Employee>();
            try
            {
                Employee? Employee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id);

                if (Employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Funcionário não encontrado";
                    serviceResponse.Sucess = false;
                }

                serviceResponse.Data = Employee;

            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Employee>>> Get()
        {
            ServiceResponse<List<Employee>> serviceResponse = new ServiceResponse<List<Employee>>();

            try
            {
                serviceResponse.Data = await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Employee>>> GetFilter(Employee employee)
        {
            ServiceResponse<List<Employee>> serviceResponse = new ServiceResponse<List<Employee>>();

            /*try
            {
                var query = _context.Employees.AsQueryable();
                if (employee.Name != null)
                {
                    query = query.Where(a => a.Name != null && a.Name.Equals(Employee.Name));
                }
                if (Employee.Inactive == true)
                {
                    query = query.Where(a => !a.Inactive);
                }
                else
                {
                    query = query.Where(a => a.Inactive);
                }
                serviceResponse.Data = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }*/

            return serviceResponse;

        }

        public async Task<ServiceResponse<Employee>> Inactivate(Guid id)
        {
            ServiceResponse<Employee> serviceResponse = new ServiceResponse<Employee>();

            try
            {
                Employee? employee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Funcionário não encontrado";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    employee.Inactive = true;
                    employee.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Employees.Update(employee);
                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Funcionário inativado";
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

        public async Task<ServiceResponse<Employee>> Update(Employee employee)
        {
            ServiceResponse<Employee> serviceResponse = new ServiceResponse<Employee>();

            try
            {
                Employee? employee1 = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(a => a.Id == employee.Id);

                if(employee?.BirthDate != null)
                {
                    var dia = employee?.BirthDate?.Substring(0, 2);
                    var mes = employee?.BirthDate?.Substring(2, 2);
                    var ano = employee?.BirthDate?.Substring(4, 4);

                    var data = dia + "/" + mes + "/" + ano;

                    employee.BirthDate = data;
                }

                

                if (employee1 == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Funcionário não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {

                    employee.ChangeDate = DateTime.Now.ToLocalTime();
                    _context.Employees.Update(employee);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Funcionário atualizado";
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
