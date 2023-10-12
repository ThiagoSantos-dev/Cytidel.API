using Cytidel.Application;
using Cytidel.Application.Services;
using Cytidel.Core.Repositories;
using Cytidel.Infrastructure.Auth;
using Cytidel.Infrastructure.Context;
using Cytidel.Infrastructure.Exceptions;
using Cytidel.Infrastructure.Mongo.Documents;
using Cytidel.Infrastructure.Mongo.Repositories;
using Cytidel.Infrastructure.Services;
using Cytidel.Infrastructure.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Omatka;
using Omatka.Auth;
using Omatka.CQRS.Queries;
using Omatka.Docs.Swagger;
using Omatka.HTTP;
using Omatka.MessageBrokers;
using Omatka.MessageBrokers.RabbitMQ;
using Omatka.Metrics.AppMetrics;
using Omatka.Persistence.MongoDB;
using Omatka.Security;
using Omatka.WebApi;
using Omatka.WebApi.CQRS;
using Omatka.WebApi.Swagger;
using Swashbuckle.AspNetCore.ReDoc;
using System.Text;
using SecurityKey = Cytidel.Infrastructure.Types.SecurityKey;

namespace Cytidel.Infrastructure;

public static class Extensions
{
    public static IOmatkaBuilder AddInfrastructure(this IOmatkaBuilder builder)
    {
        //Register Services on the API
        var requestsOptions = builder.GetOptions<RequestsOptions>("requests");
        var securityKey = builder.GetOptions<SecurityKey>("securityKey");
        builder.Services.AddSingleton(requestsOptions);
        builder.Services.AddSingleton(securityKey);
        builder.Services.AddTransient<IUserRepository,UserRepository>();
        builder.Services.AddTransient<ITaskRepository,TaskRepository>();
        builder.Services.AddTransient<IIdentityService, IdentityService>();
        builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
        builder.Services.AddSingleton<IPasswordService, PasswordService>();
        builder.Services.AddSingleton<IPasswordHasher<IPasswordService>, PasswordHasher<IPasswordService>>();
        builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
        builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((host) => true));
        });
        //adding the services to the context
        return builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddJwt()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddSignalR()
                .AddMetrics()
                .AddMongoRepository<ToDoTaskDocument, Guid>("tasks")
                .AddMongoRepository<UserDocument, Guid>("users")
                .AddWebApiSwaggerDocs()
                .AddSecurity();
    }
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        //enable the services
        app
            .UseOmatka()
            .UseCors("CorsPolicy")
            .UseSwaggerDocs()
            .UseAccessTokenValidator()
            .UseMetrics()
            .UsePublicContracts<ContractAttribute>()
            .UseAuthentication()
            .UseAuthorization();
        return app;
    }
    //add signalR
    internal static IOmatkaBuilder AddSignalR(this IOmatkaBuilder builder)
    {
        var options = builder.GetOptions<SignalrOptions>("signalR");
        builder.Services.AddSingleton(options);
        var signalR = builder.Services.AddSignalR(options =>
        {
            options.KeepAliveInterval = TimeSpan.FromMilliseconds(1200000);
            options.ClientTimeoutInterval = TimeSpan.FromMilliseconds(900000);
            options.EnableDetailedErrors = true;
        });
        return builder;
    }
    //Get the correlation Context from the request or message
    internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)

#pragma warning disable CS8604 // Possible null reference argument.
       => accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
           ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
           : null;
#pragma warning restore CS8604 // Possible null reference argument.
    public static async Task<Guid> AuthenticateUsingJwtAsync(this HttpContext context)
    {
        var authentication = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

        return authentication.Succeeded ? Guid.Parse(authentication.Principal.Identity.Name) : Guid.Empty;
    }
    // retrive the message id to add in future actions.
    internal static string GetSpanContext(this IMessageProperties messageProperties, string header)
    {
        if (messageProperties is null)
        {
            return string.Empty;
        }

        if (messageProperties.Headers.TryGetValue(header, out var span) && span is byte[] spanBytes)
        {
            return Encoding.UTF8.GetString(spanBytes);
        }

        return string.Empty;
    }

}
