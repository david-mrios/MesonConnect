using Microsoft.AspNetCore.Mvc;

namespace MesonConnect.Models
{
    public class ReservaModel
    {
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Personas { get; set; } = "";
        public string Fecha { get; set; } = "";
        public string Hora { get; set; } = "";
        public string Comentarios { get; set; } = "";
    }
}