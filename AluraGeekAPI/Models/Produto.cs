using System.ComponentModel.DataAnnotations;

namespace AluraGeekAPI.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        //range de valores
        //definir tipo da coluna
        
        
        [Range(1, 1000)]
        public decimal? Preco { get; set; }
        public string? Categoria { get; set; }
    }
}
