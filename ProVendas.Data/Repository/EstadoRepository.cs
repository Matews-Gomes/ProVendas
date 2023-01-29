using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class EstadoRepository : IEstadoRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;

        public EstadoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<EstadoModel>> GetAllAsync()
        {
            List<EstadoModel> estados = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            SqlCommand cmd = new("SELECT ID_ESTADO,DS_ESTADO FROM VW_ESTADO", conn)
            {
                CommandType = System.Data.CommandType.Text,
            };

            try
            {
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    estados.Add(new EstadoModel()
                    {
                        Id_Estado = Convert.ToInt32(reader["Id_Estado"]),
                        Ds_Estado = reader["Ds_Estado"].ToString().ToUpper()
                    });
                }

                return estados;
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

        public Task<EstadoModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EstadoModel entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(EstadoModel entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
