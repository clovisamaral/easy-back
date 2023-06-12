using EasyInvoice.API.Entities.Base;
using EasyInvoice.API.Entities.Validations;
using EasyInvoice.API.Shared.Exceptions;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyInvoice.API.Entities.Clients
{
    public class Client : EntityBase
    {
        public string Name { get; set; }

        [JsonIgnore]
        public string Code { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public bool Active { get; set; }

        public Client() { }

        public Client(string name, string email, string cpf, bool active = true)
        {
            ChangeName(name);
            Code = GenerateSigla(name);
            ChangeEmail(email);
            ChangeActive(active);
            ChangeCpf(cpf);
            Validate();
        }

        private void ChangeName(string name)
        {
            this.Name = name;
        }
        private void ChangeEmail(string email)
        {
            this.Email = email;
        }
        private string GenerateSigla(string str)
        {
            string[] palavras = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sigla = new StringBuilder();
            foreach (string palavra in palavras)
            {
                sigla.Append(palavra[0]);
            }

            return sigla.ToString().ToUpper();
        }
        private void ChangeCpf(string cpf)
        {
            this.CPF = cpf;
        }

        private void ChangeActive(bool active)
        {
            this.Active = active;
        }

        public override bool Validate()
        {
            var validator = new ClientValidator();
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
