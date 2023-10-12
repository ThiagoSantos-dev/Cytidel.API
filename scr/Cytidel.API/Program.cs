
using Cytidel.Application;
using Cytidel.Application.Commands;
using Cytidel.Application.Dtos;
using Cytidel.Application.Hubs;
using Cytidel.Application.Queries;
using Cytidel.Application.Services;
using Cytidel.Core.Repositories;
using Cytidel.Infrastructure;
using Microsoft.AspNetCore;
using Omatka;
using Omatka.Logging;
using Omatka.Types;
using Omatka.WebApi;
using Omatka.WebApi.CQRS;

namespace Cytidel.API;

public class Program
{
    public static async Task Main(string[] args)
    => await WebHost.CreateDefaultBuilder(args)
        .ConfigureServices(services => services
            .AddOmatka()
            .AddWebApi()
            .AddApplication()
            .AddInfrastructure()
            .Build())
        .Configure(app => app
            .UseInfrastructure()
            .UseEndpoints(endpoints => endpoints
                .Post<SignIn>("sign-in", async (cmd, ctx) =>
                {
                    var token = await ctx.RequestServices.GetService<IIdentityService>().SignInAsync(cmd, ctx.RequestAborted);
                    if(token.Status == "not found")
                    {
                        await ctx.Response.NotFound();
                    }
                    else if(token.Status == "password")
                    {
                        await ctx.Response.BadRequest();
                    }
                    else
                    {
                        await ctx.Response.WriteJsonAsync(token);
                    }
                })
                .Post<SignUp>("sign-up", async (cmd, ctx) =>
                {
                    var status = await ctx.RequestServices.GetService<IIdentityService>().SignUpAsync(cmd, ctx.RequestAborted);
                    if (status == "conflict")
                    {
                        await ctx.Response.BadRequest();
                    }
                    else if (status == "accepted")
                    {
                        await ctx.Response.Accepted();
                    }
                })

            )
            .UseDispatcherEndpoints(endpoints => endpoints
                .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                .Get("me", async ctx =>
                {
                    var userId = await ctx.AuthenticateUsingJwtAsync();
                    if (userId == Guid.Empty)
                    {
                        ctx.Response.StatusCode = 401;
                        return;
                    }

                    await GetUserAsync(userId, ctx, ctx.RequestAborted);
                })
                .Get<GetTask, TaskDto>("get-task/{taskId}")
                .Get<GetTasks, IEnumerable<TaskDto>>("get-tasks")
                .Post<UserChangePassword>("reset-password",  async (cmd, ctx) => 
                {
                    var userId = await ctx.AuthenticateUsingJwtAsync();
                    if (userId == Guid.Empty)
                    {
                        ctx.Response.StatusCode = 401;
                        return;
                    }
                    await ctx.Response.Created();
                })
                .Put<UpdateUser>("user-update", async (cmd, ctx) =>
                {
                    await ctx.Response.Accepted();
                })
                .Post<CreateTask>("create-task", afterDispatch: (cmd, ctx) => ctx.Response.Created())
                .Delete<DeleteTask>("delete-task/{id}", afterDispatch: (cmd, ctx) => ctx.Response.Accepted())
                .Delete<DeleteUser>("delete-user", afterDispatch: (cmd, ctx) => ctx.Response.Accepted())
                .Put<EditTask>("edit-task", afterDispatch: (cmd, ctx) => ctx.Response.Accepted())
                )
            .UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<TasksHub>("/hub/tasks");
                })

        )
        .UseLogging()
        .Build()
        .RunAsync();

    //verify if the user is registered on the database.
    private static async Task GetUserAsync(Guid id, HttpContext context, CancellationToken token)
    {
        var user = await context.RequestServices.GetService<IUserRepository>().GetUserByIdAsync(id, token);
        if (user is null)
        {
            context.Response.StatusCode = 404;
            return;
        }
        await context.Response.WriteJsonAsync(user);
    }
}
