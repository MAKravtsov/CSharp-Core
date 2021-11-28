using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient.Common
{
    public class CommonHttpClient : ICommonHttpClient
    {
        public async Task<T> GetResponse<T>(HttpMethod httpMethod,
            string url,
            string contentJson = "")
        {
            using var client = new System.Net.Http.HttpClient();
            using var request = new HttpRequestMessage(httpMethod, $"{url}")
            {
                Content = new StringContent(contentJson, Encoding.UTF8, "application/json"),
            };
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<T>();

            return result;
        }
    }
}