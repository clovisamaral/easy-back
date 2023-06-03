using EasyInvoice.API.Entities.Dto;
using EasyInvoice.API.Entities.Relationships;
using EasyInvoice.API.Repositories.Context;
using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyInvoice.API.Repositories.Relationships
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly DataContext _Db;

        public RelationshipRepository(DataContext dataContext)
        {
            _Db = dataContext;
        }
        public async Task<Relationship> AddAsync(Relationship relationship)
        {
            await _Db.AddAsync(relationship);
            await _Db.SaveChangesAsync();

            return relationship;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _Db.Relationships.FindAsync(id);
            _Db.Relationships.Remove(response);
            await _Db.SaveChangesAsync();
        }
        public async Task<List<RelationshipResponse>> GetAllAsync()
        {
            List<RelationshipResponse> results = await _Db.Relationships
            .Join(_Db.Providers, relation => relation.ProviderId, provider => provider.Id, (relation, provider) => new { relation, provider })
            .Where(p=>p.provider.Active==true)
            .Join(_Db.Clients, relation1 => relation1.relation.ClientId, client => client.Id, (relation1, client) => new { relation1, client })
            //.Where(c => c.client.Active == true)
            .Select(x => new RelationshipResponse
            {
                Id = x.relation1.relation.Id,
                Name = x.relation1.relation.Name,
                ProviderName = x.relation1.provider.Name,
                IdentifierCode = x.relation1.relation.IdentifierCode,
                BillingType = x.relation1.relation.Billing,
                ContractValue = x.relation1.relation.ContractValue,
                Extract = x.relation1.relation.Extract,
                ClientName = x.client.Name,
                CPF = x.client.CPF,
                Email = x.client.Email,
                Active = x.relation1.relation.Active
            })
            .ToListAsync();

            return results;
        }
        public async Task<Relationship> GetByIdAsync(int id)
        {
            return await _Db.Relationships.FindAsync(id);
        }

        public async Task UpdateAsync(Relationship relationship)
        {
            _Db.Relationships.Update(relationship);
            await _Db.SaveChangesAsync();
        }

    }
}
