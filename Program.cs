using chatmobile.configurations;
using chatmobile.MiddleWares;

var builder = WebApplication.CreateBuilder(args);



var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


AppConfig.Configure(builder.Services, configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
        options.DisplayRequestDuration();
    });
}

// Use routing middleware.
app.UseRouting();

// Configure authorization middleware if needed.
// app.UseAuthorization(); // Uncomment if you have authorization policies.

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Map controllers to the route.
app.MapControllers();
app.UseCors("AllowSpecificOrigin");

app.Run();
