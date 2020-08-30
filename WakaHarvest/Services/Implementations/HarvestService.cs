using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WakaHarvest.Models.WakaTimeSummariesResponseModels;
using WakaHarvest.Providers;
using WakaHarvest.Settings.Options;

namespace WakaHarvest.Services.Implementations
{
    public class HarvestService : IHarvestService
    {
        private readonly HttpClient _httpClient;
        private readonly UserOptions _userOptions;

        public HarvestService(IHttpClientProvider httpClientProvider, IOptions<UserOptions> userOptions)
        {
            _userOptions = userOptions.Value;
            _httpClient = httpClientProvider.GetHarvestClient();
        }

        public async Task CreateNewHarvestTimeEntries(ProjectOptions project, List<Branch> branches)
        {
            var summaryDate = DateTime.Now.ToString("yyyy-MM-dd");
            await Task.WhenAll(branches.Select(branch =>
            {
                var requestMessage = CreateHarvestRequestMessage(branch, project, summaryDate);
                return ShouldTimeBeRecorded(branch) ? CreateNewHarvestTimeEntry(requestMessage) : Task.CompletedTask;
            }));

            // Local Functions

            static bool ShouldTimeBeRecorded(Branch branch) => branch.Minutes > 1;
        }

        private string CreateHarvestRequestMessage(Branch branch, ProjectOptions project, string summaryDate)
        {
            var time = Convert.ToDecimal(TimeSpan.Parse(branch.Digital).TotalHours);
            var roundedTime = Math.Round(time, 2, MidpointRounding.ToPositiveInfinity);
            var requestUrl =
                $"?project_id={project.ProjectId}&task_id={project.DevelopmentTaskId}&spent_date={summaryDate}" +
                $"&user_id={_userOptions.HarvestOptions.UserId}&hours={roundedTime}";

            var ticketNumber = branch.Name.Split('.');
            if (IsTicketNumber())
                requestUrl =
                    $"{requestUrl}&external_reference[permalink]={project.ProjectExternalLink}{ticketNumber[2]}&external_reference[id]={ticketNumber[2]}" +
                    $"&external_reference[group_id]=null&external_reference[service]=dev.azure.com&notes={ticketNumber[2]}";
            else
                requestUrl = $"{requestUrl}&notes={branch.Name}";

            return requestUrl;

            // Local Functions

            bool IsTicketNumber() =>
                ticketNumber.Length > 2 && ticketNumber[2].All(char.IsDigit) && ticketNumber[2].Length == 5;
        }

        private async Task CreateNewHarvestTimeEntry(string requestMessage)
        {
            await _httpClient.PostAsync(requestMessage, null);
        }
    }
}