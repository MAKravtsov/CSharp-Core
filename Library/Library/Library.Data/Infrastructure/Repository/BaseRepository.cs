using Microsoft.EntityFrameworkCore;

namespace Library.Data.Infrastructure.Repository
{
    public abstract class BaseRepository<T> where T: DbContext
    {
        protected readonly T Context;

        protected BaseRepository(T context)
        {
            Context = context;
        }
    }
}
