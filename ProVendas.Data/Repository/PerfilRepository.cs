using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProVendas.Data.Repository
{
    public class PerfilRepository : IPerfilRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;
        public PerfilRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<PerfilModel>> GetAllAsync()
        {
            List<PerfilModel> perfis = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            SqlCommand cmd = new("SELECT ID_PERFIL,DS_PERFIL FROM VW_PERFIL", conn)
            {
                CommandType = System.Data.CommandType.Text,
            };

            try
            {
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    perfis.Add(new PerfilModel()
                    {
                        Id_Perfil = Convert.ToInt32(reader["Id_Perfil"]),
                        Ds_Perfil = reader["Ds_Perfil"].ToString().ToUpper()
                    });
                }

                return perfis;
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
