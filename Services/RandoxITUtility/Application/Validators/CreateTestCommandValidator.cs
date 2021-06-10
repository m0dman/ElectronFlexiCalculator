using FluentValidation;
using RandoxITUtility.Application.Queries;

namespace RandoxITUtility.Application.Validators
{
    /// <summary>
    /// Insert test validator for mediator/efcore
    /// </summary>
    public class CreateTestCommandValidator : AbstractValidator<CreateTestCommand>
    {
        public CreateTestCommandValidator()
        {
            RuleFor(x => x.Test.Name)
                .NotEmpty();
        }
    }
}