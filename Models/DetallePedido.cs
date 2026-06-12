using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesonConnect.Models
{
    [Table("DetallePedidos")]
    public class DetallePedido
    {
        [Key]
        public int Id { get; set; }
        public long PedidoId { get; set; }

        public int PlatilloId { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }
}