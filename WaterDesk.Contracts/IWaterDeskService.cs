using Tuya.Net.Data;

namespace WaterDesk.Contracts;

public interface IWaterDeskService
{
    Task<List<Device>> GetDevicesAsync();

    Task GetDeviceInfoAsync();
    Task<bool> ToggleDeviceSwitchAsync();
}