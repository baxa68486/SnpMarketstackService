using SnpMarketstackService.Services;
using SnpMarketstackService.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SnpMarketstackService.Http
{
    public class HttpClientHandler : IHttpClientHandler, IDisposable
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public HttpClient HttpClientManager => _httpClient;


        public HttpResponseMessage Get(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        public void SetHttpHeaderSettings(HttpHeaderSetting headerSettings)
        {
            _httpClient.BaseAddress = new Uri(headerSettings.BaseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerSettings.MediaType));
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
