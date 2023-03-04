using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class EnderecoViewModel
    {

        public int Id_Endereco { get; set; }

        [DisplayName("Logradouro")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Ds_Logradouro { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public string Ds_Numero { get; set; }

        [DisplayName("Complemento")]
        [StringLength(500, ErrorMessage = "O campo {0} aceita até {1} caracteres", MinimumLength = 2)]
        public string Ds_Complemento { get; set; }

        [DisplayName("Cep")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        [StringLength(9, ErrorMessage = "O campo {0} aceita no máximo até {1} caracteres")]
        public string Ds_Cep { get; set; }

        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public string Ds_Bairro { get; set; }
        public int Id_PessoaEndereco { get; set; }
        public CidadeViewModel Cidade { get; set; }
    }
}
