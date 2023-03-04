namespace ProVendas.Domain.Models
{
    public class EnderecoModel
    {
        public int Id_Endereco { get; set; }
        public string Ds_Logradouro { get; set; }
        public string Ds_Numero { get; set; }
        public string Ds_Complemento { get; set; }
        public string Ds_Cep { get; set; }
        public string Ds_Bairro { get; set; }
        public int Id_PessoaEndereco { get; set; }
        public CidadeModel Cidade { get; set; }
    }
}
