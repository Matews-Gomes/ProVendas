using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProVendas.APP.Models
{
    public class ClienteViewModel
    {
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Cliente { get; set; }

        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [DataType(DataType.Currency)]
        [Display(Name = "Limte de Compras", Prompt = "0,00")]
        public decimal Vl_Limite { get; set; }

        [Display(Name = "Marcar como Fornecedor ?")]
        public bool Tp_Fornecedor { get; set; }

        public PessoaViewModel PessoaDocumento { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public TipoPessoaViewModel TipoPessoa { get; set; }
        public ContatoViewModel Contato { get; set; }
    }
}
