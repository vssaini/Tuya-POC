namespace Tuya.App.Contracts;

public interface ITuyaService
{
    Task GetDeviceInfoAsync();
    Task<bool> ToggleDeviceSwitchAsync();
}