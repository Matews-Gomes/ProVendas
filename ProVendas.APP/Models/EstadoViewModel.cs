using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class EstadoViewModel
    {
        
        public int Id_Estado { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public string Cd_Estado { get; set; }
    }
}
