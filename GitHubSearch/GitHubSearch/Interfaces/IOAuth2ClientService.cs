using System.Collections.Generic;
using System.Threading.Tasks;
using GitHubSearch.OAuth2Client;

namespace GitHubSearch
{
    public interface IOAuth2ClientService
    {
        Task<OAuth2Response> GetToken(string url, IDictionary<string, string> headers, OAuth2Request request);
    }
}