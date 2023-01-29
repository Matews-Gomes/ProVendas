using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class CidadeViewModel
    {
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Cidade { get; set; }

        [DisplayName("Cidade")]
        public string Ds_Cidade { get; set; }

    }
}
