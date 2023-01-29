namespace ProVendas.Domain.Models
{
    public class ContatoModel
    {
        public int Id_Contato { get; set; }
        public string Ds_Telefone { get; set; }
        public string Ds_Celular { get; set; }
        public string Ds_Email { get; set; }
        public string Ds_Site { get; set; }
        public int Id_PessoaContato { get; set; }
    }
}
