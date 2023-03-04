using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProVendas.APP.Models
{
    public class UsuarioViewModel
    {
        public PessoaViewModel Usuario { get; set; }

        [DisplayName("Usuário")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 4)]
        public string Ds_Usuario { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [DataType(DataType.Password)]
        public string Ds_Senha { get; set; }
        public FilialViewModel Filial { get; set; }
        public PerfilViewModel Perfil { get; set; }
    }
}
