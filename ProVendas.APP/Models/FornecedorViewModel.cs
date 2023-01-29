using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class FornecedorViewModel
    {
        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Fornecedor { get; set; }

        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public string Ds_Fornecedor { get; set; }

        [DisplayName("N° Documento")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public string Ds_Documento { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public TipoPessoaViewModel TipoPessoa { get; set; }
        public ContatoViewModel Contato { get; set; }
    }
}
