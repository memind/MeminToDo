using Budget.Application.Abstractions.Hubs;
using Budget.Persistance.Consts;
using Budget.Persistance.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Budget.Persistance.SignalR.HubService
{
    public class PriceHubService : IPriceHubService
    {
        readonly IHubContext<PriceHub> _hubContext;

        public PriceHubService(IHubContext<PriceHub> hubContext) => _hubContext = hubContext;

        public async Task GetPricesAsync()
        {
            var client = new HttpClient();
            double[] prices = new double[3];

            var usdRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=USD&to=TRY&amount=1"),
                Headers =
                {
                    { "X-RapidAPI-Key", "" },
                    { "X-RapidAPI-Host", "" },
                },
            };

            var eurRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=EUR&to=TRY&amount=1"),
                Headers =
                {
                    { "X-RapidAPI-Key", "" },
                    { "X-RapidAPI-Host", "" },
                },
            };

            var btcRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=BTC&to=TRY&amount=1"),
                Headers =
                {
                    { "X-RapidAPI-Key", "" },
                    { "X-RapidAPI-Host", "" },
                },
            };

            using (var usdResponse = await client.SendAsync(usdRequest))
            {
                usdResponse.EnsureSuccessStatusCode();
                var body = await usdResponse.Content.ReadAsStringAsync();

                dynamic obj = JsonConvert.DeserializeObject(body);
                var price = obj.info.rate.ToObject<double>();

                prices[0] = price;
            }

            using (var eurResponse = await client.SendAsync(eurRequest))
            {
                eurResponse.EnsureSuccessStatusCode();
                var body = await eurResponse.Content.ReadAsStringAsync();

                dynamic obj = JsonConvert.DeserializeObject(body);
                var price = obj.info.rate.ToObject<double>();

                prices[1] = price;
            }

            using (var btcResponse = await client.SendAsync(btcRequest))
            {
                btcResponse.EnsureSuccessStatusCode();
                var body = await btcResponse.Content.ReadAsStringAsync();

                dynamic obj = JsonConvert.DeserializeObject(body);
                var price = obj.info.rate.ToObject<double>();

                prices[2] = price;
            }


            await _hubContext.Clients.All.SendAsync(SignalRConsts.GetPrices(), prices);
        }
    }
}
