using Loja.Model;
using LojaTopMoveis.Interface;
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

       

        [HttpPost]
        [Route("login")]
        public async Task<ServiceResponse<User>> LoginAsync(User user)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            UserService userService = new UserService(_context);
            var hash = userService.QuickHash(user.PasswordHash);
            var login = await _context.Usuarios.Where(u => u.Email == user.Email && u.PasswordHash == hash).FirstOrDefaultAsync();
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

        [HttpPut]
        [Route("atualizasenha")]
        public Task<ServiceResponse<User>> UpdatePassword(User user)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            UserService userService = new UserService(_context);
            user.PasswordHash = userService.QuickHash(user.PasswordHash);
            var update =  userService.Update(user);
            if (update)
            {
                serviceResponse.Sucess = true;
            }
            else
            {
                serviceResponse.Sucess = false;
            }

            return Task.FromResult(serviceResponse);

        }

    }
}
