using System;
using Microsoft.AspNetCore.SignalR;

namespace Squib.UserService.API.ChatApp;

public class TrackingHub : Hub
{
    public async Task SendLocation(string deviceId, double latitude, double longitude)
    {
        await Clients.All.SendAsync("ReceiveLocation", deviceId, latitude, longitude);
    }
}
