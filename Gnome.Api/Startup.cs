using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Gnome.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("GnomeConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                ));

            ////services.AddControllers().AddNewtonsoftJson();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            //// Configure logging
            //var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            //loggerFactory.AddFile(_configuration["Logging:LogFilePath"]);

            // Swagger configuration
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            // Apply CORS policies
            app.UseCors();

            // Authorization middleware
            app.UseAuthorization();

            // Map controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
