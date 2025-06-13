using Microsoft.AspNetCore.SignalR;

namespace IA_AbansiBabayiSystemBlazor.Hubs
{
    public class AutoUpdateHub : Hub
    {
        public async Task BroadcastUpdate(string tableName)
        {
            await Clients.All.SendAsync("ReceiveUpdate", tableName);
        }
    }
}