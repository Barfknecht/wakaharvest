namespace WakaHarvest.Providers
{
    public interface IRequestUrlProvider
    {
        string GetWakaTimeUserSummaryRequestUrl();

        string GetHarvestTimeEntryRequestUrl();
    }
}