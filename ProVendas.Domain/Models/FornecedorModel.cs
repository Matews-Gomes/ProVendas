namespace ProVendas.Domain.Models
{
    public class FornecedorModel
    {
        public int Id_Fornecedor { get; set; }
        public string Ds_Fornecedor { get; set; }
        public PessoaModel PessoaDocumento { get; set; }
        public bool Tp_Cliente { get; set; }
        public TipoPessoaModel TipoPessoa { get; set; }
        public EnderecoModel Endereco { get; set; }       
        public ContatoModel Contato { get; set; }
    }
}
