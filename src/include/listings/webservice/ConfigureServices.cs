public void ConfigureServices(IServiceCollection services) {
    services.AddLogging(config => {
        config.AddDebug();
        config.AddConsole();
    });

    services.AddSingleton(new MapperConfiguration(mc =>
        { mc.AddProfile(new AutoMapping()); })
        .CreateMapper());

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    services.AddSingleton<IAuthorizationHandler, ApiHandler>();

    services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer("SchemeReadAccess", options => {
        options.Authority = "https://localhost:5003";
        options.Audience = "scope.readaccess";
        options.RequireHttpsMetadata = false;
    }).AddJwtBearer("SchemeWriteAccess", options => {
        options.Authority = "https://localhost:5003";
        options.Audience = "scope.writeaccess";
        options.RequireHttpsMetadata = false;
    });

    services.AddAuthorization(options => {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes("SchemeReadAccess", "SchemeWriteAccess")
            .Build();
        options.AddPolicy("CustomPolicy", policy => { policy.AddRequirements(new ApiRequirement()); });
    });

    services.AddSwaggerGen(options => {
        var securityScheme = new OpenApiSecurityScheme {
            Name = "JWT Authentication",
            Description = "Enter JWT Bearer token **_only_**",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {securityScheme, new string[] { }}});

        options.SwaggerDoc("v1", new OpenApiInfo {
            Version = "v1",
            Title = "LFID Web Service",
            Description = "A .Net Core 2.1 Web API for managing the LFID database.",
            Contact = new OpenApiContact { Name = "Sven Bergmann", Email = "sven.bergmann@cae.de" }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

    services.AddDbContext<LfidContext>(options => {
        options.UseSqlServer(Configuration["ConnectionStrings:lfidDatabase"]);
    });
}