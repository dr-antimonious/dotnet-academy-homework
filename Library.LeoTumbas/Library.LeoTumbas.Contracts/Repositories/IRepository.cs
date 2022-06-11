namespace Library.LeoTumbas.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task Create(T entity);
        Task Update(T entity);
    }
}
