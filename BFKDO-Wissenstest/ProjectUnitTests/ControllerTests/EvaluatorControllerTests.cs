using Common.Model;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using WebAPI.Controllers;

namespace ProjectUnitTests.ControllerTests
{
    public class EvaluatorControllerTests
    {

        Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        Mock<BfkdoDbContext> _dbmock = new Mock<BfkdoDbContext>();

        [SetUp]
        public void Setup()
        {
            _configurationMock.Setup(x => x["Issuer"]).Returns("1HDJUZfokBS8Ps3OPvMfjjT3UXHyda6p");
            _configurationMock.Setup(x => x["Audience"]).Returns("Rm9GB255guYnOETGsIRjddxyZZfflSkh");
            _configurationMock.Setup(x => x["Key"]).Returns("3W68B4i5NoTs5VWkRpjGVgGJvu2ue780");

            _dbmock.Setup(x => x.TableEvaluationCriterias).ReturnsDbSet(new List<TableEvaluationCriteria>() {
                      new TableEvaluationCriteria() { Key= 0, CriteriaName = "1" },
                      new TableEvaluationCriteria() { Key= 1, CriteriaName = "2" },
                      new TableEvaluationCriteria() { Key= 2, CriteriaName = "2" }
                  });

            _dbmock.Setup(x => x.TableEvaluations).ReturnsDbSet(new List<TableEvaluation>() {
                      new TableEvaluation() { Id = 1 },
                  });
        }

        [Test]
        public void SubmitEvaluation_ReturnsOk()
        {
            var controller = new EvaluatorController(_dbmock.Object);

            var result = controller.SubmitEvaluation(new(1, Common.Enums.EnumEvaluation.Passed));

            Assert.IsNotNull(result);
            Assert.That(result is OkObjectResult);
        }

        [Test]
        public void SubmitEvaluation_InvalidId_ReturnsError()
        {
            var controller = new EvaluatorController(_dbmock.Object);

            try
            {
                var result = controller.SubmitEvaluation(new(2, Common.Enums.EnumEvaluation.Passed));
            }
            catch (Exception ex)
            {
                Assert.That(ex is InvalidOperationException);
            }
        }


        [Test]
        public void SubmitEvaluation_GradePersisted()
        {
            int evalId = 1;
            Common.Enums.EnumEvaluation evaluation = Common.Enums.EnumEvaluation.Passed;

            var controller = new EvaluatorController(_dbmock.Object);

            var result = controller.SubmitEvaluation(new(evalId, evaluation));

            Assert.IsNotNull(result);
            Assert.That(result is OkObjectResult);

            var grade = _dbmock.Object.TableEvaluations.Single(e => e.Id == evalId);

            Assert.IsNotNull(grade);
            Assert.That(grade.Evaluation == evaluation);
            Assert.That(grade.Evaluation != Common.Enums.EnumEvaluation.Failed);
            Assert.That(grade.Evaluation != Common.Enums.EnumEvaluation.Ungraded);
        }

        [Test]
        public void CloseEvaluation_ReturnsOk()
        {
            var controller = new EvaluatorController(_dbmock.Object);

            var result = controller.CloseEvaluation(new() { 1 });

            Assert.IsNotNull(result);
            Assert.That(result is OkObjectResult);
        }

        [Test]
        public void CloseEvaluation_InvalidId_ReturnsError()
        {
            var controller = new EvaluatorController(_dbmock.Object);

            try
            {
                var result = controller.CloseEvaluation(new() { 1 });
            }
            catch (Exception ex)
            {
                Assert.That(ex is InvalidOperationException);
            }
        }


        [Test]
        public void CloseEvaluation_StatePersisted()
        {
            int evalId = 1;

            var controller = new EvaluatorController(_dbmock.Object);

            var grade = _dbmock.Object.TableEvaluations.Single(e => e.Id == evalId);
            Assert.That(grade.EvaluationState == Common.Enums.EnumEvaluationState.Open);

            var result = controller.CloseEvaluation(new() { evalId });

            Assert.IsNotNull(result);
            Assert.That(result is OkObjectResult);

            grade = _dbmock.Object.TableEvaluations.Single(e => e.Id == evalId);

            Assert.IsNotNull(grade);
            Assert.That(grade.EvaluationState == Common.Enums.EnumEvaluationState.Closed);
        }
    }
}
