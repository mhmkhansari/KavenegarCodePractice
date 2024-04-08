
using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.User;
using Kavehnegar.External.Infrastructure;
using Kavehnegar.External.Infrastructure.MessageBroker;
using Kavehnegar.External.Infrastructure.Repositories;
using Kavehnegar.Shared.Framework.Infrastructure;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using Kavehnegar.Shared.Framework.Application;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;

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

            builder.Services.Configure<MessageBrokerSettings>(
                builder.Configuration.GetSection("MessageBroker"));
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddBearerToken(IdentityConstants.BearerScheme);
            builder.Services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<KavehnegarDbContext>()
                .AddApiEndpoints();
                
            builder.Services.AddSingleton(sp => 
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
            builder.Services.AddOpenTelemetry()
                .WithMetrics(metric =>
                {
                    metric.AddMeter("Microsoft.AspNetCore.Hosting");
                    metric.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
                    metric.AddMeter("System.Net.Http");
                    metric.AddPrometheusExporter();
                });
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddOpenTelemetry(options =>
                {
                    options.AddOtlpExporter();
                });
            });
            builder.Logging.AddOpenTelemetry(options =>
            {
                options.AddOtlpExporter();
            });
            builder.Services.AddMassTransit(options =>
            {
                options.SetKebabCaseEndpointNameFormatter();
                options.UsingRabbitMq((context, configurator) =>
                {
                    MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                });
            });
            builder.Services.AddStackExchangeRedisCache(redisOptions =>
                redisOptions.Configuration = configuration["ConnectionStrings:RedisCache"]);
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
            builder.Services.AddDbContext<KavehnegarDbContext>(builder =>
            builder.UseNpgsql(configuration["ConnectionStrings:PostgresDb"]));

            builder.Services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            builder.Services.AddTransient<IEventBus, EventBus>();
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
                app.MapIdentityApi<User>();
            }
            app.EnsureDatabase();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapPrometheusScrapingEndpoint();
            app.Run();
        }
    }
}
