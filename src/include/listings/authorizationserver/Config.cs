namespace AuthorizationServer {
    public class Config {
        public static IEnumerable<ApiResource> GetApiResources() {
            return new List<ApiResource> {
                new ApiResource("scope.readaccess", "LFID API"),
                new ApiResource("scope.fullaccess", "LFID API")
            };
        }

        public static IEnumerable<Client> GetClients() {
            return new List<Client> {
                new Client {
                    ClientId = "ClientIdThatCanOnlyRead",
                    AllowGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret1".Sha256()) },
                    AllowedScopes = {"scope.readaccess"}
                },
                new Client {
                    ClientId = "ClientIdWithWriteAccess",
                    AllowGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret1".Sha256()) },
                    AllowedScopes = {"scope.writeaccess"}
                }
            };
        }
    }
}