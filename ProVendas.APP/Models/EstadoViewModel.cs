using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class EstadoViewModel
    {
        [DisplayName("Estado")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Estado { get; set; }

        [DisplayName("Estado")]
        public string Ds_Estado { get; set; }

        [DisplayName("Estado")]
        public string Cd_Estado { get; set; }
    }
}
