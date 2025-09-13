using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using QuickShared.Application.Common.Behaviours;

namespace QuickShared.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
