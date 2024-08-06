using Microsoft.EntityFrameworkCore;
using chatmobile.services.registerappservce;
using chatmobile.DB;
using chatmobile.repositories;

namespace chatmobile.configurations
{
    public static class AppConfig
    {
        public static void Configure(IServiceCollection Services, IConfiguration configuration)
        {

            // Add the configuration to the builder's services
            Services.AddSingleton<IConfiguration>(configuration);
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Configure DbContext
            Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(); // For debugging
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            // Add services to the container.
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddControllers();
            Services.AddControllersWithViews();
            Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials(); // Allow credentials (cookies, authorization headers, etc.)
                    });
            });

            Services.RegServices();
        }
    }
}