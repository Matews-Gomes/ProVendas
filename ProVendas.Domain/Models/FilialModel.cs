namespace ProVendas.Domain.Models

{
    public class FilialModel
    {
        public int Id_Filial { get; set; }
        public string Ds_Filial { get; set; }
        public string Cm_Certificado { get; set; }
        public DateTime Dt_Abertura { get; set; }
        public PessoaModel PessoaDocumento { get; set; }
        public TipoPessoaModel TipoPessoa { get; set; }
        public EnderecoModel Endereco { get; set; }
        public ContatoModel Contato { get; set; }
    }
}
