using FindYourClub.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOS;
using Model.ViewModel;
using Moq;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class EquipoTest_POST
    {
        private readonly EquipoController _controller;
        private readonly Mock<IEquipService> _mock;
        public EquipoTest_POST()
        {
            _mock = new Mock<IEquipService>();
            _controller = new EquipoController(_mock.Object);

            // Mock the user identity
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("NameIdentifier", "1")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
        [Fact]
        public void CrearContrato_OK()
        {
            int userid = 1;
            string teamid = "1";
            // Datos de ejemplo para la prueba
            var contrato = new ContratoViewModel { SalarioJugador = 10 };
            _mock.Setup(service => service.CrearContrato(contrato, teamid, userid)).Returns("Creado correctamente");

            // Llamar al método POST que estás probando
            var result = _controller.CrearContrato(contrato, userid);

            // Verificar que el resultado es del tipo esperado
            var createdResult = Assert.IsType<ActionResult<string>>(result);
            var okResult = Assert.IsType<OkObjectResult>(createdResult.Result);
            Assert.Equal("Creado correctamente", okResult.Value);
        }
        [Fact]
        public void CrearContrato_Problem()
        {
            int userid = 1;
            string teamid = "1";
            // Datos de ejemplo para la prueba
            var contrato = new ContratoViewModel { SalarioJugador = 10 };
            _mock.Setup(service => service.CrearContrato(contrato, teamid, userid)).Returns(contrato.ToString());

            // Llamar al método POST que estás probando
            var result = _controller.CrearContrato(contrato, userid);

            // Verificar que el resultado es del tipo esperado
            var createdResult = Assert.IsType<ActionResult<string>>(result);
            var okResult = Assert.IsType<BadRequestObjectResult>(createdResult.Result);
            Assert.Equal("El equipo ya tiene un contrato con esta persona", okResult.Value);
            Assert.Equal("Este jugador no existe", okResult.Value);
        }
    }
}
