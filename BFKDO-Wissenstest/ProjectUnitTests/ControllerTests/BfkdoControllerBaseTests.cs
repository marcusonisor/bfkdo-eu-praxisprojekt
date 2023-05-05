using Database;
using Database.Tables;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace ProjectUnitTests.ControllerTests
{
    public class BfkdoControllerBaseTests
    {
        private BfkdoControllerBase _controller;
        private TableTestperson _testperson;
        private TableKnowledgeTest _knowledgeTest;


        [SetUp]
        public void Setup()
        {
            _controller = new BfkdoControllerBase();
            _testperson = new TableTestperson()
            {
                FireDepartmentBranch = "FF Musterstadt",
                Id = 1,
                FirstName = "Max",
                LastName = "Mustermann"
            };

            _knowledgeTest = new TableKnowledgeTest()
            {
                Designation = "Testjahr 2022",
                Id = 1
            };
        }

        [Test]
        public void Test_EmptyListIfKnowledgeTestIsNull()
        {
            var list = _controller.GetResultsForTestPerson(null!, _testperson);
            Assert.That(list, Is.Empty);
        }

        [Test]
        public void Test_EmptyListIfTestPersonIsNull()
        {
            var list = _controller.GetResultsForTestPerson(_knowledgeTest, null!);
            Assert.That(list, Is.Empty);
        }

        [Test]
        public void Test_GetPersonTestResult()
        {
            var extendedKnowledgeTest = new TableKnowledgeTest()
            {
                Id = 1,
                Designation = "Testjahr 2022",
                Registrations = new List<TableRegistration>()
                {
                    new TableRegistration()
                    {
                        Id = 1,
                        TestpersonId = 1,
                        KnowledgeLevel = new TableKnowledgeLevel()
                        {
                            Id = 1,
                            Description = "Stufe 1"
                        },
                        Evaluations = new List<TableEvaluation>()
                        {
                            new TableEvaluation()
                            {
                                Id = 1,
                                Evaluation = Common.Enums.EnumEvaluation.Passed,
                                EvaluationState = Common.Enums.EnumEvaluationState.Closed,
                                RegistrationId = 1,
                            },
                            new TableEvaluation()
                            {
                                Id = 2,
                                Evaluation = Common.Enums.EnumEvaluation.Passed,
                                EvaluationState = Common.Enums.EnumEvaluationState.Closed,
                                RegistrationId = 1,
                            },
                            new TableEvaluation()
                            {
                                Id = 3,
                                Evaluation = Common.Enums.EnumEvaluation.Passed,
                                EvaluationState = Common.Enums.EnumEvaluationState.Closed,
                                RegistrationId = 1,
                            }
                        }
                    }
                }
            };

            var controller = new BfkdoControllerBase();
            var result = controller.GetResultsForTestPerson(extendedKnowledgeTest, _testperson);
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(result.FirstOrDefault()!.LevelName, Is.EqualTo("Stufe 1"));
                Assert.That(result.FirstOrDefault()!.LevelResult, Is.EqualTo("3 / 3"));
            });
        }
    }
}
