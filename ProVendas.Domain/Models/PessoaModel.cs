using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProVendas.Domain.Models
{
    public class PessoaModel
    {
        public int Id_PessoaDocumento { get; set; }
        public string Ds_Documento { get; set; }
        public string Ds_InscricaoEstadual { get; set;}
    }
}
