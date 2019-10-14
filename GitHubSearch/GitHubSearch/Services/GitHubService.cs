using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace GitHubSearch
{
    public static class GitHubService
    {
        #region Public Methods
        public static GitHubUser SearchByName(string name, IValidationResultList validationResults)
        {
            try
            {
                var user = GetGitHubNameSearchResults(name, validationResults);

                if (user != null)
                {

                    var responseRepos = GetGitHubUserRepoResponse(user.ReposUrl, validationResults);

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
        }
        #endregion

        #region Private Helpers
        private static GitHubUser GetGitHubNameSearchResults(string name, IValidationResultList validationResults)
        {
            var url = string.Format("http://api.github.com/users/{0}", name);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            if (WebRequest.Create(url) is HttpWebRequest webRequest)
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
                    validationResults.Add(new ValidationResult
                    {
                        Level = ValidationLevel.Error,
                        Message = "Error searching for name."
                    });
                }
            }

            return null;
        }

        private static IEnumerable<GitHubUserRepo> GetGitHubUserRepoResponse(string url, IValidationResultList validationResults)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            if (WebRequest.Create(url) is HttpWebRequest webRequest)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {
                        var reader = responseReader.ReadToEnd();
                        return JsonConvert.DeserializeObject<List<GitHubUserRepo>>(reader);
                    }
                }
                catch (Exception ex)
                {
                    validationResults.Add(new ValidationResult
                    {
                        Level = ValidationLevel.Error,
                        Message = "Error searching for name."
                    });
                }
            }

            return null;
        }
        #endregion
    }
}