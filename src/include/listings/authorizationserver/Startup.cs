namespace AuthorizationServer {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.isDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.useHsts();
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseMvc(routes => {
                routes.MapRoute("default",
                    "{controller=Home}/{action=index}/{id?}");
            });
        }
    }
}