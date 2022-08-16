namespace Application___LFID_WebService_Parser {
    public partial class MainWindow {

        private static readonly HttpClient HttpClient =
            new HttpClient {
                BaseAddress = new Uri("https://localhost:5001/api/")
            };

        public MainWindow() {
            var authorizationServerTokenIssuerUri =
                new Uri("https://localhost:5003/connect/token");
            const string clientId = "ClientIdThatCanOnlyRead";
            const string clientSecret = "secret1";
            const string scope = "scope.readaccess";
            var jwtToken = RequestTokenToAuthorizationServer(
                    authorizationServerTokenIssuerUri,
                    clientId,
                    scope,
                    clientSecret)
                .Result;
            if (jwtToken != null) {
                var rawToken = JObject.Parse(jwtToken)["access_token"];
                HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", rawToken?.ToString());
            } else {
                const string message =
                    "The AuthorizationServer has not been started yet. Make sure the AuthorizationServer is up and running. Please restart the application after you started the AuthorizationServer.";
                const string caption = "AuthorizationServer down.";

                var result = MessageBox.Show(message, caption, MessageBoxButton.OK);
                if (result.Equals(MessageBoxResult.OK)) Close();
            }
        }
    }
}