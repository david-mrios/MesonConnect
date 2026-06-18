namespace MesonConnect.Models
{
    public class Platillo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }

        public int CategoriaPlatilloId { get; set; }

        public CategoriaPlatillo? CategoriaPlatillo { get; set; }

        public string? Categoria { get; set; } 
    }
}