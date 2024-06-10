using Business.Constants.Messages;
using Core.Entities.Dtos.Role;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.RoleValidators;

public class RoleUpdateValidator : AbstractValidator<RoleUpdateDto>
{
    public RoleUpdateValidator()
    {
        RuleFor(role => role.Name).NotEmpty().NotNull().WithMessage(RoleMessages.RoleNameCanNotBeEmpty);
        RuleFor(role => role.Name).MinimumLength(3).WithMessage(RoleMessages.RoleNameMustBeAtLeast3Characters);
        RuleFor(role => role.Name).MaximumLength(50).WithMessage(RoleMessages.RoleNameMustBeAtMost50Characters);
    }
}