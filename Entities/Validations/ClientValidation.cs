using FluentValidation;

namespace EasyInvoice.API.Entities.Validations
{
    public class ClientValidator : AbstractValidator<Clients.Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("The name field cannot be empty.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("The e-mail field cannot be empty");
            RuleFor(x=>x.Code)
                .NotEmpty()
                .NotNull()
                .WithMessage("The code field cannot be empty");
            RuleFor(x => x.CPF)
               .NotEmpty()
               .NotNull()
               .WithMessage("The cpf field cannot be empty");
        }
    }
}
