using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class PessoaViewModel
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Pessoa { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Ds_Pessoa { get; set; }

        [DisplayName("N° CPF/CNPJ")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(18, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 14)]
        public string Ds_Documento { get; set; }

        [DisplayName("N° IE/RG")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Ds_InscricaoEstadual { get; set; }
    }
}
