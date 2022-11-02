using Moq;
using NUnit.Framework;
using System.Net.Http;

namespace MyService.Tests;

public class Tests
{
    private Mock<HttpMessageHandler> handlerMock;
    private Mock<IHttpClientFactory> mockHttpClientFactory;
    private Service _service;

    [SetUp]
    public void Setup()
    {

        var mockServiceslingapps = new MockFakeApi();
        handlerMock = mockServiceslingapps.handlerMock;
        mockHttpClientFactory = mockServiceslingapps.mockHttpClientFactory;
        _service = new Service(mockHttpClientFactory.Object);
    }

    [Test]
    public void Get_Product_Should_Return_With_OK()
    {
        var result = _service.GetProduct();

        Assert.That(result, Is.Not.Null);
    }
}