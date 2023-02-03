using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProVendas.APP.Models
{
    public class CategoriaViewModel
    {
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public int Id_Categoria { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "O Campo {0}, é Obrigatório!")]
        public string Ds_Categoria { get; set; }
    }
}
