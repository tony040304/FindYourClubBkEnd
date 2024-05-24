using FindYourClub.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Model.DTOS;
using Moq;
using Service.IServices;
using System.Security.Claims;


namespace UnitTests
{
    public class EquipoTest_GET
    {
        private readonly EquipoController _controller;
        private readonly Mock<IEquipService> _mock;

        public EquipoTest_GET()
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
        public void ContratoList_Test()
        {
            // Id de ejemplo para la prueba
            string id = "1";

            // Configurar el comportamiento del mock
            var expectedContracts = new List<ContratoEquipoDTO>(); // Añade los contratos esperados
            _mock.Setup(service => service.ContratoList(id)).Returns(expectedContracts);

            // Llamar al método que estás probando con el parámetro id
            var result = _controller.ContratoList();

            // Verificar que el resultado es del tipo esperado (ActionResult<List<ContratoEquipoDTO>>)
            var actionResult = Assert.IsType<ActionResult<List<ContratoEquipoDTO>>>(result);

            // Verificar que el resultado devuelto es un OkObjectResult o BadRequestObjectResult
            if (actionResult.Result is OkObjectResult)
            {
                // Si es OkObjectResult, verificar el contenido del resultado
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var actualContracts = Assert.IsType<List<ContratoEquipoDTO>>(okResult.Value);
                Assert.Equal(expectedContracts, actualContracts);
            }
            else if (actionResult.Result is BadRequestObjectResult)
            {
                // Si es BadRequestObjectResult, verificar el mensaje de error
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
                Assert.Equal("Error al procesar la solicitud", badRequestResult.Value);
            }
        }
        [Fact]
        public void GetPostulacionbyTeam_Test()
        {
            int UsuEquipoId = 1;

            var expectedPostulations = new List<UserPostulacionDTO>();
            _mock.Setup(s=> s.GetPostulacionbyTeam(UsuEquipoId)).Returns(expectedPostulations);

            var result = _controller.GetPostulacionbyTeam();

            var actionResult = Assert.IsType<ActionResult<List<UserPostulacionDTO>>>(result);

            if (actionResult.Result is OkObjectResult)
            {
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var actualPostulation = Assert.IsType<List<UserPostulacionDTO>>(okResult.Value);
                Assert.Equal(expectedPostulations, actualPostulation);
            }
            else if (actionResult.Result is BadRequestObjectResult)
            {
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }
        [Fact]
        public void GetPlantel_Test()
        {
            string id = "1";

            var expected = new List<JugadoresEquipoDTO>();
            _mock.Setup(s => s.GetPlantel(id)).Returns(expected);

            var result = _controller.GetPlantel();

            var actionResult = Assert.IsType<ActionResult<List<JugadoresEquipoDTO>>>(result);

            if (actionResult.Result is OkObjectResult)
            {
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var actualPlantel = Assert.IsType<List<JugadoresEquipoDTO>>(okResult.Value);
                Assert.Equal(expected, actualPlantel);
            }
            else if (actionResult.Result is BadRequestObjectResult)
            {
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }
        [Fact]
        public void MyData_Test()
        {
            string id = "1";

            var expected = new List<EquipoDTO>();
            _mock.Setup(s => s.MyData(id)).Returns(expected);

            var result = _controller.MyData();

            var actionResult = Assert.IsType<ActionResult<List<EquipoDTO>>>(result);

            if (actionResult.Result is OkObjectResult)
            {
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var actual = Assert.IsType<List<EquipoDTO>>(okResult?.Value);
                Assert.Equal(expected, actual);
                Assert.True(actual != null);
            }
            else if (actionResult.Result is BadRequestObjectResult)
            {
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }
    }
}