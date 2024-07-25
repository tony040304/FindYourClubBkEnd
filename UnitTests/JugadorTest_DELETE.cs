using FindYourClub.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.ViewModel;
using Moq;
using Service.IServices;
using System.Security.Claims;
using Xunit;

namespace UnitTests
{
    public class JugadorTest_DELETE
    {
        private readonly JugadorController _controller;
        private readonly Mock<IJugadorServices> _mock;

        public JugadorTest_DELETE()
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
        public void DeletePostulacion_OK()
        {
            // Arrange
            int userId = 1;
            _mock.Setup(services => services.DeletePostulacion(userId));
            // Act
            var result = _controller.DeletePostulacion(userId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void DeletePostulacion_BadRequest()
        {
            // Arrange
            int userId = 1;
            _mock.Setup(services => services.DeletePostulacion(userId)).Throws(new Exception());
            // Act
            var result = _controller.DeletePostulacion(userId);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void DeleteContrato_OK()
        {
            // Arrange
            int userId = 1;
            _mock.Setup(services => services.DeleteContrato(userId));
            // Act
            var result = _controller.DeleteContrato(userId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void DeleteContrato_BadRequest()
        {
            // Arrange
            int userId = 1;
            string errorMessage = "Test exception";
            _mock.Setup(services => services.DeleteContrato(userId)).Throws(new Exception(errorMessage));
            // Act
            var result = _controller.DeleteContrato(userId) as BadRequestObjectResult;
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, result.Value);
        }
    }
}
