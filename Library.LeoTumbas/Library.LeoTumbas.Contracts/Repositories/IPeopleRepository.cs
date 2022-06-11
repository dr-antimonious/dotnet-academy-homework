namespace Library.LeoTumbas.Contracts.Repositories
{
    public interface IPeopleRepository<T> : IRepository<T> where T : class
    {
        Task<IList<T>?> GetByCityNameAsync(string city);
    }
}
