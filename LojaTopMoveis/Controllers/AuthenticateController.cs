using Loja.Model;
using LojaTopMoveis.Model;
using LojaTopMoveis.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
 /*       private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;*/

        private readonly ILoja<User> _userInterface;
        private readonly LojaContext _context;

        public AuthenticateController(ILoja<User> userInterface, LojaContext context)
        {
            _userInterface = userInterface;
            _context = context;
        }

       /* [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] User model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists is not null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ServiceResponse<Employee> { Sucess = false, Message = "Usuário já existe!" }
                );

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ServiceResponse<Employee> { Sucess = false, Message = "Erro ao criar usuário" }
                );



            return Ok(new ServiceResponse<Employee> { Message = "Usuário criado com sucesso!" });
        }
       */

        [HttpPost]
        [Route("login")]
        public async Task<ServiceResponse<User>> LoginAsync(User user)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            var login = await _context.Usuarios.Where(u => u.Email == user.Email && u.PasswordHash == user.PasswordHash).FirstOrDefaultAsync();
            if(login != null)
            {
                var tokenService = new TokenService();
                var token = tokenService.Generate(user);

                serviceResponse.Sucess = true;
                serviceResponse.Token = token;
             
            }
            else
            {
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }

    }
}
