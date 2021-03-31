using Moq;
using Newtonsoft.Json;
using SnpMarketstackService.DTOs;
using SnpMarketstackService.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace MarketStackServiceTests
{
    public class MarketStackServiceTests
    {
        [Fact]
        public async Task GetStackMarkerStackIndexes()
        {
            string accessKey = "8c35d18f65e26dcac9339ae44e1e7b36";
            var url = "http://api.marketstack.com/v1/eod?access_key";
            var url1 = $"{url}={accessKey}&symbols=AAPL";
            Mock<IHttpClientHandler> _mockUserService = new Mock<IHttpClientHandler>();
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("https://api.com/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(url1);
            string resp = await response.Content.ReadAsStringAsync();
            var res2 = JsonConvert.DeserializeObject<SPIndexDTO>(resp);

            var res = await httpClient.GetAsync(url1) ;
        }
    }
}
