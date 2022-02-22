using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectInterviewProject.Models
{
    public class DriverSite
    {
        public Site Site { get; set; }
        public InventorySummary InventorySummary { get; set; }

        public DriverSite(Site site, InventorySummary inventorySummary)
            => (Site, InventorySummary) = (site, inventorySummary);

        public override bool Equals(object obj)
        {
            var site = obj as DriverSite;
            return site != null &&
                   EqualityComparer<Site>.Default.Equals(Site, site.Site);
        }

        public override int GetHashCode()
        {
            return Site.GetHashCode();
        }

        public static bool operator ==(DriverSite site1, DriverSite site2)
        {
            return EqualityComparer<DriverSite>.Default.Equals(site1, site2);
        }

        public static bool operator !=(DriverSite site1, DriverSite site2)
        {
            return !(site1 == site2);
        }
    }
}
