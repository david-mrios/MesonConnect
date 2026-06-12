namespace MesonConnect.Models
{
    public class CategoriaPlatillo
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

        // Relación opcional (MUY recomendada)
        public List<Platillo>? Platillo { get; set; }
    }
}