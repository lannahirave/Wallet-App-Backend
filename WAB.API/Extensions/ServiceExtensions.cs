using System.Reflection;
using WAB.API.Middleware;
using WAB.BLL.MapingProfiles;
using WAB.BLL.Services;
using WAB.BLL.Services.Abstract;
using WAB.DAL.Repositories;
using WAB.DAL.Repositories.Abstract;

namespace WAB.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<UserBaseService, UserService>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<TransactionBaseService, TransactionService>();
    }

    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<TransactionProfile>();
            },
            Assembly.GetExecutingAssembly());
    }

    public static void RegisterMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}