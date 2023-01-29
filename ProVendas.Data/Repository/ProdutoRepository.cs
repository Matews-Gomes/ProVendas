using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        public string Connection { get; set; }
        public IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ProdutoModel>> GetAllAsync()
        {
            List<ProdutoModel> produtos = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_PRODUTO", conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read()) 
                {
                    produtos.Add(new ProdutoModel()
                    {
                        Id_Produto = Convert.ToInt32(reader["Id_Produto"]),
                        Ds_Produto = reader["Ds_Produto"].ToString().ToUpper(),
                        Ds_Descricao = reader["Ds_Descricao"].ToString().ToUpper(),
                        Ds_Imagem = reader["Ds_Imagem"].ToString().ToUpper(),                                                                      
                        Vl_PrecoCusto = Convert.ToDecimal(reader["Vl_PrecoCusto"]),
                        Vl_PrecoVenda = Convert.ToDecimal(reader["Vl_PrecoVenda"]),
                        Id_Fornecedor = Convert.ToInt32(reader["Id_Fornecedor"]),
                        Ds_Fornecedor = reader["Ds_Fornecedor"].ToString().ToUpper(),
                        Id_Categoria = Convert.ToInt32(reader["Id_Categoria"]),
                        Ds_Categoria = reader["Ds_Categoria"].ToString().ToUpper()
                    });
                }

                return produtos;
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
        public async Task<ProdutoModel> GetByIdAsync(int id)
        {
            ProdutoModel produtoById = new();

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SELECT * FROM VW_PRODUTO WHERE ID_PRODUTO = " + id, conn)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    ProdutoModel produto = new()
                    {
                        Id_Produto = Convert.ToInt32(reader["Id_Produto"]),
                        Ds_Produto = reader["Ds_Produto"].ToString().ToUpper(),
                        Ds_Descricao = reader["Ds_Descricao"].ToString().ToUpper(),
                        Ds_Imagem = reader["Ds_Imagem"].ToString().ToUpper(),
                        Vl_PrecoCusto = Convert.ToDecimal(reader["Vl_PrecoCusto"]),
                        Vl_PrecoVenda = Convert.ToDecimal(reader["Vl_PrecoVenda"]),
                        Id_Fornecedor = Convert.ToInt32(reader["Id_Fornecedor"]),
                        Ds_Fornecedor = reader["Ds_Fornecedor"].ToString().ToUpper(),
                        Id_Categoria = Convert.ToInt32(reader["Id_Categoria"]),
                        Ds_Categoria = reader["Ds_Categoria"].ToString().ToUpper()
                    };

                    produtoById = produto;
                }

                return produtoById;
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
        public async Task AddAsync(ProdutoModel entity)
        {

            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_PRODUTO_INS", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Ds_Produto", entity.Ds_Produto);
                cmd.Parameters.AddWithValue("@Ds_Descricao", entity.Ds_Descricao);
                cmd.Parameters.AddWithValue("@Ds_Imagem", entity.Ds_Imagem);
                cmd.Parameters.AddWithValue("@Vl_PrecoCusto", entity.Vl_PrecoCusto);
                cmd.Parameters.AddWithValue("@Vl_PrecoVenda", entity.Vl_PrecoVenda);
                cmd.Parameters.AddWithValue("@Id_Fornecedor", entity.Id_Fornecedor);
                cmd.Parameters.AddWithValue("@Id_Categoria", entity.Id_Categoria);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar adicionar o registro: " + ex.Message);
            }
            finally 
            { 
                conn.Close(); 
            }
        }
        public async Task UpdateAsync(ProdutoModel entity)
        {
          
            using SqlConnection conn = new(Connection);
            conn.Open();

            try
            {
                SqlCommand cmd = new("SP_PRODUTO_UPD", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id_Produto", entity.Id_Produto);
                cmd.Parameters.AddWithValue("@Ds_Produto", entity.Ds_Produto);
                cmd.Parameters.AddWithValue("@Ds_Descricao", entity.Ds_Descricao);
                cmd.Parameters.AddWithValue("@Ds_Imagem", entity.Ds_Imagem);
                cmd.Parameters.AddWithValue("@Vl_PrecoCusto", entity.Vl_PrecoCusto);
                cmd.Parameters.AddWithValue("@Vl_PrecoVenda", entity.Vl_PrecoVenda);
                cmd.Parameters.AddWithValue("@Id_Fornecedor", entity.Id_Fornecedor);
                cmd.Parameters.AddWithValue("@Id_Categoria", entity.Id_Categoria);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar atualizar registro: " + ex.Message);
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
                SqlCommand cmd = new("SP_PRODUTO_DEL", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id_Produto", id);

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
