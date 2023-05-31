using Common.Model;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using WebAPI.Controllers;

namespace ProjectUnitTests.ControllerTests
{
    public class KnowledgeTestControllerTests
    {
        private ILogger<KnowledgeTestController> _logger;
        [SetUp]
        public void Setup()
        {
            _logger = Mock.Of<ILogger<KnowledgeTestController>>();
        }

        [Test]
        public void Test_BadRequestIfIdIsInvalid()
        { 
            var controller = new KnowledgeTestController(null!,_logger);
            var response = controller.GetKnowledgeTestDetails(0);
            Assert.That(response is BadRequestObjectResult);
        }

        [Test]
        public void Test_BadRequestIfKnowledgeTestNotFound()
        {
            var dbmock = new Mock<BfkdoDbContext>();
            dbmock.Setup(x => x.TableKnowledgeTests).ReturnsDbSet(new List<TableKnowledgeTest>());
            var controller = new KnowledgeTestController(dbmock.Object, _logger);
            var response = controller.GetKnowledgeTestDetails(1);
            Assert.That(response is BadRequestObjectResult);
        }

        [Test]
        public void Test_OkWithEmptyListIfTableIsEmpty()
        {
            var dbmock = new Mock<BfkdoDbContext>();
            dbmock.Setup(x => x.TableKnowledgeTests).ReturnsDbSet(new List<TableKnowledgeTest>());
            var controller = new KnowledgeTestController(dbmock.Object, _logger); 
            var response = controller.GetKnowledgeTests();
            if(response is OkObjectResult result)
            {
                if(result.Value is List<ModelKnowledgeTest> items)
                {
                    Assert.That(items.Count == 0);
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void Test_OkWithThreeItemsIfTableContainsThreeItems()
        {
            var dbmock = new Mock<BfkdoDbContext>();
            dbmock.Setup(x => x.TableKnowledgeTests).ReturnsDbSet(new List<TableKnowledgeTest>()
            {
                new()
                {
                    Id = 1,
                    Designation = "Testjahr 2021"
                },
                new()
                {
                    Id = 2,
                    Designation = "Testjahr 2022"
                },
                new()
                {
                    Id = 3,
                    Designation = "Testjahr 2023"
                }
            });
            var controller = new KnowledgeTestController(dbmock.Object, _logger);
            var response = controller.GetKnowledgeTests();
            if (response is OkObjectResult result)
            {
                if (result.Value is List<ModelKnowledgeTest> items)
                {
                    var ids = new int[] { 1, 2, 3 };
                    Assert.That(items.Select(t => t.Id).All(t => ids.Contains(t)));
                    Assert.That(items[0].Designation == "Testjahr 2021");
                    Assert.That(items[1].Designation == "Testjahr 2022");
                    Assert.That(items[2].Designation == "Testjahr 2023");
                }
                else
                {
                    Assert.Fail();
                }
            }
        }
    }
}
