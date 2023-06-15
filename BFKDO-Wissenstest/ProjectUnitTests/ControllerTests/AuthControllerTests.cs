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
            SetupAdminDB();

            var controller = new AuthController(_configurationMock.Object, _dbmock.Object);
            var response = controller.AuthAdmin(new ModelAdminAuthData("max", "password"));
            Assert.That(response is BadRequestResult);
        }

        [Test]
        public void Test_BadRequestIfAdminPasswordIsWrong()
        {
            SetupAdminDB();

            var controller = new AuthController(_configurationMock.Object, _dbmock.Object);
            var response = controller.AuthAdmin(new ModelAdminAuthData("admin", ""));

            Assert.That(response is BadRequestResult);
        }

        [Test]
        public void Test_BadRequestIfEvaluatorPasswordIsWrong()
        {
            SetupEvaluatorDB();

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

        private void SetupAdminDB()
        {
            _dbmock.Setup(x => x.TableAdministrators).ReturnsDbSet(new List<TableAdministrator>() {
                      new TableAdministrator() { Email = "admin", Password = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8" }
                  });
        }

        private void SetupEvaluatorDB()
        {
            _dbmock.Setup(x => x.TableKnowledgeTests).ReturnsDbSet(new List<TableKnowledgeTest>() {
                      new TableKnowledgeTest() { EvaluatorPassword = "password" }
                  });
        }

        private void SetupParticipantDB()
        {
            _dbmock.Setup(x => x.TableTestpersons).ReturnsDbSet(new List<TableTestperson>() {
                      new TableTestperson() { SybosId = 1337, Password = "password" }
                  });
        }
    }
}
