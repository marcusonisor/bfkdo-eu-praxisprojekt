using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using MudBlazor;
using System.Text.RegularExpressions;

namespace EndToEndTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class BenutzerTests : PageTest
    {
        private string _url;

        [SetUp]
        public void Setup()
        {
            _url = "http://localhost:5194";
        }

        [Test]
        public async Task Test_HasRightTitle()
        {
            await Page.GotoAsync(_url);

            await Expect(Page).ToHaveTitleAsync("BenutzerApp");
        }

        [Test]
        public async Task Test_HasRightLogos()
        {
            await Page.GotoAsync(_url);

            var bfkdologo = Page.GetByAltText("Bezirksfeuerwehrkommando Eisenstadt-Umgebung Logo");
            Expect(bfkdologo);

            var fhb = Page.GetByAltText("FH Burgenland Logo");
            Expect(fhb);
        }

        [Test]
        public async Task Test_HasThemeSwitch()
        {
            await Page.GotoAsync(_url);

            var themeswitch = Page.GetByLabel("Theme ändern");
            Expect(themeswitch);

        }

        [Test]
        public async Task Test_ShowStartScreen()
        {
            await Page.GotoAsync(_url);

            var bewerter = Page.GetByLabel("Als Testbewerter einloggen");
            Expect(bewerter);

            var testperson = Page.GetByLabel("Als Testperson einloggen");
            Expect(testperson);
        }

        [Test]
        public async Task Test_ShowsLoginForm()
        {
            await Page.GotoAsync(_url);

            var bewerter = Page.GetByLabel("Als Testbewerter einloggen");
            Expect(bewerter);
            await bewerter.ClickAsync();

            await Expect(Page).ToHaveURLAsync(new Regex("authevaluator"));

            var password = Page.GetByLabel("Passwort für Login");
            Expect(password);

            var loginbtn = Page.GetByLabel("Anmelde-Button");
            Expect(loginbtn);
        }

        [Test]
        public async Task Test_LoginAndLogout()
        {
            await Page.GotoAsync(_url);

            var bewerter = Page.GetByLabel("Als Testbewerter einloggen");
            Expect(bewerter);
            await bewerter.ClickAsync();

            await Expect(Page).ToHaveURLAsync(new Regex("authevaluator"));

            var password = Page.GetByLabel("Passwort für Login");
            Expect(password);
            await password.FillAsync("rmRj3s");

            var loginbtn = Page.GetByLabel("Anmelde-Button");
            Expect(loginbtn);
            await loginbtn.ClickAsync();

            await Expect(Page).ToHaveURLAsync(new Regex("stationchange"));

            var logout = Page.GetByLabel("Logout");
            Expect(logout);
            await logout.ClickAsync();
            await Expect(Page).ToHaveURLAsync($"{_url}/");
        }
    }
}
