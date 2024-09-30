using System;

namespace Squib.UserService.API;

public class DeviceLocationDto
{
    public string DeviceId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

}
