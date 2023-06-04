using EasyInvoice.API.Entities.Clients;
using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EasyInvoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Client obj = new(client.Name, client.Email, client.CPF, client.Active);

                await clientRepository.AddAsync(obj);
                return Ok(obj);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool all)
        {
            var response = await clientRepository.GetAllAsync(all);
            return Ok(response);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await clientRepository.GetByIdAsync(id);

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
            var result = await clientRepository.GetByNameAsync(name);

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
                var obj = await clientRepository.GetByIdAsync(id);

                if (obj == null)
                {
                    return NotFound();
                }

                await clientRepository.DeleteAsync(obj.Id);
                return Ok(new { message = "Client deleted." });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var resource = await clientRepository.GetByIdAsync(id);

                if (resource == null)
                {
                    return NotFound();
                }

                resource.Id = id;
                resource.Name = client.Name;
                resource.Email = client.Email;
                resource.CPF = client.CPF;
                resource.Active = client.Active;

                await clientRepository.UpdateAsync(resource);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}