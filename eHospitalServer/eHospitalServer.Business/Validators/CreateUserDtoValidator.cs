using eHospitalServer.Entities.DTOs;
using FluentValidation;

namespace eHospitalServer.Business.Validators;
public sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Minimum length of First Name is 3");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
    }
}
