namespace MesonConnect.Models
{
    public class CategoriaPlatillo
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

        // RELACION 
        public List<Platillo>? Platillo { get; set; }
    }
}