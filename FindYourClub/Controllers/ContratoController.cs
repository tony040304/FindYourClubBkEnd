using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Model.Models;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoServices _contratoServices;
        private readonly ILogger<ContratoController> _logger;

        public ContratoController(IContratoServices contratoServices, ILogger<ContratoController> logger)
        {
            _contratoServices = contratoServices;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<string> CrearContrato(ContratoDTO contrato)
        {
            string response = string.Empty;
            try
            {
                response = _contratoServices.CrearContrato(contrato);
                if (response == "Falta id equipo o id jugador" || response == "Contrato existente")
                    return BadRequest(response);

            }
            catch (Exception ex)
            {
                _logger.LogError("Cree usuario", ex);
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }
    }
}
