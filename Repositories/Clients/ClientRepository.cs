using EasyInvoice.API.Entities.Clients;
using EasyInvoice.API.Repositories.Context;
using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyInvoice.API.Repositories.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _Db;

        public ClientRepository(DataContext dataContext)
        {
            _Db = dataContext;
        }
        public async Task<Client> AddAsync(Client client)
        {
            await _Db.AddAsync(client);
            await _Db.SaveChangesAsync();

            return client;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _Db.Clients.FindAsync(id);
            _Db.Clients.Remove(response);
            await _Db.SaveChangesAsync();
        }
        public async Task<List<Client>> GetAllAsync()
        {
            var results = await _Db.Clients.ToListAsync();
            return results;
        }
        public async Task<Client> GetByIdAsync(int id)
        {
            return await _Db.Clients.FindAsync(id);
        }

        public async Task<Client> GetByNameAsync(string name)
        {
            return await _Db.Clients.FirstOrDefaultAsync(x => x.Name == name);   
        }

        public async Task UpdateAsync(Client client)
        {
            _Db.Clients.Update(client);
            await _Db.SaveChangesAsync();
        }

    }
}
