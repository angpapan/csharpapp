using FluentValidation.Results;

namespace CSharpApp.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("Validation Error")
    {
        Errors = failures
                .Distinct()
                .Select(failure => failure.ErrorMessage)
                .ToList();
    }

    public IReadOnlyCollection<string> Errors { get; private set; }


}
