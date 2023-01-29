using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class CidadeRepository : ICidadeRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;
        public CidadeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<CidadeModel>> GetAllAsync()
        {
            List<CidadeModel> cidades = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            SqlCommand cmd = new("SELECT ID_CIDADE,DS_CIDADE FROM VW_CIDADE", conn)
            {
                CommandType = System.Data.CommandType.Text,
            };

            try
            {
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    cidades.Add(new CidadeModel()
                    {
                        Id_Cidade = Convert.ToInt32(reader["Id_Cidade"]),
                        Ds_Cidade = reader["Ds_Cidade"].ToString().ToUpper()
                    });
                }

                return cidades;
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

        public Task<CidadeModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(CidadeModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CidadeModel entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
