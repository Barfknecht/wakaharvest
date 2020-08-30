using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using WakaHarvest.Settings.Options;

namespace WakaHarvest.Providers.Implementations
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserOptions _userOptions;
        private readonly IRequestUrlProvider _requestUrlProvider;

        public HttpClientProvider(IOptions<UserOptions> userOptions, IHttpClientFactory httpClientFactory, IRequestUrlProvider requestUrlProvider)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(IHttpClientFactory));
            _userOptions = userOptions.Value;
            _requestUrlProvider = requestUrlProvider ?? throw new ArgumentNullException(nameof(IRequestUrlProvider));
        }


        public HttpClient GetWakaTimeClient()
        {
            var baseUrl = _requestUrlProvider.GetWakaTimeUserSummaryRequestUrl();
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUrl);

            return httpClient;
        }

        public HttpClient GetHarvestClient()
        {
            var baseUrl = _requestUrlProvider.GetHarvestTimeEntryRequestUrl();
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Add("Harvest-Account-Id", _userOptions.HarvestOptions.HarvestAccountId);
            httpClient.DefaultRequestHeaders.Add("Authorization", _userOptions.HarvestOptions.AccessToken);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Nicholas Barfknecht (nicholasbarfknecht@gmail.com)");
            return httpClient;
        }
        
    }
}