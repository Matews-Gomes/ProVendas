using ProVendas.Domain.Models;

namespace ProVendas.Domain.IRepository
{
    public interface  IEnderecoRepository : IGenericRepository<EnderecoModel>
    {
        public async Task AddAsync(EnderecoModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EnderecoModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<EnderecoModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(EnderecoModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
