namespace EasyInvoice.API.Repositories.Data
{
    public class CorsanLayoutRegex
    {
        public CorsanLayoutRegex()
        { }

        public static Dictionary<string, string> GetLayoutInvoice()
        {
            Dictionary<string, string> dados = new Dictionary<string, string>()
        {
            { "Competence", ".(?<=COMPETÊNCIA:\\s)\\d{2}/\\d{4}" },
            { "DateIssue", ".(?<=DATA EMISSÃO:\\s)\\d{2}/\\d{2}/\\d{4}" },
            { "InvoiceNumber", ".(?<=Nº FATURA:\\s)\\d+" },
            { "ClientName", ".(?<=USUÁRIO:\\s)[a-zA-Z\\s]+" },
            { "Address", ".(?<=ENDEREÇO:\\s)[\\w\\s]*\\d[\\w\\s]" },
            { "ContractCode", ".(?<=CÓDIGO IMÓVEL:\\s)[0-9-]+" },
            { "Consumption", "(?<=CONSUMO ÁGUA.*: )\\d+" },
            { "DueDate", "(?<=ATÉ A DATA DE VENCIMENTO\\n)\\d{2}/\\d{2}/\\d{4}" },
            { "Amount", "(?<=ATÉ A DATA DE VENCIMENTO\\n\\d+\\/\\d+\\/\\d+\\s)(\\d{1,3}(?:\\.\\d{3})*,\\d+)" },
        };
            
            return dados;
        }
    }
}
