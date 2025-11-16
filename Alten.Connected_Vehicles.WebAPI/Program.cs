using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Alten.Connected_vehicle.Model;

namespace Alten.Connected_Vehicles.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers()
                .AddNewtonsoftJson(); // For compatibility with existing JSON serialization

            // Add CORS support
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Add Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext - configure connection string in appsettings.json
            builder.Services.AddDbContext<Connected_Vehicles_Models>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Connected_Vehicles_Models")
                    ?? "Server=(localdb)\\mssqllocaldb;Database=ConnectedVehicles;Trusted_Connection=True;MultipleActiveResultSets=true"));

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
