using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Tuya.App;
using Tuya.App.Services;

var host = Startup.CreateHostBuilder();

var tuyaSvc = ActivatorUtilities.CreateInstance<TuyaService>(host.Services);
await tuyaSvc.Run();

// Necessary; otherwise logs will not show in Seq
Log.CloseAndFlush();