using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WaterDesk.Console;
using WaterDesk.Services;

var host = Startup.CreateHostBuilder();

var tuyaSvc = ActivatorUtilities.CreateInstance<WaterDeskService>(host.Services);
await tuyaSvc.GetDeviceInfoAsync();

// Necessary; otherwise logs will not show in Seq
Log.CloseAndFlush();