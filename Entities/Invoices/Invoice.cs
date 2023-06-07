using EasyInvoice.API.Entities.Base;

namespace EasyInvoice.API.Entities.Invoices
{
    public class Invoice : EntityBase
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

        public Invoice()
        {}

        public Invoice(string competence, DateTime dateIssue, string invoiceNumber, string clientName, string address, string contractCode, string consumption, DateTime dueDate, string ammount)
        {
            Competence = competence;
            DateIssue = dateIssue;
            InvoiceNumber = invoiceNumber;
            ClientName = clientName;
            Address = address;
            ContractCode = contractCode;
            Consumption = consumption;
            DueDate = dueDate;
            Amount = ammount;

        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
