using FindYourClub.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class EquipoTest_DELETE
    {
        private readonly EquipoController _controller;
        private readonly Mock<IEquipService> _mock;

        public EquipoTest_DELETE()
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
        public void DeletePostulacion_OK()
        {
            // Id de ejemplo para la prueba
            int id = 1;

            // Configurar el comportamiento del mock
            _mock.Setup(service => service.DeletePostulacion(id));

            // Llamar al método DELETE que estás probando
            var result = _controller.DeletePostulacion(id);

            // Verificar que el resultado es del tipo esperado
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void DeletePostulacion_BadRequest()
        {
            // Id de ejemplo para la prueba
            int id = 1;

            // Configurar el comportamiento del mock
            _mock.Setup(service => service.DeletePostulacion(id));

            // Llamar al método DELETE que estás probando
            var result = _controller.DeletePostulacion(id);

            // Verificar que el resultado es del tipo esperado
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void DeleteContrato_OK()
        {
            // Id de ejemplo para la prueba
            int id = 1;

            // Configurar el comportamiento del mock
            _mock.Setup(service => service.DeleteContrato(id));

            // Llamar al método DELETE que estás probando
            var result = _controller.DeleteContrato(id);

            // Verificar que el resultado es del tipo esperado
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void DeleteContrato_BadRequest()
        {
            // Id de ejemplo para la prueba
            int id = 1;

            // Configurar el comportamiento del mock
            _mock.Setup(service => service.DeleteContrato(id));

            // Llamar al método DELETE que estás probando
            var result = _controller.DeleteContrato(id);

            // Verificar que el resultado es del tipo esperado
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
