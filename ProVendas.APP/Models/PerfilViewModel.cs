using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class PerfilViewModel
    {
        [DisplayName("Perfil")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Perfil { get; set; }

        [DisplayName("Perfil")]
        public string Ds_Perfil { get; set; }
    }
}
