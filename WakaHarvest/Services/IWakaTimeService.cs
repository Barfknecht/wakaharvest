using System.Threading.Tasks;
using WakaHarvest.Models.WakaTimeSummariesResponseModels;

namespace WakaHarvest.Services
{
    public interface IWakaTimeService
    {
        Task<Root> GetWakaTimeUserSummary(string project);
    }
}