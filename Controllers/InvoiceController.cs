using EasyInvoice.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyInvoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            this._invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices([FromQuery] bool all)
        {
            var response = await _invoiceRepository.GetAllAsync(all);
            return Ok(response);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetByInvoiceNumber(string invoiceNumber)
        {
            var result = await _invoiceRepository.GetByInvoiceNumberAsync(invoiceNumber);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("clientName")]
        public async Task<IActionResult> GetClientName(string clientName)
        {
            var result = await _invoiceRepository.GetByClientNameAsync(clientName);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
