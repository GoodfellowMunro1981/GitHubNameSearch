using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubSearch.Extensions;
using GitHubSearch.RestClient;
using Newtonsoft.Json;

namespace GitHubSearch.OAuth2Client
{
    public class OAuth2ClientService : IOAuth2ClientService
    {
        private IRestClientService restClient;

        public OAuth2ClientService(IRestClientService restClient)
        {
            this.restClient = new RestClientService();
        }

        public async Task<OAuth2Response> GetToken(string url, IDictionary<string, string> headers, OAuth2Request request)
        {
            var keyValues = request.ToKeyValue();
            var content = new FormUrlEncodedContent(keyValues);
            var response = restClient.Post(url, headers, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OAuth2Response>(responseString);
        }
    }
}