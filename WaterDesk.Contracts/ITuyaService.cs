﻿namespace WaterDesk.Contracts;

public interface ITuyaService
{
    Task GetDeviceInfoAsync();
    Task<bool> ToggleDeviceSwitchAsync();
}