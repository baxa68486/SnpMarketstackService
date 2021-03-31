using SnpMarketstackService.Settings;
using System.Threading.Tasks;

namespace SnpMarketstackService.Http.Services
{
    public interface IMarketStackService<T>
    {
        Task<T> GetMarketStackIndexes(string marketStackIndexProviderUrl, 
                                      HttpHeaderSetting httpHeaderSetting);
    }
}
