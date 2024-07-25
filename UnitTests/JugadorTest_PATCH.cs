using FindYourClub.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
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
    public class JugadorTest_PATCH
    {
        private readonly JugadorController _controller;
        private readonly Mock<IJugadorServices> _mock;

        public JugadorTest_PATCH()
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
        public void CambiarContraseña_OK()
        {
            string id = "1";
            var password = new ChangePasswordViewModel { Password = "Test", CheckPassword = "Test" };
            _mock.Setup(service => service.ChangePassword(password, id)).Returns("Contraseñas cambiada");

            // Llamar al método PATCH que estás probando
            var result = _controller.ChangePassword(password);

            // Verificar que el resultado es del tipo esperado (OkObjectResult)
            var actionResult = Assert.IsType<ActionResult<string>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void CambiarContraseña_ValidationProblem()
        {
            string id = "1";
            var password = new ChangePasswordViewModel { Password = "Test", CheckPassword = "hola" };
            _mock.Setup(service => service.ChangePassword(password, id)).Returns("Contraseñas diferentes");

            // Llamar al método PATCH que estás probando
            var result = _controller.ChangePassword(password);

            // Verificar que el resultado es del tipo esperado (OkObjectResult)
            var actionResult = Assert.IsType<ActionResult<string>>(result);
            var okResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

    }
}
