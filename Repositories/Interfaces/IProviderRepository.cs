using EasyInvoice.API.Entities.Providers;
namespace EasyInvoice.API.Repositories.Interfaces;

public interface IProviderRepository
{
    Task<Provider> GetByIdAsync(int id);
    Task<Provider> GetByNameAsync(string name);
    Task<List<Provider>> GetAllAsync(bool all=true);
    Task<Provider> AddAsync(Provider provider);
    Task UpdateAsync(Provider provider);
    Task DeleteAsync(int id);
 }