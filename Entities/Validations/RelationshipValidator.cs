using EasyInvoice.API.Entities.Relationships;
using FluentValidation;

namespace EasyInvoice.API.Entities.Validations
{
    public class RelationshipValidator : AbstractValidator<Relationship>
    {
        public RelationshipValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("The name field cannot be empty.");
            RuleFor(x => x.ClientId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The client id field cannot be empty");
            RuleFor(x => x.ProviderId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The provider id field cannot be empty");
            RuleFor(x => x.IdentifierCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("The IdentifierCode field cannot be empty");
            RuleFor(x => x.Billing)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Billing field cannot be empty");
            RuleFor(x => x.ContractValue)
               .NotEmpty()
               .NotNull()
               .WithMessage("The ContractValue field cannot be empty");
        }
    }
}
