using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class FilialRepository : IFilialRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;        
        public FilialRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<FilialModel>> GetAllAsync()
        {
            List<FilialModel> filiais = new ();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_FILIAL", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    filiais.Add(new FilialModel()
                    {
                        Id_Filial = Convert.ToInt32(reader["Id_Filial"]),
                        Ds_Filial = reader["Ds_Filial"].ToString().ToUpper(),
                        Cm_Certificado = reader["Cm_Certificado"].ToString().Trim(),
                        Dt_Abertura = Convert.ToDateTime(reader["Dt_Abertura"]),

                        TipoPessoa = new TipoPessoaModel()
                        {
                            Ds_TipoPessoa = reader["Ds_TipoPessoa"].ToString().ToUpper()
                        },                        

                        PessoaDocumento = new PessoaModel()
                        {
                            Ds_Pessoa = reader["Ds_Pessoa"].ToString().ToUpper(),
                            Ds_Documento = reader["Ds_Documento"].ToString().Trim(),
                            Ds_InscricaoEstadual = reader["Ds_InscricaoEstadual"].ToString().Trim(),
                        },

                        Endereco = new EnderecoModel()
                        {
                            Ds_Logradouro = reader["Ds_Logradouro"].ToString().ToUpper(),
                            Ds_Complemento = reader["Ds_Complemento"].ToString().ToUpper(),
                            Ds_Bairro = reader["Ds_Bairro"].ToString().ToUpper(),
                            Ds_Cep = reader["Ds_Cep"].ToString().Trim(),
                            Ds_Numero = reader["Ds_Numero"].ToString().Trim(),

                            Cidade = new CidadeModel()
                            {
                                Ds_Cidade = reader["Ds_Cidade"].ToString().ToUpper().Trim(),

                                Estado = new EstadoModel()
                                {
                                    Cd_Estado = reader["Cd_Estado"].ToString().ToUpper()
                                }

                            }
                        },

                        Contato = new ContatoModel()
                        {
                            Ds_Celular = reader["Ds_Celular"].ToString(),
                            Ds_Telefone = reader["Ds_Telefone"].ToString(),
                        }

                    });
                }

                return filiais;
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

        public async Task<FilialModel> GetByIdAsync(int id)
        {
            FilialModel filialById = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {

                SqlCommand cmd = new("SELECT * FROM VW_FILIAL WHERE ID_FILIAL = " + id, conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    FilialModel filial = new()
                    {

                        TipoPessoa = new TipoPessoaModel()
                        {
                            Id_TipoPessoa = Convert.ToInt32(reader["Id_TipoPessoa"]),
                            Ds_TipoPessoa = reader["Ds_TipoPessoa"].ToString().ToUpper()
                        },

                        Id_Filial = Convert.ToInt32(reader["Id_Filial"]),
                        Ds_Filial = reader["Ds_Filial"].ToString().ToUpper(),
                        Cm_Certificado = reader["Cm_Certificado"].ToString().Trim(),
                        Dt_Abertura = Convert.ToDateTime(reader["Dt_Abertura"]),

                        PessoaDocumento = new PessoaModel()
                        {
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

                    filialById = filial;
                }

                return filialById;
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

        public async Task AddAsync(FilialModel entity)
        {
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_FILIAL_INS", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id_Filial", entity.Id_Filial);
                cmd.Parameters.AddWithValue("@Ds_Filial", entity.Ds_Filial);
                cmd.Parameters.AddWithValue("@Dt_Abertura", entity.Dt_Abertura);
                cmd.Parameters.AddWithValue("@Cm_Certificado", entity.Cm_Certificado);               

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

        public Task UpdateAsync(FilialModel entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
