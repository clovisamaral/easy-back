using EasyInvoice.API.Entities.Providers;
using EasyInvoice.API.Repositories.Context;
using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyInvoice.API.Repositories.Providers
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly DataContext _Db;

        public ProviderRepository(DataContext dataContext)
        {
            _Db = dataContext;
        }
        public async Task<Provider> AddAsync(Provider provider)
        {
            await _Db.AddAsync(provider);
            await _Db.SaveChangesAsync();

            return provider;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _Db.Providers.FindAsync(id);
            _Db.Providers.Remove(response);
            await _Db.SaveChangesAsync();
        }
        public Task<List<Provider>> GetAllAsync()
        {
            return _Db.Providers.ToListAsync();
        }
        public async Task<Provider> GetByIdAsync(int id)
        {
            return await _Db.Providers.FindAsync(id);
        }

        public async Task<Provider> GetByNameAsync(string name)
        {
            return await _Db.Providers.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(Provider provider)
        {
            _Db.Providers.Update(provider);
            await _Db.SaveChangesAsync();
        }
    }
}
