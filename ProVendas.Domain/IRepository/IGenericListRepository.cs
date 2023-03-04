namespace ProVendas.Domain.IRepository
{
    public interface IGenericListRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
