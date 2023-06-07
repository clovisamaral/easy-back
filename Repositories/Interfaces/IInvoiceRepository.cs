using EasyInvoice.API.Entities.Invoices;

namespace EasyInvoice.API.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(int id);
        Task<Invoice>GetByInvoiceNumberAsync(string invoiceNumber);
        Task<Invoice> GetByClientNameAsync(string name);
        Task<List<Invoice>> GetAllAsync(bool all = true);
        Task<Invoice> AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int id);
    }
}
