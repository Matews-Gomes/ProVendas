using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class TipoPessoaRepository : ITipoPessoaRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;

        public TipoPessoaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<TipoPessoaModel>> GetAllAsync()
        {
            List<TipoPessoaModel> tipoPessoas = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            SqlCommand cmd = new("SELECT ID_TIPOPESSOA,DS_TIPOPESSOA FROM VW_TIPOPESSOA", conn)
            {
                CommandType = System.Data.CommandType.Text,
            };

            try
            {
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    tipoPessoas.Add(new TipoPessoaModel()
                    {
                        Id_TipoPessoa = Convert.ToInt32(reader["Id_TipoPessoa"]),
                        Ds_TipoPessoa = reader["Ds_TipoPessoa"].ToString().ToUpper()
                    });
                }

                return tipoPessoas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar registros: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }       

    }
}
