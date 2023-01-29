using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;

        public FornecedorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<FornecedorModel>> GetAllAsync()
        {
            List<FornecedorModel> fornecedores = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT ID_FORNECEDOR,DS_FORNECEDOR,DS_DOCUMENTO,DS_TIPOPESSOA,DS_CELULAR,DS_TELEFONE FROM VW_FORNECEDOR", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    fornecedores.Add(new FornecedorModel()
                    {
                        TipoPessoa = new TipoPessoaModel()
                        {
                            Ds_TipoPessoa = reader["Ds_TipoPessoa"].ToString().ToUpper()
                        },
                        Id_Fornecedor = Convert.ToInt32(reader["Id_Fornecedor"]),
                        Ds_Fornecedor = reader["Ds_Fornecedor"].ToString().ToUpper(),
                        Ds_Documento = reader["Ds_Documento"].ToString(),
                        
                        Contato = new ContatoModel()
                        {
                            Ds_Celular = reader["Ds_Celular"].ToString(),
                            Ds_Telefone = reader["Ds_Telefone"].ToString(),
                        }
                    });
                }

                return fornecedores;
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
        public async Task<FornecedorModel> GetByIdAsync(int id)
        {
            FornecedorModel fornecedorById = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {

                SqlCommand cmd = new("SELECT * FROM VW_FORNECEDOR WHERE ID_FORNECEDOR = " + id, conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    FornecedorModel fornecedor = new()
                    {

                        TipoPessoa = new TipoPessoaModel()
                        {
                            Id_TipoPessoa = Convert.ToInt32(reader["Id_TipoPessoa"]),
                            Ds_TipoPessoa = reader["Ds_TipoPessoa"].ToString().ToUpper()
                        },
                        Id_Fornecedor = Convert.ToInt32(reader["Id_Fornecedor"]),
                        Ds_Fornecedor = reader["Ds_Fornecedor"].ToString().ToUpper(),
                        Ds_Documento = reader["Ds_Documento"].ToString(),
                        Endereco = new EnderecoModel()
                        {
                            Ds_Logradouro = reader["Ds_Logradouro"].ToString().ToUpper(),
                            Ds_Complemento = reader["Ds_Complemento"].ToString().ToUpper(),
                            Ds_Numero = reader["Ds_Numero"].ToString(),
                            Ds_Bairro = reader["Ds_Bairro"].ToString().ToUpper(),
                            Ds_Cep = reader["Ds_Cep"].ToString(),
                            Cidade = new CidadeModel()
                            {
                                Id_Cidade = Convert.ToInt32(reader["Id_Cidade"]),
                                Ds_Cidade = reader["Ds_Cidade"].ToString().ToUpper()
                            },
                            Estado = new EstadoModel()
                            {
                                Id_Estado = Convert.ToInt32(reader["Id_Estado"]),
                                Ds_Estado = reader["Ds_Estado"].ToString().ToUpper(),
                                Cd_Estado = reader["Cd_Estado"].ToString().ToUpper()
                            }
                        },
                        Contato = new ContatoModel()
                        {
                            Ds_Celular = reader["Ds_Celular"].ToString(),
                            Ds_Telefone = reader["Ds_Telefone"].ToString(),
                            Ds_Email = reader["Ds_Email"].ToString(),
                            Ds_Site = reader["Ds_Site"].ToString()
                        }
                    };

                    fornecedorById = fornecedor;
                }

                return fornecedorById;
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

        public async Task AddAsync(FornecedorModel entity)
        {
            using SqlConnection conn = new(Connection); 
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_FORNECEDOR_INS", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Ds_Fornecedor", entity.Ds_Fornecedor);
                cmd.Parameters.AddWithValue("@Ds_Documento", entity.Ds_Documento);
                cmd.Parameters.AddWithValue("@Id_TipoPessoa", entity.TipoPessoa.Id_TipoPessoa);
                cmd.Parameters.AddWithValue("@Ds_Logradouro", entity.Endereco.Ds_Logradouro);
                cmd.Parameters.AddWithValue("@Ds_Numero", entity.Endereco.Ds_Numero);
                cmd.Parameters.AddWithValue("@Ds_Complemento", entity.Endereco.Ds_Complemento);
                cmd.Parameters.AddWithValue("@Ds_Bairro", entity.Endereco.Ds_Bairro);
                cmd.Parameters.AddWithValue("@Ds_Cep", entity.Endereco.Ds_Cep);
                cmd.Parameters.AddWithValue("@Id_Estado", entity.Endereco.Estado.Id_Estado);
                cmd.Parameters.AddWithValue("@Id_Cidade", entity.Endereco.Cidade.Id_Cidade);
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

        public async Task UpdateAsync(FornecedorModel entity)
        {
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_FORNECEDOR_UPD", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id_Pessoa", entity.Id_Fornecedor);
                cmd.Parameters.AddWithValue("@Ds_Fornecedor", entity.Ds_Fornecedor);
                cmd.Parameters.AddWithValue("@Ds_Documento", entity.Ds_Documento);
                cmd.Parameters.AddWithValue("@Id_TipoPessoa", entity.TipoPessoa.Id_TipoPessoa);
                cmd.Parameters.AddWithValue("@Ds_Logradouro", entity.Endereco.Ds_Logradouro);
                cmd.Parameters.AddWithValue("@Ds_Numero", entity.Endereco.Ds_Numero);
                cmd.Parameters.AddWithValue("@Ds_Complemento", entity.Endereco.Ds_Complemento);
                cmd.Parameters.AddWithValue("@Ds_Bairro", entity.Endereco.Ds_Bairro);
                cmd.Parameters.AddWithValue("@Ds_Cep", entity.Endereco.Ds_Cep);
                cmd.Parameters.AddWithValue("@Id_Estado", entity.Endereco.Estado.Id_Estado);
                cmd.Parameters.AddWithValue("@Id_Cidade", entity.Endereco.Cidade.Id_Cidade);
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
                SqlCommand cmd = new("SP_FORNECEDOR_DEL", conn)
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
