using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubSearch.RestClient
{
    public class RestClientService : IRestClientService
    {
        private static HttpClient client = new HttpClient();

        #region Synchronous Methods
        public HttpResponseMessage Get(string url, IDictionary<string, string> headers)
        {
            return ExecuteRequest(url, headers, null, HttpMethod.Get);
        }

        public HttpResponseMessage Post(string url, IDictionary<string, string> headers, HttpContent content)
        {
            return ExecuteRequest(url, headers, content, HttpMethod.Post);
        }

        public HttpResponseMessage Put(string url, IDictionary<string, string> headers, HttpContent content)
        {
            return ExecuteRequest(url, headers, content, HttpMethod.Put);
        }

        public HttpResponseMessage Patch(string url, IDictionary<string, string> headers, HttpContent content)
        {
            return ExecuteRequest(url, headers, content, new HttpMethod("PATCH"));
        }

        public HttpResponseMessage Delete(string url, IDictionary<string, string> headers)
        {
            return ExecuteRequest(url, headers, null, HttpMethod.Delete);
        }

        private HttpResponseMessage ExecuteRequest(string url, IDictionary<string, string> headers, HttpContent content, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11;
            //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            foreach (var item in headers)
            {
                request.Headers.TryAddWithoutValidation(item.Key, item.Value);
            }

            if (content != null)
            {
                request.Content = content;
            }

            System.Diagnostics.Debug.WriteLine(request);

            try
            {
                return client.SendAsync(request).Result;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("REST call failed", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("REST call failed", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("REST call failed", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("REST call failed", ex);
            }
        }

        #endregion

        #region Asynchronous 
        public async Task<HttpResponseMessage> GetAsync(string url, IDictionary<string, string> headers)
        {
            return await ExecuteRequestAsync(url, headers, null, HttpMethod.Get);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, IDictionary<string, string> headers, HttpContent content)
        {
            return await ExecuteRequestAsync(url, headers, content, HttpMethod.Post);
        }

        public async Task<HttpResponseMessage> PutAsync(string url, IDictionary<string, string> headers, HttpContent content)
        {
            return await ExecuteRequestAsync(url, headers, content, HttpMethod.Put);
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, IDictionary<string, string> headers, HttpContent content)
        {
            return await ExecuteRequestAsync(url, headers, content, new HttpMethod("PATCH"));
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, IDictionary<string, string> headers)
        {
            return await ExecuteRequestAsync(url, headers, null, HttpMethod.Delete);
        }

        private async Task<HttpResponseMessage> ExecuteRequestAsync(string url, IDictionary<string, string> headers, HttpContent content, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11;
            // ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            foreach (var item in headers)
            {
                request.Headers.TryAddWithoutValidation(item.Key, item.Value);
            }

            if (content != null)
            {
                request.Content = content;
            }

            System.Diagnostics.Debug.WriteLine(request);

            try
            {
                return await client.SendAsync(request).ConfigureAwait(false);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("REST call failed", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("REST call failed", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("REST call failed", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("REST call failed", ex);
            }
        }
        #endregion
    }
}