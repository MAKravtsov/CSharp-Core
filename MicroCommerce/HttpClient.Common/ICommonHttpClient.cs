using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClient.Common
{
    public interface ICommonHttpClient
    {
        public Task<T> GetResponse<T>(HttpMethod httpMethod,
            string url,
            string contentJson = "");
    }
}