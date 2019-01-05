using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Controllers;

namespace NUnitTests
{
    [TestFixture]
    public class MockyController_
    {
        [Test]
        public async Task StatusOKAsync()
        {
            // Arrange
            Mock<IMockyService> mock = new Mock<IMockyService>();
            mock.Setup(service => service.GetAsync()).ReturnsAsync("Czary mary");
            MockyController controller = new MockyController(mock.Object);

            // Act
            var actionResult = await controller.RequestMocky();

            // Assert 
            Assert.That(actionResult.Result, Is.Null);
            Assert.That(actionResult.Value, Is.EqualTo("Czary mary"));
        }

        [Test]
        public async Task BadRequestAsync()
        {
            // Arrange
            Mock<IMockyService> mock = new Mock<IMockyService>();
            mock.Setup(service => service.GetAsync()).Throws<Exception>();
            MockyController controller = new MockyController(mock.Object);

            // Act
            var actionResult = await controller.RequestMocky();

            // Assert 
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult.Result);
        }
    }
}
