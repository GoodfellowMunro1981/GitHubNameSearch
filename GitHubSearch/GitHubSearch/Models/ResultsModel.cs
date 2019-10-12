using System.Collections.Generic;

namespace GitHubSearch
{
    public class ResultsModel
    {
        public string Username { get; set; }

        public string Location { get; set; }

        public string Avatar { get; set; }

        public IEnumerable<GitHubRepo> Repos { get; set; }
    }
}