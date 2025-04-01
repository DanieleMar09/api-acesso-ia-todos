using System.ComponentModel.DataAnnotations.Schema;

namespace api_acesso_ia.Models
{
    [Table("registrar")]
    public class Registrar
    {
        public int Id { get; set; }
        public int IdUsuarios { get; set; }
        public DateTime DataHoraAcesso { get; set; }
      

    }
}
