using SnpMarketstackService.DTOs;
using SnpMarketstackService.Exceptions;
using SnpMarketstackService.Services;
using SnpMarketstackService.Settings;
using SnpMarketstackService.StreamService;
using System.Threading.Tasks;

namespace SnpMarketstackService.Http.Services
{
    public class MarketStackService : IMarketStackService<SPIndexDTO>
    {
        private readonly IHttpClientHandler _httpClientHandler;
        private readonly IStreamHandler _streamHandler;

        public MarketStackService(IHttpClientHandler httpClientHandler, IStreamHandler streamHandler)
        {
            _httpClientHandler = httpClientHandler;
            _streamHandler = streamHandler;
        }

        public async Task<SPIndexDTO> GetMarketStackIndexes(string marketStackIndexProviderUrl, 
                                                            HttpHeaderSetting httpHeaderSetting)
        {
            using (var response = await _httpClientHandler.GetAsync(marketStackIndexProviderUrl, httpHeaderSetting))
            {
                var stream = await _streamHandler.ReadAsStreamAsync(response);

                if (response.IsSuccessStatusCode)
                    return await _streamHandler.DeserializeJsonFromStreamAsync<SPIndexDTO>(stream);

                var content = await _streamHandler.StreamToStringAsync(stream);

                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };

            }
        }
    }
}
