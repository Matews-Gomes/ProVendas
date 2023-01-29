using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class ProdutoViewModel
    {
        public int Id_Produto { get; set; }

        [DisplayName("Produto")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Ds_Produto { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Ds_Descricao { get; set; }

        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [DataType(DataType.Currency)]
        [Display(Name = "Custo", Prompt ="0,00")]
        public decimal Vl_PrecoCusto { get; set; }

        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor", Prompt = "0,00")]
        public decimal Vl_PrecoVenda { get; set; }

        [JsonIgnore]
        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }

        public string Ds_Imagem { get; set; }

        [Display(Name = "Fornecedor", Prompt = "Selecione...")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Fornecedor { get; set; }

        [DisplayName("Fornecedor")]
        public string Ds_Fornecedor { get; set; }

        [Display(Name = "Categoria", Prompt = "Selecione...")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Categoria { get; set; }

        [DisplayName("Categoria")]
        public string Ds_Categoria { get; set; }
    }
}
