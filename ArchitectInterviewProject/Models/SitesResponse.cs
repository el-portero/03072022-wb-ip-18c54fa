using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectInterviewProject.Models
{
    [JsonObject]
    public class SitesResponse
    {
        [JsonProperty]
        public IEnumerable<Site> Sites { get; set; }
    }
}
