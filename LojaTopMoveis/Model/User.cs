using System.ComponentModel.DataAnnotations;

namespace LojaTopMoveis.Model
{
    public class User
    {

        public User()
        { }

        public User(string? email, string? password)
        {
            Id = new Guid();
            Email = email;
            Password = password;
        }

        public Guid Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatório!")]
        public string? Password { get; set; }
    }
}
