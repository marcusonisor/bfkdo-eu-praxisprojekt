using AdminApp;
using AdminApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace AdminApp
{
    /// <summary>
    ///     Program Class of App.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Entrypoint of Program Class.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddMudServices();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7250") });
            builder.Services.AddScoped<CommunicationService>();
            await builder.Build().RunAsync();
        }
    }
}