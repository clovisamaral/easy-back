using EasyInvoice.API.Entities.Relationships;
using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyInvoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IRelationshipRepository relationshipRepository;
        private readonly IClientRepository clientRepository;
        private readonly IProviderRepository providerRepository;

        public RelationshipController(IRelationshipRepository relationshipRepository,
                                      IClientRepository clientRepository,
                                      IProviderRepository providerRepository)
        {
            this.relationshipRepository = relationshipRepository;
            this.clientRepository = clientRepository;
            this.providerRepository = providerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Relationship relationship)
        {
            try
            {
                if (relationship == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var clientId = clientRepository.GetByNameAsync(relationship.ClientName).Result?.Id;
                var providerId = providerRepository.GetByNameAsync(relationship.ProviderName).Result?.Id;

                Relationship obj = new(relationship.Name, clientId.Value, providerId.Value, relationship.IdentifierCode, relationship.Billing, relationship.ContractValue, relationship.Extract, relationship.Active);

                await relationshipRepository.AddAsync(obj);
                return Ok(obj);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await relationshipRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await relationshipRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var obj = await relationshipRepository.GetByIdAsync(id);

                if (obj == null)
                {
                    return NotFound();
                }

                await relationshipRepository.DeleteAsync(obj.Id);
                return Ok(new { message = "Relationship deleted." });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Relationship relationship)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var resource = await relationshipRepository.GetByIdAsync(id);

                if (resource == null)
                {
                    return NotFound();
                }

                var clientId = clientRepository.GetByNameAsync(relationship.ClientName).Result?.Id;
                var providerId = providerRepository.GetByNameAsync(relationship.ProviderName).Result?.Id;

                resource.Id = id;
                resource.Name = relationship.Name;
                resource.ClientId = clientId.Value;
                resource.ProviderId = providerId.Value;
                resource.Extract = relationship.Extract;
                resource.Active = relationship.Active;

                await relationshipRepository.UpdateAsync(resource);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
