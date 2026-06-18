using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesonConnect.Models
{
    [Table("pedidos_pedidos")]
    public class Pedido
    {
        [Key]
        public long id { get; set; }

        public DateTime fecha_pedido { get; set; }

        public decimal total { get; set; }

        public long Cliente_id { get; set; } 
        public string Estado { get; set; } = "Pendiente";
    }
}