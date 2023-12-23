using Budget.Persistance.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace Budget.Persistance.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication) => webApplication.MapHub<PriceHub>("/price-hub");
        
    }
}
