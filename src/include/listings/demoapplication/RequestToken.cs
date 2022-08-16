private static async Task<string> RequestTokenToAuthorizationServer(Uri uriAuthorizationServer, string clientId, string scope, string clientSecret) {
    HttpResponseMessage responseMessage;
    using (var client = new HttpClient()) {
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, uriAuthorizationServer);
        HttpContent httpContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("scope", scope),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            });
        tokenRequest.Content = httpContent;
        try {
            responseMessage = client.SendAsync(tokenRequest).Result;
            return await responseMessage.Content.ReadAsStringAsync();
        } catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }
}