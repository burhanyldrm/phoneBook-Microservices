using FluentValidation;

namespace Contact.Application.Features.ContactPersonInfos.Commands.SaveContactPersonInfo
{
    public class SaveContactInfoCommandValidator : AbstractValidator<SaveContactPersonInfoCommand>
    {
        public SaveContactInfoCommandValidator()
        {
            RuleFor(p => p.ContactPersonId)
                .NotEmpty().WithMessage("{ContactPersonId} is required.")
                .NotNull();

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{Type} is required.")
                .NotNull();

            RuleFor(p => p.Info)
                .NotEmpty().WithMessage("{Info} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{Info} must not be over 100 characters.");
        }
    }
}
