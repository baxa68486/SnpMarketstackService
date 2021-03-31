using SnpMarketstackService.Settings;
using System.Net.Http;
using System.Threading.Tasks;

namespace SnpMarketstackService.Services
{
    public interface IHttpClientHandler
    {
        HttpResponseMessage Get(string url, HttpHeaderSetting headerSetting);
        Task<HttpResponseMessage> GetAsync(string url, HttpHeaderSetting headerSetting);
    }
}
