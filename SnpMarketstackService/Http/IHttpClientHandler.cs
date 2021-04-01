using SnpMarketstackService.Settings;
using System.Net.Http;
using System.Threading.Tasks;

namespace SnpMarketstackService.Services
{
    public interface IHttpClientHandler
    {
        HttpResponseMessage Get(string url);
        Task<HttpResponseMessage> GetAsync(string url);
        void SetHttpHeaderSettings(HttpHeaderSetting headerSettings);
    }
}
