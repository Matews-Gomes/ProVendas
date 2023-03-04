using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ClienteModel>> GetAllAsync()
        {
            List<ClienteModel> clientes = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_CLIENTE", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    clientes.Add(new ClienteModel()
                    {
                        TipoPessoa = new TipoPessoaModel()
                        {
                            Ds_TipoPessoa = reader["Ds_TipoPessoa"].ToString().ToUpper()
                        },

                        Id_Cliente = Convert.ToInt32(reader["Id_Cliente"]),
                        Vl_limite = Convert.ToDecimal(reader["Vl_limite"]),
                        Tp_Fornecedor = Convert.ToBoolean(reader["Tp_Fornecedor"]),

                        PessoaDocumento = new PessoaModel()
                        {
                            Ds_Pessoa = reader["Ds_Pessoa"].ToString().ToUpper(),
                            Ds_Documento = reader["Ds_Documento"].ToString(),
                            Ds_InscricaoEstadual = reader["Ds_InscricaoEstadual"].ToString()
                        },

                        Contato = new ContatoModel()
                        {
                            Ds_Celular = reader["Ds_Celular"].ToString(),
                            Ds_Telefone = reader["Ds_Telefone"].ToString(),
                        }
                    });
                }
                return clientes;
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

        public async Task<ClienteModel> GetByIdAsync(int id)
        {
            ClienteModel clienteById = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_CLIENTE WHERE ID_CLIENTE = " + id, conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    ClienteModel cliente = new()
                    {
                        TipoPessoa = new TipoPessoaModel()
                        {
                            Id_TipoPessoa = Convert.ToInt32(reader["Id_TipoPessoa"]),
                            Ds_TipoPessoa = reader["Ds_TipoPessoa"].ToString().ToUpper()
                        },

                        Id_Cliente = Convert.ToInt32(reader["Id_Cliente"]),
                        Vl_limite = Convert.ToDecimal(reader["Vl_limite"]),
                        Tp_Fornecedor = Convert.ToBoolean(reader["Tp_Fornecedor"]),

                        PessoaDocumento = new PessoaModel()
                        {
                            Ds_Pessoa = reader["Ds_pessoa"].ToString().ToUpper(),
                            Ds_Documento = reader["Ds_Documento"].ToString(),
                            Ds_InscricaoEstadual = reader["Ds_InscricaoEstadual"].ToString()
                        },

                        Endereco = new EnderecoModel()
                        {
                            Ds_Logradouro = reader["Ds_Logradouro"].ToString().ToUpper(),
                            Ds_Complemento = reader["Ds_Complemento"].ToString().ToUpper(),
                            Ds_Numero = reader["Ds_Numero"].ToString(),
                            Ds_Bairro = reader["Ds_Bairro"].ToString().ToUpper(),
                            Ds_Cep = reader["Ds_Cep"].ToString(),
                            Cidade = new CidadeModel()
                            {
                                Ds_Cidade = reader["Ds_Cidade"].ToString().ToUpper(),

                                Estado = new EstadoModel()
                                {
                                    Cd_Estado = reader["Cd_Estado"].ToString().ToUpper()
                                }

                            },                           
                        },

                        Contato = new ContatoModel()
                        {
                            Ds_Celular = reader["Ds_Celular"].ToString(),
                            Ds_Telefone = reader["Ds_Telefone"].ToString(),
                            Ds_Email = reader["Ds_Email"].ToString(),
                            Ds_Site = reader["Ds_Site"].ToString(),
                        }
                    };

                    clienteById = cliente;
                }

                return clienteById;
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

        public async Task AddAsync(ClienteModel entity)
        {

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_CLIENTE_INS", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Ds_Pessoa", entity.PessoaDocumento.Ds_Pessoa);
                cmd.Parameters.AddWithValue("@Ds_Documento", entity.PessoaDocumento.Ds_Documento);
                cmd.Parameters.AddWithValue("@Ds_InscricaoEstadual", entity.PessoaDocumento.Ds_InscricaoEstadual);
                cmd.Parameters.AddWithValue("@Vl_Limite", entity.Vl_limite);
                cmd.Parameters.AddWithValue("@Tp_Fornecedor", entity.Tp_Fornecedor);
                cmd.Parameters.AddWithValue("@Id_TipoPessoa", entity.TipoPessoa.Id_TipoPessoa);
                cmd.Parameters.AddWithValue("@Ds_Logradouro", entity.Endereco.Ds_Logradouro);
                cmd.Parameters.AddWithValue("@Ds_Numero", entity.Endereco.Ds_Numero);
                cmd.Parameters.AddWithValue("@Ds_Complemento", entity.Endereco.Ds_Complemento);
                cmd.Parameters.AddWithValue("@Ds_Bairro", entity.Endereco.Ds_Bairro);
                cmd.Parameters.AddWithValue("@Ds_Cep", entity.Endereco.Ds_Cep);
                cmd.Parameters.AddWithValue("@Ds_Cidade", entity.Endereco.Cidade.Ds_Cidade);
                cmd.Parameters.AddWithValue("@Cd_Estado", entity.Endereco.Cidade.Estado.Cd_Estado);
                cmd.Parameters.AddWithValue("@Ds_Telefone", entity.Contato.Ds_Telefone);
                cmd.Parameters.AddWithValue("@Ds_Celular", entity.Contato.Ds_Celular);
                cmd.Parameters.AddWithValue("@Ds_Email", entity.Contato.Ds_Email);
                cmd.Parameters.AddWithValue("@Ds_Site", entity.Contato.Ds_Site);

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

        public async Task UpdateAsync(ClienteModel entity)
        {
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_CLIENTE_UPD", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id_Pessoa", entity.Id_Cliente);
                cmd.Parameters.AddWithValue("@Ds_Pessoa", entity.PessoaDocumento.Ds_Pessoa);
                cmd.Parameters.AddWithValue("@Ds_Documento", entity.PessoaDocumento.Ds_Documento);
                cmd.Parameters.AddWithValue("@Vl_Limite", entity.Vl_limite);
                cmd.Parameters.AddWithValue("@Tp_Fornecedor", entity.Tp_Fornecedor);
                cmd.Parameters.AddWithValue("@Ds_InscricaoEstadual", entity.PessoaDocumento.Ds_InscricaoEstadual);
                cmd.Parameters.AddWithValue("@Id_TipoPessoa", entity.TipoPessoa.Id_TipoPessoa);
                cmd.Parameters.AddWithValue("@Ds_Logradouro", entity.Endereco.Ds_Logradouro);
                cmd.Parameters.AddWithValue("@Ds_Numero", entity.Endereco.Ds_Numero);
                cmd.Parameters.AddWithValue("@Ds_Complemento", entity.Endereco.Ds_Complemento);
                cmd.Parameters.AddWithValue("@Ds_Bairro", entity.Endereco.Ds_Bairro);
                cmd.Parameters.AddWithValue("@Ds_Cep", entity.Endereco.Ds_Cep);
                cmd.Parameters.AddWithValue("@Ds_Cidade", entity.Endereco.Cidade.Ds_Cidade);
                cmd.Parameters.AddWithValue("@Cd_Estado", entity.Endereco.Cidade.Estado.Cd_Estado);
                cmd.Parameters.AddWithValue("@Ds_Telefone", entity.Contato.Ds_Telefone);
                cmd.Parameters.AddWithValue("@Ds_Celular", entity.Contato.Ds_Celular);
                cmd.Parameters.AddWithValue("@Ds_Email", entity.Contato.Ds_Email);
                cmd.Parameters.AddWithValue("@Ds_Site", entity.Contato.Ds_Site);

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
                SqlCommand cmd = new("SP_CLIENTE_DEL", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id_Pessoa", id);

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

    }
}
