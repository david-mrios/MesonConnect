namespace MesonConnect.Models
{
    public class PedidoItem
    {
        public int platilloId { get; set; }

        public string name { get; set; } = "";

        public decimal price { get; set; }

        public int cantidad { get; set; }
    }
}