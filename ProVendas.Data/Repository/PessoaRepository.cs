using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;

        public PessoaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<PessoaModel>> GetAllAsync()
        {
            List<PessoaModel> pessoas = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT ID_PESSOA,DS_PESSOA FROM VW_PESSOA", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    pessoas.Add(new PessoaModel()
                    {
                        Id_Pessoa = Convert.ToInt32(reader["Id_Pessoa"]),
                        Ds_Pessoa = reader["Ds_Pessoa"].ToString().ToUpper(),
                    });
                }

                return pessoas;
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
