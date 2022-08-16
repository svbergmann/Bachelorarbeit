namespace AuthorizationServer {
    public class Program {
        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).
                    .ConfigureLogging(config =>
                    {
                        config.AddDebug();
                        config.AddConsole();
                    })
                    .UseStartup<Startup>()
                    .UseUrls("https://*:5003");
    }
}