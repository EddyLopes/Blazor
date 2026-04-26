using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Pipelines;

public class ValidationPipelineBahavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IValidateMe
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBahavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            if(!validationResults.Any(r => r.IsValid))
            {
                List<string> errors = [];
                var failures = validationResults.SelectMany(r => r.Errors)
                                                .Where(f => f != null)
                                                .ToList();
                foreach( var error in failures) 
                { 
                    errors.Add(error.ErrorMessage);
                }

                return (TResponse)await ResponseWrapper.FailAsync(errors);
            }
        }

        return await next();
    }
}
