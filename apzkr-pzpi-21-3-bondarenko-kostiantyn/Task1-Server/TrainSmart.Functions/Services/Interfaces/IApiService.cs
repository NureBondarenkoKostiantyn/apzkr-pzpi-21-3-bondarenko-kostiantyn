namespace TrainSmart.Functions.Services.Interfaces;

public interface IApiService
{
    Task<HttpResponseMessage> PostAsync<T>(
        string requestUri,
        T input);
}