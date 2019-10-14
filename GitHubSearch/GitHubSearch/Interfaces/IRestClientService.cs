using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubSearch
{
    public interface IRestClientService
    {
        HttpResponseMessage Get(string url, IDictionary<string, string> headers);

        HttpResponseMessage Post(string url, IDictionary<string, string> headers, HttpContent content);

        HttpResponseMessage Put(string url, IDictionary<string, string> headers, HttpContent content);

        HttpResponseMessage Delete(string url, IDictionary<string, string> headers);

        HttpResponseMessage Patch(string url, IDictionary<string, string> headers, HttpContent content);

        Task<HttpResponseMessage> GetAsync(string url, IDictionary<string, string> headers);

        Task<HttpResponseMessage> PostAsync(string url, IDictionary<string, string> headers, HttpContent content);

        Task<HttpResponseMessage> PutAsync(string url, IDictionary<string, string> headers, HttpContent content);

        Task<HttpResponseMessage> DeleteAsync(string url, IDictionary<string, string> headers);

        Task<HttpResponseMessage> PatchAsync(string url, IDictionary<string, string> headers, HttpContent content);
    }
}