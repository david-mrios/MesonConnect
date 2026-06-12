using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MesonConnect.Models
{
    public class Testimonio
    {
        [Key]
        public int IdTestimonio { get; set; }

        public long IdCliente { get; set; }

        public required string Mensaje { get; set; }

        public required int Calificacion { get; set; }

        public bool Estado { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
    }
}