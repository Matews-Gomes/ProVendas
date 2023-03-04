namespace ProVendas.Domain.Models
{
    public class UsuarioModel
    {
        public PessoaModel Usuario { get; set; }
        public string Ds_Usuario { get; set; }
        public string Ds_Senha { get; set; }
        public FilialModel Filial { get; set; }
        public PerfilModel Perfil { get; set; }

    }
}
