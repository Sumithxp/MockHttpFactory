using AutoFixture;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MyService.Tests
{
    public class MockFakeService
    {
        public Mock<HttpMessageHandler> handlerMock;
        public Mock<IHttpClientFactory> mockHttpClientFactory;
        public MockFakeService()
        {
            var fixture = new Fixture();
            var testUri = fixture.Create<Uri>();
            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            HttpResponseMessage result = new HttpResponseMessage();
            var fakeProduct = new Product
            {
                Id = 1,
                Title = "Fake Title",
                Price = 1
            };

            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
              )
              .ReturnsAsync(new HttpResponseMessage
              {
                  StatusCode = System.Net.HttpStatusCode.OK,
                  Content = new StringContent(JsonSerializer.Serialize(fakeProduct)),
              })
              .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }
    }
}
