using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WakaHarvest.Models.WakaTimeSummariesResponseModels;
using WakaHarvest.Providers;
using WakaHarvest.Settings.Options;

namespace WakaHarvest.Services.Implementations
{
    public class WakaTimeService : IWakaTimeService
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly UserOptions _userOptions;

        public WakaTimeService(IHttpClientProvider httpClientProvider, IOptions<UserOptions> userOptions)
        {
            _httpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(IHttpClientProvider));
            _userOptions = userOptions.Value;
        }

        public async Task<Root> GetWakaTimeUserSummary(string project)
        {
            var summaryDate = DateTime.Now.ToString("yyyy-MM-dd");
            var httpClient = _httpClientProvider.GetWakaTimeClient();
            var requestUrl = $"?api_key={_userOptions.WakaTimeOptions.ApiKey}&project={project}&start={summaryDate}&end={summaryDate}";
            var response = await httpClient.GetAsync(requestUrl);
            var stringResponseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Root>(stringResponseBody);
        }
    }
}