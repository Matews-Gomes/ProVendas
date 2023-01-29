using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class TipoPessoaViewModel
    {

        [DisplayName("Tipo Pessoa")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_TipoPessoa { get; set; }

        [DisplayName("Tipo Pessoa")]
        public string Ds_TipoPessoa { get; set; }
    }
}
