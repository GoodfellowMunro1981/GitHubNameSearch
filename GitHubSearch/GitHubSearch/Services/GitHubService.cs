using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using GitHubSearch.OAuth2Client;
using GitHubSearch.RestClient;
using Newtonsoft.Json;

namespace GitHubSearch
{
    public static class GitHubService
    {
        public static GitHubUser SearchByName(string name, IValidationResultList validationResults)
        {
            var restClientService = new RestClientService();

            try
            {
                var user = GetGitHubNameSearchResults(restClientService, name);

                if (user != null)
                {

                    var responseRepos = GetGitHubUserRepoResponse(restClientService, user.ReposUrl);

                    user.Repos = responseRepos
                                    .OrderByDescending(x => x.StargazersCount)
                                    .Take(5)
                                    .ToList();
                }
                else
                {
                    user.Repos = new List<GitHubUserRepo>();
                }

                return user;
            }
            catch (Exception ex)
            {
                validationResults.Add(new ValidationResult
                {
                    Level = ValidationLevel.Error,
                    Message = "Error searching for name."
                });
            }

            return null;


            //return new GitHubSearchResponse
            //{
            //    Username = name,
            //    Avatar = "https://avatars0.githubusercontent.com/u/53115751?s=460&v=4",
            //    Location = "Newcastle upon Tyne",
            //    Repos = new List<GitHubUserRepo> {new GitHubUserRepo
            //    {
            //        Name = "Test repo",
            //        StarGazerCount = 5,
            //    }}
            //};
        }

        private static GitHubUser GetGitHubNameSearchResults(
            IRestClientService restClientService,
            string name)
        {
            var url = string.Format("http://api.github.com/users/{0}", name);
            //var response = restClientService.Get(url, new Dictionary<string, string>());
            //var responseString = response.Content.ReadAsStringAsync().Result;
            //return JsonConvert.DeserializeObject<GitHubSearchResponse>(responseString);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;

            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {
                        string reader = responseReader.ReadToEnd();
                        return JsonConvert.DeserializeObject<GitHubUser>(reader);
                    }
                }
                catch (Exception ex)
                {
                    // add message to validation Results
                }
            }


            return null;
        }

        private static IEnumerable<GitHubUserRepo> GetGitHubUserRepoResponse(
            IRestClientService restClientService,
            string url)
        {
            //var response = restClientService.Get(url, new Dictionary<string, string>());
            //var responseString = response.Content.ReadAsStringAsync().Result;
            //return JsonConvert.DeserializeObject<GitHubUserRepoResponse>(responseString);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;

            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {
                        string reader = responseReader.ReadToEnd();

                        return JsonConvert.DeserializeObject<List<GitHubUserRepo>>(reader);

                        //return JsonConvert.DeserializeObject<GitHubUserRepoResponse>(reader);
                    }
                }
                catch (Exception ex)
                {
                    // add message to validation Results
                }
            }


            return null;
        }

        #region Authentication
        private static OAuth2Token GetOAuth2Token(
            IOAuth2ClientService oAuth2ClientService,
            string oAuthUrlEndpoint,
            string clientId,
            string clientSecret,
            string grantType)
        {
            var oAuth2Request = new OAuth2Request
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                GrantType = grantType,
            };

            var response = oAuth2ClientService.GetToken(oAuthUrlEndpoint, new Dictionary<string, string>(), oAuth2Request).Result;

            return new OAuth2Token
            {
                TokenType = response.TokenType,
                RefreshToken = response.RefreshToken,
                AccessToken = response.AccessToken,
                ExpiryDateTime = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
            };
        }
        #endregion
    }
}