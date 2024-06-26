﻿using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Model;

namespace LojaTopMoveis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILoja<Employee> _employeeInterface;

        public EmployeeController(ILoja<Employee> employeeInterface)
        {
            _employeeInterface = employeeInterface;
        }

        [HttpPost]
        [Route("/funcionarios")]
        public async Task<ActionResult<ServiceResponse<Employee>>> Get(ServiceParameter<Employee> sp)
        {
            return Ok(await _employeeInterface.Get(sp));
        }

        [HttpPost("/funcionarios/0")]
        public async Task<ActionResult<ServiceResponse<Employee>>> Create(Employee employee)
        {
            return Ok(await _employeeInterface.Create(employee));
        }

        [HttpGet("/funcionarios/{id}")]
        public async Task<ActionResult<ServiceResponse<Employee>>> GetById(Guid id)
        {
            return Ok(await _employeeInterface.GetByID(id));
        }

        
        /*[HttpPut("/funcionarios/id")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> InactivateProduct(Guid id)
        {
            return Ok(await _employeeInterface.InactivateProduct(id));
        }*/

        [HttpPut("/funcionarios/{id}")]
        public async Task<ActionResult<ServiceResponse<Employee>>> Update(Employee employee)
        {
            return Ok(await _employeeInterface.Update(employee));
        }

        [HttpDelete("/funcionarios/{id}")]
        public async Task<ActionResult<ServiceResponse<Employee>>> Delete(Guid id)
        {
            return Ok(await _employeeInterface.Delete(id));
        }
    }
    
    

}
