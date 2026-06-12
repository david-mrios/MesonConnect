using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesonConnect.Models
{
    [Table("Promocion")]
    public class Promocion
    {
        [Key]
        public int IdPromocion { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public decimal Descuento { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public bool Estado { get; set; }
    }
}