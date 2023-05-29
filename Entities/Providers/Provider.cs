using EasyInvoice.API.Entities.Base;
using EasyInvoice.API.Entities.Validations;
using EasyInvoice.API.Shared.Exceptions;

namespace EasyInvoice.API.Entities.Providers
{
    public class Provider : EntityBase
    {
        public string Name { get; set; }
        public string UrlBase { get; set; }
        public bool Active { get; set; }
             
      
        public Provider() { }

        public Provider(string name, string urlBase, bool active)
        {
            ChangeName(name);
            ChangeUrlBase(urlBase);
            ChangeActive(active);
            Validate();
        }

        private void ChangeName(string name) => this.Name = name;
        private void ChangeUrlBase(string urlBase) => this.UrlBase = urlBase;
        private void ChangeActive(bool active) => this.Active = active;


        public override bool Validate()
        {
            var validator = new ProviderValidator();
            var validations = validator.Validate(this);

            if (validations.IsValid) return true;
            foreach (var err in validations.Errors)
            {
                _errors.Add(err.ErrorMessage);

                throw new DomainException("Some errors were found, please correct them", _errors);
            }
            return true;
        }
    }
}

