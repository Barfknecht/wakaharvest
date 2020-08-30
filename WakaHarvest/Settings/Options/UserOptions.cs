using System.Collections.Generic;

namespace WakaHarvest.Settings.Options
{
    public class UserOptions
    {
        public List<ProjectOptions> Projects { get; set; }
        public HarvestOptions HarvestOptions { get; set; }
        public WakaTimeOptions WakaTimeOptions { get; set; }
    }
}