using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesonConnect.Models
{
    [Table("clientes_cliente")]
    public class Cliente
    {
        [Key]
        public long id { get; set; }

        public required string nombre { get; set; }

        public required string correo { get; set; }

        public string? telefono { get; set; }

        public string? direccion { get; set; }

        public required string  contrasena { get; set; }
    }
}




