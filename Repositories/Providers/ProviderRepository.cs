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
        public async Task<List<Provider>> GetAllAsync(bool all = true)
        {
            var results = all == true ?
                    await _Db.Providers.ToListAsync() :
                    await _Db.Providers.Where(x => x.Active == true)
                    .ToListAsync();
            return results;
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
