using FindYourClub.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Moq;
using Service.IServices;
using System.Security.Claims;


namespace UnitTests
{
    public class JugadorTest_GET
    {
        private readonly JugadorController _controller;
        private readonly Mock<IJugadorServices> _mock;

        public JugadorTest_GET()
        {
            _mock = new Mock<IJugadorServices>();
            _controller = new JugadorController(_mock.Object);
            // Mock the user identity
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("NameIdentifier", "1") }, "mock"));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
        [Fact]
        public void GetEquipo_Test()
        {
            // Arrange
            string userId = "1";
            var expected = new List<EquipoViewModel>();
            _mock.Setup(services => services.GetEquipo(userId)).Returns(expected);
            // Act
            var result = _controller.GetEquipo();
            // Assert
            var actionResult = Assert.IsType<ActionResult<List<EquipoViewModel>>>(result);

            // Verificar que el resultado devuelto es un OkObjectResult o BadRequestObjectResult
            if (actionResult.Result is OkObjectResult)
            {
                // Si es OkObjectResult, verificar el contenido del resultado
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var actualContracts = Assert.IsType<List<EquipoViewModel>>(okResult.Value);
                Assert.Equal(expected, actualContracts);
            }
            else if (actionResult.Result is BadRequestObjectResult)
            {
                // Si es BadRequestObjectResult, verificar el mensaje de error
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
                Assert.Equal("Error al procesar la solicitud", badRequestResult.Value);
            }
        }
        [Fact]
        public void MisPostulaciones_Test()
        {
            // Arrange
            string userId = "1";
            var expected = new List<TeamPostulacionDTO>();
            _mock.Setup(services => services.MisPostulaciones(userId)).Returns(expected);
            // Act
            var result = _controller.MisPostulaciones();
            // Assert
            var actionResult = Assert.IsType<ActionResult<TeamPostulacionDTO>>(result);

            // Verificar que el resultado devuelto es un OkObjectResult o BadRequestObjectResult
            if (actionResult.Result is OkObjectResult)
            {
                // Si es OkObjectResult, verificar el contenido del resultado
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var actualContracts = Assert.IsType<List<TeamPostulacionDTO>>(okResult.Value);
                Assert.Equal(expected, actualContracts);
            }
            else if (actionResult.Result is BadRequestObjectResult)
            {
                // Si es BadRequestObjectResult, verificar el mensaje de error
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
                Assert.Equal("Error al procesar la solicitud", badRequestResult.Value);
            }
        }
        [Fact]
        public void MiEquipo_Test()
        {
            // Arrange
            string userId = "1";
            var expected = new List<ContratoJugadorDTO>();
            _mock.Setup(services => services.MiContrato(userId));
            // Act
            var result = _controller.MiContrato();
            // Assert
            var actionResult = Assert.IsType<ActionResult<List<ContratoJugadorDTO>>>(result);

            // Verificar que el resultado devuelto es un OkObjectResult o BadRequestObjectResult
            if (actionResult.Result is OkObjectResult)
            {
                // Si es OkObjectResult, verificar el contenido del resultado
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var actualContracts = Assert.IsType<List<ContratoJugadorDTO>>(okResult.Value);
                Assert.Equal(expected, actualContracts);
            }
            else if (actionResult.Result is BadRequestObjectResult)
            {
                // Si es BadRequestObjectResult, verificar el mensaje de error
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
                Assert.Equal("Error al procesar la solicitud", badRequestResult.Value);
            }
        }
    }
}