namespace WebService.Test {
    public class CountriesWebTestAuthorized : IClassFixture<AuthorizedClientFixture> {
        private readonly AuthorizedClientFixture _authorizedClientFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public CountriesWebTestAuthorized(ITestOutputHelper testOutputHelper,
            AuthorizedClientFixture authorizedClientFixture) {
            _testOutputHelper = testOutputHelper;
            _authorizedClientFixture = authorizedClientFixture;
        }

        [Theory]
        [Repeat(10)]
        public void Should_return_all_countries_with_Authentication() {
            var t0 = DateTime.Now;
            _testOutputHelper.WriteLine($"Started request with
                authentication at: {t0}");
            var countries = GetCountryModels(_authorizedClientFixture.Client,
                "Countries/full").Result;
            var t1 = DateTime.Now;
            if (countries.Count != 0)
                _testOutputHelper.WriteLine($"Received countries in:
                    {(t1-t0).Milliseconds} milliseconds.");
            Assert.NotEmpty(countries);
        }

        public static async Task<List<CountryModel>> GetCountryModels(
            HttpClient httpClient, string uri) {
            var response = await httpClient.GetAsync(uri);
            if (response.isSuccessStatusCode)
                return JsonConvert
                    .DeserializeObject<List<CountryModel>>(
                    response.Content.ReadAsStringAsync().Result);
            return null;
        }
    }
}
