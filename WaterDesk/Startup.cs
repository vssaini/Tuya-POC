﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Tuya.Net.Data;
using WaterDesk.Contracts;
using WaterDesk.Models;
using WaterDesk.Services;

namespace WaterDesk;

internal static class Startup
{
    public static IHost CreateHostBuilder()
    {
        var builder = GetConfigBuilder();
        var configuration = builder.Build();

        ConfigureLogger(configuration);

        var appName = configuration.GetSection("ApplicationName").Value;
        Log.Logger.Information("Starting {ApplicationName} application", appName);

        return GetHost(configuration);
    }

    private static ConfigurationBuilder GetConfigBuilder()
    {
        var builder = new ConfigurationBuilder();

        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddUserSecrets<Main>()
            .AddEnvironmentVariables();

        return builder;
    }

    private static void ConfigureLogger(IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();
    }

    private static IHost GetHost(IConfiguration config)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.BindSettings(config);
                services.AddAutoMapper();
                services.AddSingleton<Main>();
                services.AddTransient<IWaterDeskService, WaterDeskService>();
            })
            .UseSerilog()
            .Build();

        return host;
    }

    private static void BindSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<TuyaSetting>(options => config.GetSection(Constants.TuyaSectionName).Bind(options));
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Device, DeviceDto>()
                .ForMember(x => x.TimeUpdated, opt => opt.MapFrom(src => src.TimeUpdated.ToDateTime()))
                .ForMember(d => d.DeviceId, opt => opt.MapFrom(src => src.Id));
        });

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }
}