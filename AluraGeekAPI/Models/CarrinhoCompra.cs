namespace AluraGeekAPI.Models
{
    public class CarrinhoCompra
    {
        public int CarrinhoCompraId { get; set; }
        public List<CarrinhoItem> CarrinhoItens { get; set; }
    }
}
