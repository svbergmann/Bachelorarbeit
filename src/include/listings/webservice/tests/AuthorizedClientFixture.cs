namespace WebService.Tests {
    public class AuthorizedClientFixture : IDisposable {
        public AuthorizedClientFixture() {
            Client = new HttpClient {
                BaseAddress = new Uri("https://localhost:5001/api/")
            };
            var authorizationServerTokenIssuerUri =
                new Uri("https://localhost:5003/connect/token");
            const string clientId = "ClientIdThatCanOnlyRead";
            const string scope = "scope.readaccess";
            const string clientSecret = "secret1";
            var jwtToken = RequestTokenToAuthorizationServer(
                authorizationServerTokenIssuerUri,
                clientId, scope, clientSecret).Result;
            var rawToken = JObject.Parse(jwtToken)["accessToken"];
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", rawToken?.ToString());
        }

        public HttpClient Client { get; }

        public void Dispose() { Client.Dispose(); }

        private async Task<string> RequestTokenToAuthorizationServer( Uri uriAuthorizationServer, string clientId, string scope, string clientSecret) {
            HttpResponseMessage responseMessage;
            using(var client = new HttpClient()) {
                var tokenRequest = new HttpRequestMessage(HttpMethod.Post, uriAuthorizationServer);
                HttpContent httpContent = new FormUrlEncodedContent(new[] {
                        new KeyValuePair<string, string>("grant_type",
                            "client_credentials"),
                        new KeyValuePair<string, string>("clientId", clientId),
                        new KeyValuePair<string, string>("scope", scope),
                        new KeyValuePair<string, string>("clientSecret", clientSecret)
                    });
                tokenRequest.Content = httpContent;
                responseMessage = client.SendAsync(tokenRequest).Result;
            }
        return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}