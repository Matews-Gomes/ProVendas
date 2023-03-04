using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class CidadeViewModel
    {
        
        public int Id_Cidade { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public string Ds_Cidade { get; set; }

        public EstadoViewModel Estado { get; set; }

    }
}
