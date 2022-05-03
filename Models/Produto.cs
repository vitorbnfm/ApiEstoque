﻿namespace API.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public bool Ativo { get; set; }
        public Categoria Categoria { get; set; }
    }
}
