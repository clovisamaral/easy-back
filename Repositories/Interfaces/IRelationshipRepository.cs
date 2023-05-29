using EasyInvoice.API.Entities.Dto;
using EasyInvoice.API.Entities.Relationships;

namespace EasyInvoice.API.Repositories.Interfaces
{
    public interface IRelationshipRepository
    {
        Task<Relationship> GetByIdAsync(int id);
        Task<List<RelationshipResponse>> GetAllAsync();
        Task<Relationship> AddAsync(Relationship relationship);
        Task UpdateAsync(Relationship relationship);
        Task DeleteAsync(int id);
    }
}
