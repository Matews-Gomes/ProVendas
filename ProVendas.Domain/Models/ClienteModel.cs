namespace ProVendas.Domain.Models
{
    public class ClienteModel
    {
        public int Id_Cliente { get; set; }
        public string Ds_Cliente { get; set;}
        public PessoaModel PessoaDocumento { get; set; }
        public decimal Vl_limite { get; set; }
        public bool Tp_Fornecedor { get; set; }
        public TipoPessoaModel TipoPessoa { get; set; }
        public EnderecoModel Endereco { get; set; }
        public ContatoModel Contato { get; set; }
    }
}
