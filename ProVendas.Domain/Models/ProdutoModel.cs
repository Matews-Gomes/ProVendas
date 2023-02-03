using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ProVendas.Domain.Models
{
    public class ProdutoModel
    {
        [JsonIgnore]
        public IFormFile ImagemUpload { get; set; }
        public int Id_Produto { get; set; }
        public string Ds_Produto { get; set; }
        public string Ds_Descricao { get; set; }
        public decimal Vl_PrecoCusto { get; set; }
        public decimal Vl_PrecoVenda { get; set; }
        public int Qt_Quantidade { get; set; }
        public string Ds_Imagem { get; set; }
        public FornecedorModel Fornecedor { get; set; }
        public CategoriaModel Categoria { get; set; }

    }
}
