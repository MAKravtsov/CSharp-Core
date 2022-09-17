namespace Library.Data.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
    }
}
