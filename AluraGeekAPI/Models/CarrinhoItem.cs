﻿namespace AluraGeekAPI.Models
{
    public class CarrinhoItem
    {
        public int CarrinhoItemId { get; set; }
        
        public Produto Produto { get; set; } 
        public int Quantidade { get; set; }

        public string CarrinhoCompraId { get; set; }
    }
}
