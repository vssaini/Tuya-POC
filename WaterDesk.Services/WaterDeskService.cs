using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Tuya.Net;
using Tuya.Net.Data;
using Tuya.Net.Data.Settings;
using WaterDesk.Contracts;
using WaterDesk.Models;
using WaterDesk.Models.Exceptions;

namespace WaterDesk.Services;

public class WaterDeskService : IWaterDeskService
{
    private readonly ILogger<WaterDeskService> _logger;
    private readonly IMapper _mapper;
    private readonly TuyaSetting _tuya;

    public WaterDeskService(IOptions<TuyaSetting> options, ILogger<WaterDeskService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _tuya = options.Value;
    }

    public async Task<IList<DeviceDto>> GetDevicesAsync()
    {
        var client = GetTuyaClient();
        var devices = await client.DeviceManager.GetDevicesByUserAsync(_tuya.UserId);

        if (devices == null)
        {
            _logger.LogWarning("No devices found.");
            return new List<DeviceDto>();
        }

        _logger.LogInformation("Retrieved {TotalDevices} devices", devices.Count);
        var deviceDtos = _mapper.Map<List<DeviceDto>>(devices);

        return deviceDtos.OrderBy(d => d.Name).ToList();
    }

    public async Task<DeviceDto> GetDeviceInfoAsync()
    {
        _logger.LogInformation("Retrieving information and status of device {DeviceId}", _tuya.DeviceId);

        var client = GetTuyaClient();
        var device = await client.DeviceManager.GetDeviceAsync(_tuya.DeviceId);

        return _mapper.Map<DeviceDto>(device);
    }

    public async Task<bool> ToggleDeviceSwitchAsync(string deviceId)
    {
        var client = GetTuyaClient();

        var device = await client.DeviceManager.GetDeviceAsync(deviceId);
        if (device == null)
            throw new NotFoundException($"No device found for device id {deviceId}.");

        var status = device.StatusList?.FirstOrDefault(ds => ds.Code == "switch");
        if (status?.Value is not bool)
            throw new Exception("Cannot obtain the value of the device status, the switch status did not return bool as expected.");

        var isTurnedOn = (bool)status.Value!;
        _logger.LogInformation("Device {DeviceId} is currently turned {SwitchStatus}", deviceId, isTurnedOn ? "on" : "off");

        // Create the command to send an instruction to manipulate the device status
        var command = new Command
        {
            Code = "switch",
            Value = !isTurnedOn
        };

        // Send the command and obtain the result from the server.
        var result = await client.DeviceManager.SendCommandAsync(device, command);

        if (result)
            _logger.LogInformation("Successfully toggled device {DeviceId} switch to {SwitchStatus}", deviceId, !isTurnedOn);
        else
            _logger.LogError("Failed to toggle device {DeviceId} switch to {SwitchStatus}", deviceId, !isTurnedOn);

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

