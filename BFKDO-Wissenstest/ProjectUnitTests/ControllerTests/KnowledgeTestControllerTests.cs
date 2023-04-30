using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectUnitTests.HelperTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using ILogger = Microsoft.Extensions.Logging;

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
            Assert.That(response is BadRequestResult);
        }

        [Test]
        public void Test_OkIfIdIsValid()
        {
            var controller = new KnowledgeTestController(null!, _logger);
            var response = controller.GetKnowledgeTestDetails(2);
            Assert.That(response is OkObjectResult);
        }
    }
}
