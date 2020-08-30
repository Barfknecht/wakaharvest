using System.Collections.Generic;
using System.Threading.Tasks;
using WakaHarvest.Models.WakaTimeSummariesResponseModels;
using WakaHarvest.Settings.Options;

namespace WakaHarvest.Services
{
    public interface IHarvestService
    {
        Task CreateNewHarvestTimeEntries(ProjectOptions project, List<Branch> branches);
    }
}