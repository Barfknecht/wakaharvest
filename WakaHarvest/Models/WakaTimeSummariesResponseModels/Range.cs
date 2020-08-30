using System;
using Newtonsoft.Json;

namespace WakaHarvest.Models.WakaTimeSummariesResponseModels
{
    public class Range    {
        [JsonProperty("date")]
        public string Date { get; set; } 

        [JsonProperty("end")]
        public DateTime End { get; set; } 

        [JsonProperty("start")]
        public DateTime Start { get; set; } 

        [JsonProperty("text")]
        public string Text { get; set; } 

        [JsonProperty("timezone")]
        public string Timezone { get; set; } 
    }
}