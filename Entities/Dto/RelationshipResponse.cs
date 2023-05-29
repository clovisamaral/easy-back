namespace EasyInvoice.API.Entities.Dto
{
    public class RelationshipResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public string IdentifierCode { get; set; }
        public string BillingType { get; set; }
        public string ContractValue { get; set; }
        public bool Extract { get; set; }
        public string ClientName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
