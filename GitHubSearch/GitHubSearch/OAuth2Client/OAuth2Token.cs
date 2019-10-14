using System;

namespace GitHubSearch.OAuth2Client
{
    public class OAuth2Token
    {
        public string TokenType { get; set; }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }

        public DateTime ExpiryDateTime { get; set; }
    }
}