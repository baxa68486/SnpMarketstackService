using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SnpMarketstackService.StreamService
{
    public interface IStreamHandler
    {
        Task<T> DeserializeJsonFromStreamAsync<T>(Stream stream);
        Task<Stream> ReadAsStreamAsync(HttpResponseMessage responseMessage);
        Task<string> StreamToStringAsync(Stream stream);
    }
}
