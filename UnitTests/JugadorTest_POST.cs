using FindYourClub.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOS;
using Model.Models;
using Model.ViewModel;
using Moq;
using Service.IServices;
using Service.IServices.FactoryMethod;
using Service.Services.FactoryMehod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class JugadorTest_POST
    {
        private readonly JugadorController _controller;
        private readonly Mock<IJugadorServices> _mock;
        public JugadorTest_POST()
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
        public void CrearPostulacion_OK()
        {
            // Arrange
            string userId = "1";
            var postulacion = new PostulacionViewModel { };
            _mock.Setup(services => services.CrearPostulaciones(postulacion, userId)).Returns("Postulacion creada");
            // Act
            var result = _controller.CrearPostulaciones(postulacion);
            // Assert
            var actionResult = Assert.IsType<ActionResult<string>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
        [Fact]
        public void CrearPostulacion_BadRequest()
        {
            // Arrange
            string userId = "1";
            var postulacion = new PostulacionViewModel { };
            _mock.Setup(services => services.CrearPostulaciones(postulacion, userId)).Returns("Ya has realizado una postulación para este equipo");
            // Act
            var result = _controller.CrearPostulaciones(postulacion);
            // Assert
            var actionResult = Assert.IsType<ActionResult<string>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
    }
}
