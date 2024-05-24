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
    public class EquipoTest_PATCH
    {
        private readonly EquipoController _controller;
        private readonly Mock<IEquipService> _mock;

        public EquipoTest_PATCH()
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
        public void UpdateInfo_OK()
        {
            string id = "1";
            var equipo = new EquipoViewModel { Nombre = "Test" };
            _mock.Setup(service => service.UpdateInfo(equipo, id)).Returns("Equipo actualizado");

            // Llamar al método PATCH que estás probando
            var result = _controller.UpdateInfo(equipo);

            // Verificar que el resultado es del tipo esperado (OkObjectResult)
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Equipo actualizado", okResult.Value);
        }

        [Fact]
        public void UpdateInfo_ValidationProblem()
        {
            string id = "1";
            var equipo = new EquipoViewModel { Nombre = "Test" };
            _mock.Setup(service => service.UpdateInfo(equipo, id)).Returns("Credenciales incorrectas");

            // Llamar al método PATCH que estás probando
            var result = _controller.UpdateInfo(equipo);

            // Verificar que el resultado es del tipo esperado (ValidationProblemDetails)
            var validationProblemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal("Credenciales incorrectas", validationProblemResult.Value);
        }

        [Fact]
        public void PasswordChange_OK()
        {
            string id = "1";
            var Password = new ChangePasswordViewModel { Password = "Test", CheckPassword = "Test" };
            _mock.Setup(service => service.PasswordChange(Password, id)).Returns("Contraseñas cambiada");

            // Llamar al método PATCH que estás probando
            var result = _controller.PasswordChange(Password);

            // Verificar que el resultado es del tipo esperado (OkObjectResult)
            var actionResult = Assert.IsType<ActionResult<string>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal("Contraseñas cambiada", okResult.Value);
        }

        [Fact]
        public void PasswordChange_ValidationProblem()
        {
            string id = "1";
            var Password = new ChangePasswordViewModel { Password = "Test", CheckPassword = "Test" };
            _mock.Setup(service => service.PasswordChange(Password, id)).Returns("Credenciales incorrectas");

            // Llamar al método PATCH que estás probando
            var result = _controller.PasswordChange(Password);

            // Verificar que el resultado es del tipo esperado (ValidationProblemDetails)
            var actionResult = Assert.IsType<ActionResult<string>>(result);
            var validationProblemResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Credenciales incorrectas", validationProblemResult.Value); 
            Assert.Equal("Contraseñas diferentes", validationProblemResult.Value);
        }
    }
}
