using EasyInvoice.API.Entities.Clients;

namespace EasyInvoice.API.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByNameAsync(string name);
        Task<List<Client>> GetAllAsync();
        Task<Client> AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
    }
}
