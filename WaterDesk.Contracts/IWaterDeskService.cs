using WaterDesk.Models;

namespace WaterDesk.Contracts;

public interface IWaterDeskService
{
    Task<IList<DeviceDto>> GetDevicesAsync();

    Task<DeviceDto> GetDeviceInfoAsync();
    Task<bool> ToggleDeviceSwitchAsync(string deviceId);
}