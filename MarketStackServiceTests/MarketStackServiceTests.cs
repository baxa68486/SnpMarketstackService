using Moq;
using SnpMarketstackService.DTOs;
using SnpMarketstackService.Exceptions;
using SnpMarketstackService.Http.Services;
using SnpMarketstackService.Services;
using SnpMarketstackService.Settings;
using SnpMarketstackService.StreamService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MarketStackServiceTests
{
    public class MarketStackServiceTests
    {
        [Fact]
        public async Task GetMarketStackIndexes_HtttReponseNotContainsError_ReturnsValidContent()
        {
            Mock<IHttpClientHandler> _mockHttpClientHandler = new Mock<IHttpClientHandler>();
            Mock<IStreamHandler> _mockStreamHandler = new Mock<IStreamHandler>();

           _mockHttpClientHandler.Setup(httpClient => httpClient.GetAsync(It.IsAny<string>()))
                                                                 .ReturnsAsync(CreateHttpResponseMessage(HttpStatusCode.OK));

           _mockStreamHandler.Setup(stream => stream.DeserializeJsonFromStreamAsync<SPIndexDTO>(It.IsAny<Stream>()))
                                                    .ReturnsAsync(new SPIndexDTO()
                                                    {
                                                        Data = new List<SPIndexValueDTO>()
                                                        {
                                                           CreateMockedSPIndexValueDTO(3, 4),
                                                           CreateMockedSPIndexValueDTO(6, 7)
                                                        },
                                                        Pagination = CreateMockedSPIndexPaginationDTO(2, 100, 15)
                                                    });

            MarketStackService marketStackService = new MarketStackService(_mockHttpClientHandler.Object, _mockStreamHandler.Object);
            
            HttpHeaderSetting setting = CreateHttpHeaderSetting("https://marketstack.com/", "testKey", "json");
            var url = "api/testURL";
            var res = await marketStackService.GetMarketStackIndexes(url, setting);
            Assert.Equal(2, res.Data.Count);

            Assert.Equal(3, res.Data[0].Low);
            Assert.Equal(4, res.Data[0].High);
            Assert.Equal(6, res.Data[1].Low);
            Assert.Equal(7, res.Data[1].High);
        }

        [Fact]
        public async Task GetMarketStackIndexes_HtttReponseContainsError_ThrowsCustomException()
        {
            Mock<IHttpClientHandler> _mockHttpClientHandler = new Mock<IHttpClientHandler>();
            Mock<IStreamHandler> _mockStreamHandler = new Mock<IStreamHandler>();

            _mockHttpClientHandler.Setup(httpClient => httpClient.GetAsync(It.IsAny<string>()))
                                                                  .ReturnsAsync(CreateHttpResponseMessage(HttpStatusCode.InternalServerError));

            MarketStackService marketStackService = new MarketStackService(_mockHttpClientHandler.Object, _mockStreamHandler.Object);

            HttpHeaderSetting setting = CreateHttpHeaderSetting("https://marketstack.com/", "testKey", "json");
            var url = "api/testURL";
            const int internalServerError = 500;
            
            Func<Task<SPIndexDTO>> deleg = () => marketStackService.GetMarketStackIndexes(url, setting);

            var exceptionDetails = await Assert.ThrowsAsync<ApiException>(deleg);
            Assert.Equal(exceptionDetails.StatusCode, internalServerError);
        }

        private SPIndexValueDTO CreateMockedSPIndexValueDTO(int low, int high) =>
                                                        new SPIndexValueDTO()
                                                        {
                                                            Low = low,
                                                            High = high
                                                        };

        private SPIndexPaginationDTO CreateMockedSPIndexPaginationDTO(int count, int total, int limit) =>
                                                        new SPIndexPaginationDTO()
                                                        {
                                                            Count = count,
                                                            Total = total,
                                                            Limit = limit
                                                        };

        private HttpHeaderSetting CreateHttpHeaderSetting(string baseAddress, string key, string mediaType) =>
                                                          new HttpHeaderSetting()
                                                          {
                                                              BaseAddress = baseAddress,
                                                              Key = key,
                                                              MediaType = mediaType
                                                          };

        private HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode) => new HttpResponseMessage
                                                              {
                                                                StatusCode = statusCode
                                                              };
    }
}
