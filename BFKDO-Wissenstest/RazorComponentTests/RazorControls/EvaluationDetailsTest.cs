using Bunit;
using NUnit.Framework;
using RazorClassLibrary;

namespace RazorComponentTests.RazorControls
{
    public class EvaluationDetailsTest : Bunit.TestContext
    {
        [Test]
        public void Test_RenderNothingIfNoParameterPassed()
        {
            var cut = RenderComponent<EvaluationDetails>();

            cut.MarkupMatches(string.Empty);
        }

        [Test]
        public void Test_RenderGrayTextIfNotGradedYet()
        {
            var result = "3 / 5";
            var cut = RenderComponent<EvaluationDetails>(options =>
            {
                options.Add(x => x.State, Common.Enums.EnumEvaluation.Ungraded);
                options.Add(x => x.Result, result);
            });

            cut.MarkupMatches($"<label style=\"color:gray\">{result}</label>");
        }

        [Test]
        public void Test_RenderGreenTextIfPassed()
        {
            var result = "3 / 5";
            var cut = RenderComponent<EvaluationDetails>(parameters =>
            {
                parameters.Add(x => x.State, Common.Enums.EnumEvaluation.Passed);
                parameters.Add(x => x.Result, result);
            });

            cut.MarkupMatches($"<label style=\"color:green\">{result}</label>");
        }

        [Test]
        public void Test_RenderRedTextIfFailed()
        {
            var result = "3 / 5";
            var cut = RenderComponent<EvaluationDetails>(parameters =>
            {
                parameters.Add(x => x.State, Common.Enums.EnumEvaluation.Failed);
                parameters.Add(x => x.Result, result);
            });

            cut.MarkupMatches($"<label style=\"color:red\">{result}</label>");
        }
    }
}
