using Common.Model;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using WebAPI.Controllers;

namespace ProjectUnitTests.ControllerTests
{
    public class AuthControllerTests
    {
        Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        Mock<BfkdoDbContext> _dbmock = new Mock<BfkdoDbContext>();

        [SetUp]
        public void Setup()
        {
            _configurationMock.Setup(x => x["Issuer"]).Returns("1HDJUZfokBS8Ps3OPvMfjjT3UXHyda6p");
            _configurationMock.Setup(x => x["Audience"]).Returns("Rm9GB255guYnOETGsIRjddxyZZfflSkh");
            _configurationMock.Setup(x => x["Key"]).Returns("3W68B4i5NoTs5VWkRpjGVgGJvu2ue780");
        }

        [Test]
        public void Test_BadRequestIfAdminNotFound()
        {
            var controller = new AuthController(_configurationMock.Object, _dbmock.Object);
            var response = controller.AuthAdmin(new ModelAdminAuthData("max", "password"));
            Assert.That(response is BadRequestResult);
        }

        [Test]
        public void Test_BadRequestIfAdminPasswordIsWrong()
        {
            var controller = new AuthController(_configurationMock.Object, _dbmock.Object);
            var response = controller.AuthAdmin(new ModelAdminAuthData("admin", ""));

            Assert.That(response is BadRequestResult);
        }

        [Test]
        public void Test_BadRequestIfEvaluatorPasswordIsWrong()
        {
            var controller = new AuthController(_configurationMock.Object, _dbmock.Object);
            var response = controller.AuthEvaluator(new ModelEvaluatorAuthData(""));

            Assert.That(response is BadRequestResult);
        }

        [Test]
        public void Test_BadRequestIfParticipantNotFound()
        {
            var controller = new AuthController(_configurationMock.Object, _dbmock.Object);
            var response = controller.AuthParticipant(new ModelParticipantAuthData(42, "password"));
            Assert.That(response is BadRequestResult);
        }

        [Test]
        public void Test_BadRequestIfParticipantPasswordIsWrong()
        {
            var controller = new AuthController(_configurationMock.Object, _dbmock.Object);
            var response = controller.AuthParticipant(new ModelParticipantAuthData(1337, ""));

            Assert.That(response is BadRequestResult);
        }
    }
}
