using System;

namespace API.Models
{
    public class Venda
    {   
        public Venda() => DataVenda = DateTime.Now;
        public int VendaId { get; set; }
        public double TotalVenda { get; set; }
        public int QuantidadeVenda { get; set; }
        public double PrecoUnitario { get; set; }
        public int Documento { get; set; }
        public DateTime DataVenda { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

   



    }
}
