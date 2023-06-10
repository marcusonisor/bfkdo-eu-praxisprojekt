using BenutzerApp.Services;
using Blazored.LocalStorage;
using Common.Helper;
using Common.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace BenutzerApp
{
    /// <summary>
    ///     Program Class of App.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Entrypoint of Programm Class.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateService>();
#if DEBUG
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(WebConstants.LocalApiAddress) });
#else
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(WebConstants.AzureApiAddress) });
#endif
            builder.Services.AddScoped<EvaluatorService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ParticipantService>();


            await builder.Build().RunAsync();
        }
    }
}