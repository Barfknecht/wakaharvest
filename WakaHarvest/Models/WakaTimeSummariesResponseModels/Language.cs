using Newtonsoft.Json;

namespace WakaHarvest.Models.WakaTimeSummariesResponseModels
{
    public class Language    {
        [JsonProperty("digital")]
        public string Digital { get; set; } 

        [JsonProperty("hours")]
        public int Hours { get; set; } 

        [JsonProperty("minutes")]
        public int Minutes { get; set; } 

        [JsonProperty("name")]
        public string Name { get; set; } 

        [JsonProperty("percent")]
        public double Percent { get; set; } 

        [JsonProperty("seconds")]
        public int Seconds { get; set; } 

        [JsonProperty("text")]
        public string Text { get; set; } 

        [JsonProperty("total_seconds")]
        public double TotalSeconds { get; set; } 
    }
}