public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

    if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
    else
        app.UseHsts();

    app.UseExceptionHandler(c => c.Run(async context => {
        var exception = context.Features
                            .Get<IExceptionHandlerPathFeature>()
                            .Error;
        string response;

        switch (exception)  {
            case ArgumentOutOfRangeException _:
                response = JsonConvert.SerializeObject(new {error = "Argument/s is/are out of range!"});
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case DataNotFoundException dataNotFoundException:
                response = JsonConvert.SerializeObject(new {dataNotFoundException.Message});
                context.Response.StatusCode = (int) dataNotFoundException.StatusCode;
                break;
            default:
                response = JsonConvert.SerializeObject(new {error = "Exception Handling not defined!"});
                context.Response.StatusCode = StatusCodes.Status501NotImplemented;
                break;
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(response);
    }));

    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        });

    app.UseStaticFiles();
    app.UseAuthentication();
    app.UseStatusCodePages();
    app.UseHttpsRedirection();
    app.UseMvc();
    }
}