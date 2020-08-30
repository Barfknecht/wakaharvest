namespace WakaHarvest.Constants
{
    public static class ApiConstants
    {
        public static string WakaTimeApiBaseUrl = "https://wakatime.com/api/v1";

        public static string WakaTimeUserSummaryUrl = "/users/current/summaries";

        public static string HarvestApiBaseUrl = "https://api.harvestapp.com/v2/";

        public static string HarvestTimeEntryUrl = "time_entries";
        
        public enum RequestType
        {
            WakaTime,
            Harvest
        }
    }
}