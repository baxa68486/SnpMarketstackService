using Newtonsoft.Json;
using SnpMarketstackService.DTOs;
using SnpMarketstackService.StreamService;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SnpMarketstackService
{
    public class StreamHandler : IStreamHandler
    {
        public async Task<T> DeserializeJsonFromStreamAsync<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            var res = await JsonSerializer.DeserializeAsync<T>(stream);
            return res;
        }

        public async Task<Stream> ReadAsStreamAsync(HttpResponseMessage responseMessage)
        {
            return await responseMessage.Content.ReadAsStreamAsync();
        }

        public async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }
    }
}
