using FluentResults;
using FluentValidation;
using Mediator;
using System.Collections.Immutable;

namespace QuickShared.Application.Common.Behaviours;

internal sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async ValueTask<TResponse> Handle(
        TRequest message,
        MessageHandlerDelegate<TRequest, TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(message);

        var validationTasks = _validators.Select(v => v.ValidateAsync(context, cancellationToken));
        var validationResults = await Task.WhenAll(validationTasks);

        var validationErrors = validationResults
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .ToImmutableArray();

        if (validationErrors.Length > 0)
        {
            var errors = validationErrors
                .Select(e => new Error(e.ErrorMessage))
                .ToList();

            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var genericArg = typeof(TResponse).GenericTypeArguments[0];
                var failMethod = typeof(Result)
                    .GetMethods()
                    .First(m => m.Name == nameof(Result.Fail) && m.IsGenericMethod)
                    .MakeGenericMethod(genericArg);

                return (TResponse)failMethod.Invoke(null, [errors[0]])!;
            }

            if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Fail(errors);
            }

            throw new InvalidOperationException(
                $"ValidationBehavior só funciona com Result ou Result<T>, mas o handler retornou {typeof(TResponse).Name}");
        }

        return await next(message, cancellationToken);
    }
}
