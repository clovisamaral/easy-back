using EasyInvoice.API.Entities.Providers;
using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace EasyInvoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderRepository providerRepository;

        public ProviderController(IProviderRepository providerRepository)
        {
            this.providerRepository = providerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Provider provider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Provider obj = new(provider.Name, provider.UrlBase, provider.Active);

                await providerRepository.AddAsync(obj);
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
            var response = await providerRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await providerRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("name")]
        public async Task<IActionResult> Get(string name)
        {
            if (name == null)
            { 
                return NotFound();
            }

            var result = await providerRepository.GetByNameAsync(name);

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
                var obj = await providerRepository.GetByIdAsync(id);

                if (obj == null)
                {
                    return NotFound();
                }

                await providerRepository.DeleteAsync(obj.Id);
                return Ok(new { message = "Client deleted." });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Provider provider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var resource = await providerRepository.GetByIdAsync(id);

                if (resource == null)
                {
                    return NotFound();
                }

                resource.Id = id;
                resource.Name = provider.Name;
                resource.Active = provider.Active;

                await providerRepository.UpdateAsync(resource);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
