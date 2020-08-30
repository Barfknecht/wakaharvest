using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WakaHarvest.Services;
using WakaHarvest.Settings.Options;

namespace WakaHarvest
{
    public class WakaHarvestFunction
    {
        private readonly IWakaTimeService _wakaTimeService;
        private readonly IHarvestService _harvestService;
        private readonly List<ProjectOptions> _projects;

        public WakaHarvestFunction(IWakaTimeService wakaTimeService, IHarvestService harvestService,
            IOptions<UserOptions> userOptions)
        {
            _wakaTimeService = wakaTimeService ?? throw new ArgumentException(nameof(IWakaTimeService));
            _harvestService = harvestService ?? throw new ArgumentNullException(nameof(IHarvestService));
            _projects = userOptions.Value.Projects;
        }

        [FunctionName("WakaHarvestFunction")]
        public async Task  RunAsync([TimerTrigger("0 0 23 * * *", RunOnStartup = false)]
            TimerInfo myTimer, ILogger log)
        {
            await Task.WhenAll(_projects.Select(project => 
            {
                var userSummary =  _wakaTimeService.GetWakaTimeUserSummary(project.Name).Result;
                var projectBranch = userSummary.Data.First().Branches;
                log.LogInformation($"Recording time for {project.Name}");
                var taskResult =  _harvestService.CreateNewHarvestTimeEntries(project, projectBranch);
                log.LogInformation($"Recorded time for {project.Name}");
                return taskResult;
            }));
        }
    }
}