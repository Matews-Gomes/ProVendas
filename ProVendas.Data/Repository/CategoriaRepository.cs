using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;
        public CategoriaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<CategoriaModel>> GetAllAsync()
        {
            List<CategoriaModel> categorias = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_CATEGORIA", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    categorias.Add(new CategoriaModel()
                    {
                        Id_Categoria = Convert.ToInt32(reader["Id_Categoria"]),
                        Ds_Categoria = reader["Ds_Categoria"].ToString().ToUpper()
                    });
                }

                return categorias;
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

        public Task<CategoriaModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(CategoriaModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CategoriaModel entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
