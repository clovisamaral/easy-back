namespace EasyInvoice.API.Entities.Dto
{
    public class InvoiceRequest
    {
        public string Competence { get; set; }
        public DateTime DateIssue { get; set; }
        public string InvoiceNumber { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string ContractCode { get; set; }
        public string Consumption { get; set; }
        public DateTime DueDate { get; set; }
        public string Amount { get; set; }
    }
}
