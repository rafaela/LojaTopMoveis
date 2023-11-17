using Loja.Model;
using LojaTopMoveis.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LojaTopMoveis.Service
{
    public class TokenService : ILoja<User>
    {
        public TokenService()
        {

        }
        public string Generate(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
            };

            var token = handler.CreateToken(tokenDescriptor);

            var strToken = handler.WriteToken(token);

            return strToken;
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(type: ClaimTypes.Name, value: user.Email));

            return ci;
        }

        public Task<ServiceResponse<List<User>>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<User>>> GetFilter(User t)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> Create(User t)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> Update(User t)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> Inactivate(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
