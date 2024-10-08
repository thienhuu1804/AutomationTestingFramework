

namespace APIPractice.Test.Core.API
{
    public class APIClient
    {
        private readonly RestClient _client;
        public RestRequest Request;
        public APIClient(RestClient client)
        {
            _client = client;
            Request = new RestRequest();
        }
        public APIClient(string url)
        {
            _client = new RestClient(url);
            Request = new RestRequest();
        }
        public APIClient SetBasicAuth(string username, string password)
        {
            _client.Authenticator = new HttpBasicAuthenticator(username, password);
            return this;
        }
        public APIClient SetRequestTokenAuthentication(string consumerKey, string consumerSecrect)
        {
            _client.Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecrect);
            return this;
        }
        public APIClient SetAccessTokenAuthentication(string consumerKey, string consumerSecrect, string oauthToken, string oauthTokenSecret)
        {
            _client.Authenticator = OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecrect, oauthToken, oauthTokenSecret);
            return this;
        }
        public APIClient SetRequestHeaderAuthentication(string token, string authType = "Bearer")
        {
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, authType);
            return this;
        }
        public APIClient SetJwtAuthenticator(string token)
        {
            _client.Authenticator = new JwtAuthenticator(token);
            return this;
        }
        public APIClient ClearAuthencator()
        {
            _client.Authenticator = null;
            return this;
        }
        public APIClient AddDefaultHeaders(Dictionary<string, string> headers)
        {
            _client.AddDefaultHeaders(headers);
            return this;
        }
        public APIClient CreateRequest(string source = "")
        {
            Request = new RestRequest(source);
            return this;
        }
        public APIClient AddHeader(string name, string value)
        {
            Request.AddHeader(name, value);
            return this;
        }
        public APIClient AddAuthorizationHeader(string name, string value)
        {
            Request.AddHeader("Authorization", value);
            return this;
        }
        public APIClient AddContentTypeHeader(string value = ContentType.Json)
        {
            Request.AddHeader("Content-Type", value);
            return this;
        }
        public APIClient AddParameter(string name, string value)
        {
            Request.AddParameter(name, value);
            return this;
        }
        public APIClient AddBody(object obj, string contentType = ContentType.Json)
        {
            string json = JsonConvert.SerializeObject(obj);
            Request.AddStringBody(json, contentType);
            return this;
        }
        public async Task<RestResponse<T>> ExecuteGetAsync<T>()
        {
            return await _client.ExecuteGetAsync<T>(Request);
        }
        public async Task<RestResponse> ExecuteGetAsync()
        {
            return await _client.ExecuteGetAsync(Request);
        }
        public async Task<RestResponse<T>> ExecutePostAsync<T>()
        {
            return await _client.ExecutePostAsync<T>(Request);
        }
        public async Task<RestResponse> ExecutePostAsync()
        {
            return await _client.ExecutePostAsync(Request);
        }
        public async Task<RestResponse> ExecuteDeleteAsync()
        {
            Request.Method = Method.Delete;
            return await _client.ExecuteAsync(Request);
        }
        public async Task<RestResponse> ExecuteAsync()
        {
            return await _client.ExecuteAsync(Request);
        }
        public async Task<RestResponse> ExecutePutAsync<T>()
        {
            return await _client.ExecutePutAsync<T>(Request);
        }
    }

}