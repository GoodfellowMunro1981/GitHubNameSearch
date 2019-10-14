using System.Collections.Generic;

namespace GitHubSearch
{
    public class GitHubUserRepoResponse
    {
        public IEnumerable<GitHubUserRepo> Repos { get; set; }
    }
}