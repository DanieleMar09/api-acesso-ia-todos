using System.ComponentModel.DataAnnotations.Schema;

namespace api_acesso_ia.Models
{
    [Table("login_usuarios")]
    public class LoginUsuario
    {  public int Id { get; set; }
      
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Cpf { get; set; }
        public string Login { get; set; }
        public string  Senha { get; set; }

       
    }
}
 