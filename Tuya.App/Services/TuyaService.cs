using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Tuya.App.Contracts;
using Tuya.App.Models;
using Tuya.Net;
using Tuya.Net.Data.Settings;

namespace Tuya.App.Services;

public class TuyaService : ITuyaService
{
    private readonly ILogger<TuyaService> _logger;
    private readonly TuyaSetting _tuyaSetting;

    public TuyaService(IOptions<TuyaSetting> options, ILogger<TuyaService> logger)
    {
        _logger = logger;
        _tuyaSetting = options.Value;
    }

    public async Task Run()
    {
        await GetDeviceInformationAsync(_tuyaSetting.DeviceId);
    }

    private async Task GetDeviceInformationAsync(string deviceId)
    {
        _logger.LogInformation("Retrieving information and status of device {DeviceId}", deviceId);

        var client = GetTuyaClient();
        var device = await client.DeviceManager.GetDeviceAsync(deviceId);

        _logger.LogInformation("Device information: {@Device}", device);
    }

    private ITuyaClient GetTuyaClient()
    {
        var client = TuyaClient.GetBuilder()
            .UsingDataCenter(DataCenter.WestUs)
            .UsingClientId(_tuyaSetting.ClientId)
            .UsingSecret(_tuyaSetting.ClientSecret)
            .UsingLogger(NullLogger<ITuyaClient>.Instance)
            .Build();

        return client;
    }
}

