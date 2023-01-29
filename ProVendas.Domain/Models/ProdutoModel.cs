using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ProVendas.Domain.Models
{
    public class ProdutoModel
    {
        public int Id_Produto { get; set; }
        public string Ds_Produto { get; set; }
        public string Ds_Descricao { get; set; }
        public decimal Vl_PrecoCusto { get; set; }
        public decimal Vl_PrecoVenda { get; set; }
        [JsonIgnore]
        public IFormFile ImagemUpload { get; set; }
        public string Ds_Imagem { get; set; }
        public int Id_Fornecedor { get; set; }
        public string Ds_Fornecedor { get; set; }
        public int Id_Categoria { get; set; }
        public string Ds_Categoria { get; set; }

    }
}
