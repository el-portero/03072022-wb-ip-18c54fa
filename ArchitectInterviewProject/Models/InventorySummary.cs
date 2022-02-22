using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectInterviewProject.Models
{
    [JsonObject]
    public class InventorySummary
    {
        /// <summary>
        /// The total number of unique NDCs at this site.
        /// </summary>
        [JsonProperty]
        public uint UniqueDrugCount { get; set; }

        /// <summary>
        /// The total inventory value at this site.
        /// </summary>
        [JsonProperty]
        public decimal TotalInventoryValue { get; set; }

        /// <summary>
        /// The most expensive drug (based on unit cost) at this site.
        /// If there are ties, take the drug with the biggest NDC.
        /// </summary>
        [JsonProperty]
        public SiteItem HighestUnitCostSiteItem { get; set; }

        /// <summary>
        /// The drug with the highest number of units at this site.
        /// If there are ties, take the drug with the biggest NDC.
        /// </summary>
        [JsonProperty]
        public SiteItem HighestInventoryUnitsSiteItem { get; set; }

        /// <summary>
        /// The drug with the higest inventory value (units * unit cost)
        /// If there are ties, take the drug with the biggest NDC.
        /// </summary>
        [JsonProperty]
        public SiteItem HighestInventoryValueSiteItem { get; set; }

        /// <summary>
        /// The least expensive drug (based on unit cost) at this site.
        /// If there are ties, take the drug with the lowest NDC.
        /// </summary>
        [JsonProperty]
        public SiteItem LowestUnitCostSiteItem { get; set; }

        /// <summary>
        /// The drug with the lowest number of units at this site.
        /// If there are ties, take the drug with the lowest NDC.
        /// </summary>
        [JsonProperty]
        public SiteItem LowestInventoryUnitsSiteItem { get; set; }

        /// <summary>
        /// The drug with the lowest inventory value (units * unit cost)
        /// If there are ties, take the drug with the lowest NDC.
        /// </summary>
        [JsonProperty]
        public SiteItem LowestInventoryValueSiteItem { get; set; }

        public override bool Equals(object obj)
        {
            var summary = obj as InventorySummary;
            return summary != null &&
                   UniqueDrugCount == summary.UniqueDrugCount &&
                   TotalInventoryValue == summary.TotalInventoryValue &&
                   EqualityComparer<SiteItem>.Default.Equals(HighestUnitCostSiteItem, summary.HighestUnitCostSiteItem) &&
                   EqualityComparer<SiteItem>.Default.Equals(HighestInventoryUnitsSiteItem, summary.HighestInventoryUnitsSiteItem) &&
                   EqualityComparer<SiteItem>.Default.Equals(HighestInventoryValueSiteItem, summary.HighestInventoryValueSiteItem) &&
                   EqualityComparer<SiteItem>.Default.Equals(LowestUnitCostSiteItem, summary.LowestUnitCostSiteItem) &&
                   EqualityComparer<SiteItem>.Default.Equals(LowestInventoryUnitsSiteItem, summary.LowestInventoryUnitsSiteItem) &&
                   EqualityComparer<SiteItem>.Default.Equals(LowestInventoryValueSiteItem, summary.LowestInventoryValueSiteItem);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(UniqueDrugCount);
            hash.Add(TotalInventoryValue);
            hash.Add(HighestUnitCostSiteItem);
            hash.Add(HighestInventoryUnitsSiteItem);
            hash.Add(HighestInventoryValueSiteItem);
            hash.Add(LowestUnitCostSiteItem);
            hash.Add(LowestInventoryUnitsSiteItem);
            hash.Add(LowestInventoryValueSiteItem);
            return hash.ToHashCode();
        }

        public static bool operator ==(InventorySummary summary1, InventorySummary summary2)
        {
            return EqualityComparer<InventorySummary>.Default.Equals(summary1, summary2);
        }

        public static bool operator !=(InventorySummary summary1, InventorySummary summary2)
        {
            return !(summary1 == summary2);
        }
    }
}
