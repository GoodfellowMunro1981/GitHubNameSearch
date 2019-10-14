using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitHubSearch.OAuth2Client
{
    public class OAuth2Response
    {
        // <summary>
        /// Token type
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        // <summary>
        /// Refresh token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The access_token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
		/// Time in seconds till the token expires
		/// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
		/// Error from OAuth2
		/// </summary>
        [JsonProperty("error")]
        public ErrorDetails Error { get; set; }
    }

    public class ErrorDetails
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("details")]
        public List<object> Details { get; set; }
    }
}