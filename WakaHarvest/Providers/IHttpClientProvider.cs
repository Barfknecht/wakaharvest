using System.Net.Http;

namespace WakaHarvest.Providers
{
    public interface IHttpClientProvider
    {
        HttpClient GetWakaTimeClient();

        HttpClient GetHarvestClient();
    }
}