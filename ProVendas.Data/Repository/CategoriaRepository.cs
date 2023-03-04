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

        public async Task<CategoriaModel> GetByIdAsync(int id)
        {
            CategoriaModel categoriaById = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_CATEGORIA WHERE ID_CATEGORIA = " + id, conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    CategoriaModel categoria = new()
                    {
                        Id_Categoria = Convert.ToInt32(reader["Id_Categoria"]),
                        Ds_Categoria = reader["Ds_Categoria"].ToString().ToUpper()
                    };

                    categoriaById = categoria;

                }

                return categoriaById;
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

        public async Task AddAsync(CategoriaModel entity)
        {
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_CATEGORIA_INS", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Ds_Categoria", entity.Ds_Categoria);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar adicionar registro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
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
