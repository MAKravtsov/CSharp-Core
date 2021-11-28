#nullable enable
using Redis.Extensions;
using StackExchange.Redis;

namespace Redis.Repositories
{
    public class BaseRepository
    {
        
        protected IDatabase RedisRepo { get; private set; } = null!;

        protected string RepositoryName { get; }

        private ConnectionMultiplexer Connection { get; set; } = null!;

        protected BaseRepository(RepositoriesEnum repositoryEnum)
        {
            RepositoryName = repositoryEnum.GetEnumDescription();
            Connect(repositoryEnum);
        }

        private void Connect(RepositoriesEnum repositoryEnum)
        {
#if DEBUG
            Connection = ConnectionMultiplexer.Connect("localhost:6379");
#else
            Connection = ConnectionMultiplexer.Connect("Redis");
#endif

            var repositoryNum = (int) repositoryEnum;
            RedisRepo = Connection.GetDatabase(repositoryNum);
        }

        ~BaseRepository()
        {
            Connection?.Dispose();
        }
    }
}