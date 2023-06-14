using EasyInvoice.API.Repositories.Interfaces;
using EasyInvoice.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EasyInvoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public BotController(IInvoiceRepository invoiceRepository)
        {
            this._invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public IActionResult DownloadAndExtractInvoices(string codigoImovel, string cpf)
        {
            try
            {
                var bot = new BotService();
                bot.RunTask(codigoImovel, cpf);

                foreach (var invoice in bot.GetInvoicesList())
                {
                    _invoiceRepository.AddAsync(invoice);
                }
                return Ok(new { message = "Baixa de faturas e inserção em database concluída com sucesso" });
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Ocorreu erro no processo de baixar faturas: {ex.Message}");
            }
            catch (PostgresException ex)
            {
                throw new Exception($"Ocorreu erro no processo de baixar faturas: {ex.Message}");
            }
            catch (Exception ex)
            {
                 throw new Exception($"Ocorreu erro no processo de baixar faturas: {ex.Message}");
            }
        }
    }
}
