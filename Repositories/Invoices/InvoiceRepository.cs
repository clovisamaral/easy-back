using EasyInvoice.API.Entities.Invoices;
using EasyInvoice.API.Repositories.Context;
using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyInvoice.API.Repositories.Invoices
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _Db;

        public InvoiceRepository(DataContext dataContext)
        {
            _Db = dataContext;
        }

        public InvoiceRepository()
        {
        }

        public Invoice AddAsync(Invoice invoice)
        {
            _Db.Add(invoice);
            _Db.SaveChanges();

            return invoice;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _Db.Invoices.FindAsync(id);
            _Db.Invoices.Remove(response);
            await _Db.SaveChangesAsync();
        }
        public async Task<List<Invoice>> GetAllAsync(bool all = true)
        {
            var results = await _Db.Invoices.ToListAsync();
            return results;
        }
        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _Db.Invoices.FindAsync(id);
        }
        public async Task<Invoice> GetByInvoiceNumberAsync(string invoiceNumber)
        {
            return await _Db.Invoices.FindAsync(invoiceNumber);
        }
        public async Task<Invoice> GetByClientNameAsync(string name)
        {
            return await _Db.Invoices.FirstOrDefaultAsync(x => x.ClientName == name);
        }
        public async Task UpdateAsync(Invoice invoice)
        {
            _Db.Invoices.Update(invoice);
            await _Db.SaveChangesAsync();
        }
    }
}
