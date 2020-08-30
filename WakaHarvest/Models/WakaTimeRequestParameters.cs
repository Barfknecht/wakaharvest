using System;

namespace WakaHarvest.Models
{
    public class WakaTimeRequestParameters
    {
        public string Project { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}