using Bunit;
using NUnit.Framework;
using RazorClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorComponentTests.RazorControls
{
    internal class ScanHelperTests : Bunit.TestContext
    {
        [Test]
        public void Test_ComponentInitRight()
        {
            var cut = RenderComponent<ScanHelper>();
            var element = cut.Find("button");
            Assert.That(element != null!);
            if (element != null!)
            {
                Assert.That(element.TextContent.Equals("Scannen", StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test_StartScanner()
        {
            var cut = RenderComponent<ScanHelper>();
            cut.Find("button").Click();
            var buttonsafterstart = cut.FindAll("button");
            Assert.That(buttonsafterstart.Count == 2);
            Assert.That(buttonsafterstart.Any(t => t.TextContent.Equals("Kamera", StringComparison.CurrentCultureIgnoreCase)));
            Assert.That(buttonsafterstart.Any(t => t.TextContent.Equals("Stoppen", StringComparison.CurrentCultureIgnoreCase)));
        }

        [Test]
        public void Test_StopScannerAfterStarted()
        {
            var cut = RenderComponent<ScanHelper>();
            cut.Find("button").Click();
            var buttonsafterstart = cut.FindAll("button");
            var stop = buttonsafterstart.FirstOrDefault(t => t.TextContent.Equals("Stoppen", StringComparison.CurrentCultureIgnoreCase));
            if (stop == null)
            {
                Assert.Fail();
                return;
            }
            stop.Click();
            var element = cut.Find("button");
            Assert.That(element != null!);
            if (element != null!)
            {
                Assert.That(element.TextContent.Equals("Scannen", StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
