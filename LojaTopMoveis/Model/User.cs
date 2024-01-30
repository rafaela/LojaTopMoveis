using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Topmoveis.Model;

namespace LojaTopMoveis.Model
{
    public class User : IdentityUser 
    {

        public User()
        { }

        public User(string? email, string? password)
        {
            Email = email;
            UserName = email;
            PasswordHash = password;
        }

        public override string Id { get; set; } = "";

        public override string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório!")]
        public override string? Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatório!")]
        public override string? PasswordHash { get; set; }

    }
}
