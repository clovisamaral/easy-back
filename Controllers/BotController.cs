using EasyInvoice.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyInvoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFaturas(string codigoImovel, string cpf)
        {
            try
            {
                var bot = new BotService();
                bot.RunTask(codigoImovel,cpf);

                return Ok(new { message = "Baixa de faturas concluída com sucesso" });
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu erro no processo de baixar faturas: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("{pathFull}")]
        public IActionResult GetTextPDF(string pathFull)
        {
            try
            {
                var bot = new BotService();
                bot.ExtractDataInvoice(pathFull);

                return Ok(new { message = "Extract de faturas concluída com sucesso" });
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu erro no processo de extrais faturas: {ex.Message}");
            }

        }
    }
}
