using FluentValidation;

namespace Contact.Application.Features.ContactPersons.Commands.SaveContactPerson
{
    public class SaveContactCommandValidator : AbstractValidator<SaveContactPersonCommand>
    {
        public SaveContactCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{Name} must not be over 100 characters.");

            RuleFor(p => p.Surname)
                .NotEmpty().WithMessage("{Surname} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{Surname} must not be over 100 characters.");

            RuleFor(p => p.Firm)
                .NotEmpty().WithMessage("{Firm} is required.")
                .NotNull()
                .MaximumLength(250).WithMessage("{Firm} must not be over 250 characters.");
        }
    }
}
