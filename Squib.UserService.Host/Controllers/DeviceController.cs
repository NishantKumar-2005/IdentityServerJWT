using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Squib.UserService.API.ChatApp;
using Squib.UserService.API.model;

namespace Squib.UserService.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IHubContext<TrackingHub> _hubContext;

    public DeviceController(IHubContext<TrackingHub> hubContext)
    {
        _hubContext = hubContext;
    }

    // API to update device location
    [HttpPost("update-location")]
    public async Task<IActionResult> UpdateLocation([FromBody] DeviceLocationDto deviceLocation)
    {
        // Notify connected clients via SignalR
        await _hubContext.Clients.All.SendAsync("ReceiveLocation", deviceLocation.DeviceId, deviceLocation.Latitude, deviceLocation.Longitude);
        return Ok();
    }
    }
}
