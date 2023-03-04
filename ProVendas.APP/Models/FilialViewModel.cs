using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class FilialViewModel
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Filial { get; set; }

        [Display(Name = "Nome Fantasia")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Ds_Filial { get; set; }

        [Display(Name = "N° Certificado Digital")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Cm_Certificado { get; set; }

        [Display(Name = "Data de Abertura")]
        [DataType(DataType.Date)]
        public DateTime Dt_Abertura { get; set; }
        public PessoaViewModel PessoaDocumento { get; set; }
        public TipoPessoaViewModel TipoPessoa { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public ContatoViewModel Contato { get; set; }
    }
}
