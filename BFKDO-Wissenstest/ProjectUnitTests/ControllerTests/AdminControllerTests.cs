using Common.Model;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using WebAPI.Controllers;

namespace ProjectUnitTests.ControllerTests
{
    public class AdminControllerTests
    {

        Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        Mock<BfkdoDbContext> _dbmock = new Mock<BfkdoDbContext>();

        [SetUp]
        public void Setup()
        {
            _configurationMock.Setup(x => x["Issuer"]).Returns("1HDJUZfokBS8Ps3OPvMfjjT3UXHyda6p");
            _configurationMock.Setup(x => x["Audience"]).Returns("Rm9GB255guYnOETGsIRjddxyZZfflSkh");
            _configurationMock.Setup(x => x["Key"]).Returns("3W68B4i5NoTs5VWkRpjGVgGJvu2ue780");

            _dbmock.Setup(x => x.TableTestpersons).ReturnsDbSet(new List<TableTestperson>() {
                      new TableTestperson() { SybosId = 1337, Password = "password" }
                  });
        }

        [Test]
        public async Task CreateQrCode_ValidSybosId_ReturnsOk()
        {
            int validSybosId = 1337;

            var controller = new AdminController(_dbmock.Object);

            var result = await controller.CreateQrCode(validSybosId);

            Assert.IsNotNull(result);
            Assert.That(result is OkObjectResult);
        }

        [Test]
        public async Task CreateQrCode_InvalidSybosId_ReturnsBadRequest()
        {
            int validSybosId = 7331;

            var controller = new AdminController(_dbmock.Object);

            var result = await controller.CreateQrCode(validSybosId);

            Assert.IsNotNull(result);
            Assert.That(result is BadRequestObjectResult);
        }
    }
}
