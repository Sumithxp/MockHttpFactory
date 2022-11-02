using System.Text.Json;

namespace MyService;


public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
}


public interface IService
{
    Task<Product> GetProduct();
}

public class Service : IService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public Service(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

    }

    private HttpClient CreateClient() { return _httpClientFactory.CreateClient(); }
    public async Task<Product> GetProduct()
    {
        var httpClient = CreateClient();
        var httpResponseMessage = await httpClient.GetAsync($"https://fakestoreapi.com/products/1");
        httpResponseMessage.EnsureSuccessStatusCode();

        using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<Product>(contentStream);

    }
}

