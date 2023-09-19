using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Tuya.App.Contracts;
using Tuya.App.Models;
using Tuya.Net;
using Tuya.Net.Data;
using Tuya.Net.Data.Settings;

namespace Tuya.App.Services;

public class TuyaService : ITuyaService
{
    private readonly ILogger<TuyaService> _logger;
    private readonly TuyaSetting _tuya;

    public TuyaService(IOptions<TuyaSetting> options, ILogger<TuyaService> logger)
    {
        _logger = logger;
        _tuya = options.Value;
    }

    public async Task GetDeviceInfoAsync()
    {
        _logger.LogInformation("Retrieving information and status of device {DeviceId}", _tuya.DeviceId);

        var client = GetTuyaClient();
        var device = await client.DeviceManager.GetDeviceAsync(_tuya.DeviceId);

        _logger.LogInformation("Device information: {@Device}", device);
    }

    public async Task<bool> ToggleDeviceSwitchAsync()
    {
        var client = GetTuyaClient();

        var device = await client.DeviceManager.GetDeviceAsync(_tuya.DeviceId);
        var status = device?.StatusList?.FirstOrDefault(ds => ds.Code == "switch");

        if (status?.Value is not bool)
            throw new Exception("Cannot obtain the value of the device status, the switch status did not return bool as expected.");

        // Get the device status (true if the device is turned on, otherwise false)
        var isTurnedOn = (bool)status.Value!;

        // Create the command to send an instruction to manipulate the device status
        var command = new Command
        {
            Code = "switch",
            Value = !isTurnedOn
        };

        // Send the command and obtain the result from the server.
        var result = await client.DeviceManager.SendCommandAsync(device, command);

        if (result)
            _logger.LogInformation("Successfully toggled device {DeviceId} switch to {SwitchStatus}", _tuya.DeviceId, !isTurnedOn);
        else
            _logger.LogError("Failed to toggle device {DeviceId} switch to {SwitchStatus}", _tuya.DeviceId, !isTurnedOn);

        return result;
    }

    private ITuyaClient GetTuyaClient()
    {
        var client = TuyaClient.GetBuilder()
            .UsingDataCenter(DataCenter.WestUs)
            .UsingClientId(_tuya.ClientId)
            .UsingSecret(_tuya.ClientSecret)
            .UsingLogger(NullLogger<ITuyaClient>.Instance)
            .Build();

        return client;
    }
}

