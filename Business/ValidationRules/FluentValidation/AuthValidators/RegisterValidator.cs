using Business.Constants;
using Core.Entities.Dtos.Auth;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.AuthValidators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(user => user.Username).NotEmpty().NotNull().WithMessage(UserMessages.UsernameCannotBeEmpty);
        RuleFor(user => user.Username).MinimumLength(3).WithMessage(UserMessages.UsernameMustBeAtLeast3Characters);
        RuleFor(user => user.Username).MaximumLength(100).WithMessage(UserMessages.UsernameMustBeAtMost50Characters);

        RuleFor(user => user.FirstName).NotEmpty().NotNull().WithMessage(UserMessages.FirstNameCannotBeEmpty);
        RuleFor(user => user.FirstName).MinimumLength(3).WithMessage(UserMessages.FirstNameMustBeAtLeast3Characters);
        RuleFor(user => user.FirstName).MaximumLength(100).WithMessage(UserMessages.FirstNameMustBeAtMost100Characters);

        RuleFor(user => user.LastName).NotEmpty().NotNull().WithMessage(UserMessages.LastNameCannotBeEmpty);
        RuleFor(user => user.LastName).MinimumLength(3).WithMessage(UserMessages.LastNameMustBeAtLeast3Characters);
        RuleFor(user => user.LastName).MaximumLength(100).WithMessage(UserMessages.LastNameMustBeAtMost100Characters);

        RuleFor(user => user.Email).NotEmpty().NotNull().WithMessage(UserMessages.EmailCannotBeEmpty);
        RuleFor(user => user.Email).EmailAddress().WithMessage(UserMessages.EmailMustBeValid);
        
    }
}