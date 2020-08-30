using WakaHarvest.Constants;

namespace WakaHarvest.Providers.Implementations
{
    public class RequestUrlProvider : IRequestUrlProvider
    {
        public string GetWakaTimeUserSummaryRequestUrl()
        {
            return $"{ApiConstants.WakaTimeApiBaseUrl}{ApiConstants.WakaTimeUserSummaryUrl}";
        }

        public string GetHarvestTimeEntryRequestUrl()
        {
            return $"{ApiConstants.HarvestApiBaseUrl}{ApiConstants.HarvestTimeEntryUrl}";
        }
    }
}