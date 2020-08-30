using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WakaHarvest.Models.WakaTimeSummariesResponseModels
{
    public class Root    {
        [JsonProperty("available_branches")]
        public List<string> AvailableBranches { get; set; } 

        [JsonProperty("branches")]
        public List<object> Branches { get; set; } 

        [JsonProperty("data")]
        public List<Datum> Data { get; set; } 

        [JsonProperty("end")]
        public DateTime End { get; set; } 

        [JsonProperty("start")]
        public DateTime Start { get; set; } 
    }
}