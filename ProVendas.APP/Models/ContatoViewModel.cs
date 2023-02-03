using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class ContatoViewModel
    {
        public int Id_Contato { get; set; }

        [DisplayName("Telefone")]
        [StringLength(14, ErrorMessage = "O campo {0} aceita no máximo até {1} caracteres")]
        public string Ds_Telefone { get; set; }

        [DisplayName("Celular")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(15, ErrorMessage = "O campo {0} aceita no máximo até {1} caracteres")]
        public string Ds_Celular { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Ds_Email { get; set; }

        [DisplayName("Web Site")]
        [DataType(DataType.Url)]
        public string Ds_Site { get; set; }
        public int Id_PessoaContato { get; set; }
    }
}
