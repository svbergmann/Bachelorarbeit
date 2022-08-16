namespace WebService.Test {
    public class CountriesSqlTest {
        private const string CONNECTION_STRING = "...";
        private const string SQL_QUERY = "SELECT * FROM [lfid].[dbo].[country]";
        private readonly ITestOutputHelper _testOutputHelper;

        public CountriesSqlTest(ITestOutputHelper testOutputHelper) {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [Repeat(10)]
        public void Should_return_all_countries_from_Database() {
            var t0 = DateTime.Now;
            _testOutputHelper.WriteLine($"Started request with query at: {t0}");
            using(var con = new SqlConnection(CONNECTION_STRING)) {
                var command = new SqlCommand(SQL_QUERY, con);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                command.Connection.Close();
            }
            var t1 = DateTime.Now;
            _testOutputHelper.WriteLine($"Received countries in:
                {(t1-t0).Milliseconds} milliseconds.");
        }
    }
}
