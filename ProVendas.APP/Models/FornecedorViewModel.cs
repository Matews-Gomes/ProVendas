using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class FornecedorViewModel
    {
        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Fornecedor { get; set; }

        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Ds_Fornecedor { get; set; }

        [Display(Name = "Marcar como Cliente ?")]
        public bool Tp_Cliente { get; set; }
        public PessoaViewModel PessoaDocumento { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public TipoPessoaViewModel TipoPessoa { get; set; }
        public ContatoViewModel Contato { get; set; }
    }
}
