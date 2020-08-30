using System.Collections.Generic;
using Newtonsoft.Json;

namespace WakaHarvest.Models.WakaTimeSummariesResponseModels
{
    public class Datum    {
        [JsonProperty("branches")]
        public List<Branch> Branches { get; set; } 

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; } 

        [JsonProperty("dependencies")]
        public List<object> Dependencies { get; set; } 

        [JsonProperty("editors")]
        public List<Editor> Editors { get; set; } 

        [JsonProperty("entities")]
        public List<Entity> Entities { get; set; } 

        [JsonProperty("grand_total")]
        public GrandTotal GrandTotal { get; set; } 

        [JsonProperty("languages")]
        public List<Language> Languages { get; set; } 

        [JsonProperty("machines")]
        public List<Machine> Machines { get; set; } 

        [JsonProperty("operating_systems")]
        public List<OperatingSystem> OperatingSystems { get; set; } 

        [JsonProperty("range")]
        public Range Range { get; set; } 
    }
}