using Microsoft.Extensions.DependencyInjection;
using WaterDesk.Helpers;

namespace WaterDesk
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            ErrorHandler.ConfigureGlobalErrorHandling();

            var host = Startup.CreateHostBuilder();
            var services = host.Services;

            var main = services.GetRequiredService<Main>();
            Application.Run(main);
        }
    }
}