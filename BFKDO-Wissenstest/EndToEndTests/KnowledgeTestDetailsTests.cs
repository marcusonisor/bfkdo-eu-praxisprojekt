using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using MudBlazor;
using System.Text.RegularExpressions;

namespace EndToEndTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest
    {
        private string _url;

        [SetUp]
        public void Setup()
        {
            _url = "http://localhost:5131";
        }

        [Test]
        public async Task Test_HasRightTitle()
        { 
            await Page.GotoAsync(_url);

            await Expect(Page).ToHaveTitleAsync("AdminApp");

            var header = Page.GetByLabel("Seitentitel");
            await Expect(header).ToHaveTextAsync("BFKDO Wissenstest - Admin App");
        }

        [Test]
        public async Task Test_ShowsDetailPageHeader()
        {
            await Page.GotoAsync(_url);

            var header = Page.GetByLabel("Testjahr");
            await Expect(header).ToHaveTextAsync("Testjahr 2022");

            var editbutton = Page.GetByLabel("Wissenstest bearbeiten");
            await Expect(editbutton).ToHaveTextAsync("Wissenstest bearbeiten");

            var pdfbutton = Page.GetByLabel("Pdf exportieren");
            await Expect(pdfbutton).ToHaveTextAsync("PDF exportieren");
        }

        [Test]
        public async Task Test_ShowsTable()
        {
            await Page.GotoAsync(_url);

            var headername = Page.GetByRole(AriaRole.Columnheader, new() { Name = "Name" });
            await Expect(headername).ToHaveTextAsync("Name");

            var headerstation = Page.GetByRole(AriaRole.Columnheader, new() { Name = "Station" });
            await Expect(headerstation).ToHaveTextAsync("Station");
        }
    }
}