using LibraryApp.Data;
using LibraryApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
namespace LibraryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("LibraryAppContextConnection")
            ?? throw new InvalidOperationException("Connection string 'LibraryAppContextConnection' not found."); ;

            builder.Services.AddDbContext<LibraryAppContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LibraryAppContext>();

            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped<BookService>();
            builder.Services.AddScoped<BorrowerService>();
            builder.Services.AddScoped<ReservationService>();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowNetlify", policy =>
                {
                    policy
                        .WithOrigins("https://library-manager-app.netlify.app/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors("AllowNetlify");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}