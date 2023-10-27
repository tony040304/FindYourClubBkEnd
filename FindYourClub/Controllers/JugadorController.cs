﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.ViewModel;
using Service.IServices;

namespace FindYourClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorServices _services;
        private readonly ILogger<JugadorController> _logger;

        public JugadorController(IJugadorServices services, ILogger<JugadorController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpPost("InsertarDatosJugador")]
        public ActionResult<string> InsertarDatos([FromBody] JugadorDTO jugador)
        {
            string response = string.Empty;
            try
            {
                response = _services.InsertarDatos(jugador);
                if (response == "ingrese un usuario" || response == "Usuario existente" || response == "Jugador y usuario no coinciden")
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