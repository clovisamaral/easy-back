using EasyInvoice.API.Entities.Providers;
using FluentValidation;

namespace EasyInvoice.API.Entities.Validations
{
    public class ProviderValidator : AbstractValidator<Provider>
    {
        public ProviderValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
                .WithMessage($"The Name field cannot be empty.");
            RuleFor(x => x.UrlBase)
                .NotEmpty()
                .NotNull()
                .WithMessage("The UrlBase field cannot be empty");
        }
    }
}
