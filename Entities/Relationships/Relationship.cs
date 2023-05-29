using EasyInvoice.API.Entities.Base;
using EasyInvoice.API.Entities.Validations;
using EasyInvoice.API.Shared.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyInvoice.API.Entities.Relationships
{
    public class Relationship : EntityBase
    {
        public string Name { get; set; }
        public int ClientId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public string ClientName { get; set; }
        public int ProviderId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public string ProviderName { get; set; }
        public string IdentifierCode { get; set; }
        public string Billing { get; set; }
        public string ContractValue { get; set; }
        public bool Extract { get; set; }
        public bool Active { get; set; }

        public Relationship()
        {}

        public Relationship(string name, int clientId, int providerId, string identifierCode, string Billing, string ContractValue, bool extract, bool active)
        {
            ChangeName(name);
            ChangeClientId(clientId);
            ChangeProviderId(providerId);
            ChangeIdentifierCode(identifierCode);
            ChangeBilling(Billing);
            ChangeContractValue(ContractValue);
            ChangeExtract(extract);
            ChangeActive(active);
            Validate(); 
        }

        private void ChangeName(string name) => this.Name = name;
        private void ChangeClientId(int clientId) => this.ClientId = clientId;
        private void ChangeProviderId(int providerId) => this.ProviderId = providerId;
        private void ChangeIdentifierCode(string identifierCode) => this.IdentifierCode = identifierCode;
        private void ChangeBilling(string billing) => this.Billing = billing;
        private void ChangeContractValue(string contractValue) => this.ContractValue = contractValue;
        private void ChangeExtract(bool extract) => this.Extract = extract;
        private void ChangeActive(bool active) => this.Active = active;


        public override bool Validate()
        {
            var validator = new RelationshipValidator();
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
