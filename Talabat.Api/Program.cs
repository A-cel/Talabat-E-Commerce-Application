using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json.Serialization;
using Talabat.Api.Extensions;
using Talabat.Api.Helpers;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Data_Seeding;
using Talabat.Repository.Identity;
using Talabat.Repository.Repositories;
using Talabat.Services;

namespace Talabat.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
                /*.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)*/
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
            {
                var conn = builder.Configuration.GetConnectionString("redis");
                return ConnectionMultiplexer.Connect(conn);
            });
            builder.Services.AddCors(o => o.AddPolicy("mypolicy", options => { options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"); }));
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();
         using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbcontext = services.GetRequiredService<StoreContext>();
            var identitydbcontext = services.GetRequiredService<AppIdentityDbContext>();
            var loggerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await dbcontext.Database.MigrateAsync();
                await SeedingData.SeedData(dbcontext);
                await identitydbcontext.Database.MigrateAsync();
                var user = services.GetRequiredService<UserManager<AppUser>>();
                await IdentityDataSeed.IdentitySeedAsync(user);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
            }
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("mypolicy");
            app.UseStaticFiles();

            app.MapControllers();
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.Run();
        }
    }
}
