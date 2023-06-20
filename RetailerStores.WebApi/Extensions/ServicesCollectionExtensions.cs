using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using RetailerStores.Application.Stores.Queries;

namespace RetailerStores.WebApi.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection self)
        {
            var validators = AssemblyScanner.FindValidatorsInAssemblyContaining<GetStoreQuery>();
            validators.ForEach(validator => self.AddTransient(validator.InterfaceType, validator.ValidatorType));

            return self;
        }

        public static IServiceCollection ConfigureProblemDetails(this IServiceCollection self)
        {
            self.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => true;
                options.OnBeforeWriteDetails = (ctx, problem) =>
                {
                    problem.Extensions.Remove("exceptionDetails");
                };
                options.ValidationProblemStatusCode = 400;

                options.Map<ValidationException>((ctx, ex) =>
                {
                    var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                    var errors = ex.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Select(x => x.ErrorMessage).ToArray());

                    return factory.CreateValidationProblemDetails(ctx, errors);
                });
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
            
            return self;
        }
    }
}
