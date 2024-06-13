using System.Text;
using Newtonsoft.Json;
using TrainSmart.Functions.Services.Interfaces;

namespace TrainSmart.Functions.Services;

public class ApiService: IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<HttpResponseMessage> PostAsync<T>(
        string requestUri,
        T input)
    {
        var requestContent = new StringContent(
            JsonConvert.SerializeObject(input), 
            Encoding.UTF8, 
            "application/json");

        return await _httpClient.PostAsync(requestUri, requestContent);
    }
}