
using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.User;
using Kavehnegar.External.Infrastructure;
using Kavehnegar.External.Infrastructure.Repositories;
using Kavehnegar.Shared.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Kavehnegar.External.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // Add services to the container.
            var applicationAssembly = typeof(Kavehnegar.Core.Application.AssemblyReference).Assembly;

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
            builder.Services.AddDbContext<KavehnegarDbContext>(builder =>
            builder.UseNpgsql(configuration["ConnectionStrings:PostgresDb"]));

            builder.Services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.EnsureDatabase();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
