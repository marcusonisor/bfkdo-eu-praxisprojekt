using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using MudBlazor;
using System.Text.RegularExpressions;

namespace EndToEndTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class AdminTests : PageTest
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
        public async Task Test_ShowsLoginForm()
        {
            await Page.GotoAsync(_url);

            var email = Page.GetByLabel("E-Mail für Login");
            Expect(email);

            var password = Page.GetByLabel("Passwort für Login");
            Expect(password);

            var btn = Page.GetByLabel("Anmelde-Button");
            Expect(btn);
        }

        private async Task Internal_Login()
        {
            await Page.GotoAsync(_url);

            var email = Page.GetByLabel("E-Mail für Login");
            Expect(email);
            await email.FillAsync("admin@bfkdo.com");

            var password = Page.GetByLabel("Passwort für Login");
            Expect(password);
            await password.FillAsync("default");

            var btn = Page.GetByLabel("Anmelde-Button");
            Expect(btn);
            await btn.ClickAsync();

            await Expect(Page).ToHaveURLAsync(new Regex("dashboard"), new PageAssertionsToHaveURLOptions() { Timeout=10000 });
        }

        [Test]
        public async Task Test_CheckLoggedInHeader()
        {
            await Internal_Login();

            var themeswitch = Page.GetByLabel("Theme ändern");
            Expect(themeswitch);

            var dashboard = Page.GetByLabel("Dashboard anzeigen");
            Expect(dashboard);

            var logout = Page.GetByLabel("Logout");
            Expect(logout);
        }

        [Test]
        public async Task Test_LoginAndLogout()
        {
            await Internal_Login();

            var logout = Page.GetByLabel("Logout");
            Expect(logout);
            await logout.ClickAsync();
            await Expect(Page).ToHaveURLAsync($"{_url}/", new PageAssertionsToHaveURLOptions() { Timeout=10000 });
        }

        [Test]
        public async Task Test_ShowDashboard()
        {
            await Internal_Login();

            var createButton = Page.GetByLabel("Test anlegen");
            Expect(createButton);

            var table = Page.GetByLabel("Angelegte Wissenstests");
            Expect(table);
        }
    }
}