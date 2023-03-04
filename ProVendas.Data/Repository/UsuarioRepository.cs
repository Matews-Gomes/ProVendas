using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;
        public UsuarioRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<UsuarioModel>> GetAllAsync()
        {
            List<UsuarioModel> usuarios = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_USUARIOS", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    usuarios.Add(new UsuarioModel()
                    {
                        Usuario = new PessoaModel()
                        {
                            Id_Pessoa = Convert.ToInt32(reader["Id_Usuario"]),
                            Ds_Pessoa = reader["Ds_Usuario"].ToString().ToUpper(),
                        },

                        Ds_Usuario = reader["Ds_Usuario"].ToString().ToUpper(),
                        Ds_Senha = reader["Ds_Senha"].ToString().Trim(),

                        Perfil = new PerfilModel()
                        {
                            Id_Perfil = Convert.ToInt32(reader["Id_Perfil"]),
                            Ds_Perfil = reader["Ds_Perfil"].ToString().ToUpper(),                            
                        },

                        Filial = new FilialModel()
                        {
                            Id_Filial = Convert.ToInt32(reader["Id_Filial"]),
                            Ds_Filial = reader["Ds_Filial"].ToString().ToUpper(),                           
                        },                        
                    });
                }

                return usuarios;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar recuperar registro: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        public async Task<UsuarioModel> GetByIdAsync(int id)
        {
            UsuarioModel usuarioByID = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_USUARIOS  WHERE ID_USUARIO = " + id, conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    UsuarioModel usuario = new()
                    {
                        Usuario = new PessoaModel()
                        {
                            Id_Pessoa = Convert.ToInt32(reader["Id_Usuario"]),
                            Ds_Pessoa = reader["Ds_Pessoa"].ToString().ToUpper(),
                        },

                        Ds_Usuario = reader["Ds_Usuario"].ToString().ToUpper(),
                        Ds_Senha = reader["Ds_Senha"].ToString().Trim(),

                        Perfil = new PerfilModel()
                        {
                            Id_Perfil = Convert.ToInt32(reader["Id_Perfil"]),
                            Ds_Perfil = reader["Ds_Perfil"].ToString().ToUpper(),
                        },

                        Filial = new FilialModel()
                        {
                            Id_Filial = Convert.ToInt32(reader["Id_Filial"]),
                            Ds_Filial = reader["Ds_Filial"].ToString().ToUpper(),
                        },
                    };

                    usuarioByID = usuario;
                }

                return usuarioByID;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar recuperar registro: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        public async Task AddAsync(UsuarioModel entity)
        {
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_USUARIO_INS", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id_Usuario", entity.Usuario.Id_Pessoa);
                cmd.Parameters.AddWithValue("@Ds_Usuario", entity.Ds_Usuario);
                cmd.Parameters.AddWithValue("@Ds_Senha", entity.Ds_Senha);
                cmd.Parameters.AddWithValue("@Id_Filial", entity.Filial.Id_Filial);
                cmd.Parameters.AddWithValue("@Id_Perfil", entity.Perfil.Id_Perfil);

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

        public async Task UpdateAsync(UsuarioModel entity)
        {
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_USUARIO_UPD", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id_Usuario", entity.Usuario.Id_Pessoa);
                cmd.Parameters.AddWithValue("@Ds_Usuario", entity.Ds_Usuario);
                cmd.Parameters.AddWithValue("@Ds_Senha", entity.Ds_Senha);
                cmd.Parameters.AddWithValue("@Id_Filial", entity.Filial.Id_Filial);
                cmd.Parameters.AddWithValue("@Id_Perfil", entity.Perfil.Id_Perfil);

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

        public async Task DeleteAsync(int id)
        {
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_USUARIO_DEL", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id_Usuario", id);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar deletar registro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /*public async Task<UsuarioModel> Login(UsuarioModel entity)
        {
            UsuarioModel usuarioLogin = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_USUARIOS " +
                 "WHERE DS_USUARIO = '" + entity.Ds_Usuario.ToUpper() + "' AND DS_SENHA = '" + entity.Ds_Senha.ToUpper() + "'", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    UsuarioModel usuario = new()
                    {

                        Usuario = new PessoaModel()
                        {
                            Id_Pessoa = Convert.ToInt32(reader["Id_Usuario"]),
                            Ds_Pessoa = reader["Ds_Pessoa"].ToString().ToUpper(),
                        },

                        Perfil = new PerfilModel()
                        {
                            Id_Perfil = Convert.ToInt32(reader["Id_Perfil"]),
                        },

                        Filial = new FilialModel()
                        {
                            Id_Filial = Convert.ToInt32(reader["Id_Filial"]),
                        },
                    };

                    usuarioLogin = usuario;

                }

                return usuarioLogin;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar recuperar registro: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }*/

        public bool Login(string usuario, string senha)
        {
            var logado = false;
            using SqlConnection conn = new(Connection);

            try
            {
                
                if (usuario != null & senha != null)
                {
                    conn.Open();

                    SqlCommand cmd = new("SELECT * FROM VW_USUARIOS " +
                        "WHERE DS_USUARIO = '" + usuario.ToUpper() + "' AND DS_SENHA = '" + senha.ToUpper() + "'", conn)
                    {
                        CommandType = System.Data.CommandType.Text,
                    };

                    var reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        reader.Read();

                        UsuarioModel usuarioLogin = new()
                        {

                            Usuario = new PessoaModel()
                            {
                                Id_Pessoa = Convert.ToInt32(reader["Id_Usuario"]),
                                Ds_Pessoa = reader["Ds_Pessoa"].ToString().ToUpper(),
                            },

                            Perfil = new PerfilModel()
                            {
                                Id_Perfil = Convert.ToInt32(reader["Id_Perfil"]),
                            },

                            Filial = new FilialModel()
                            {
                                Id_Filial = Convert.ToInt32(reader["Id_Filial"]),
                            },
                        };

                        logado = true;
                    }
                }

                return logado;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar recuperar registro: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }
    }
}
