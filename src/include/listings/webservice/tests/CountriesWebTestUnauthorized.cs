namespace WebService.Test {
    public class CountriesWebTestUnauthorized {
        protected static readonly HttpClient Client = new HttpClient {
            BaseAdress = new Uri("https://localhost:5001/api/")
        };
        private readonly ITestOutputHelper _testOutputHelper;

        public CountriesWebTestUnauthorized(ITestOutputHelper testOutputHelper) {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [Repeat(10)]
        public void Should_return_all_countries() {
            var t0 = DateTime.Now;
            _testOutputHelper.WriteLine($"Started request with
                authentication at: {t0}");
            var countries = CountriesWebTestAuthorized
                .GetCountryModels(_authorizedClientFixture.Client,
                    "Countries/fullAnonymous").Result;
            var t1 = DateTime.Now;
            if (countries.Count != 0)
                _testOutputHelper.WriteLine($"Received countries in:
                    {(t1-t0).Milliseconds} milliseconds.");
            Assert.NotEmpty(countries);
        }
    }
}