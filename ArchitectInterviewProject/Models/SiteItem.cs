using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectInterviewProject.Models
{
    [JsonObject]
    public class SiteItem
    {
        [JsonProperty]
        public string NDC { get; set; }

        [JsonProperty]
        public uint TotalUnits { get; set; }

        [JsonProperty]
        public decimal UnitCost { get; set; }

        [JsonProperty]
        public decimal TotalValue { get; set; }

        public SiteItem(string ndc, uint totalUnits, decimal unitCost, decimal totalValue)
            => (NDC, TotalUnits, UnitCost, TotalValue) = (ndc, totalUnits, unitCost, totalValue);

        public override bool Equals(object obj)
        {
            var item = obj as SiteItem;
            return item != null &&
                   NDC == item.NDC &&
                   TotalUnits == item.TotalUnits &&
                   UnitCost == item.UnitCost &&
                   TotalValue == item.TotalValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NDC, TotalUnits, UnitCost, TotalValue);
        }

        public static bool operator ==(SiteItem item1, SiteItem item2)
        {
            return EqualityComparer<SiteItem>.Default.Equals(item1, item2);
        }

        public static bool operator !=(SiteItem item1, SiteItem item2)
        {
            return !(item1 == item2);
        }
    }
}
