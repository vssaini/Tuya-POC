using WaterDesk.Models.Enums;

namespace WaterDesk.Models;

public class DeviceDto
{
    public string DeviceId { get; set; }
    public string Name { get; set; }
    public string Ip { get; set; }
    public bool IsOnline { get; set; }
    public DateTime TimeUpdated { get; set; }
    public DeviceCategory Category { get; set; }
}