using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectInterviewProject.Models
{
    [JsonObject]
    public class UploadFileResponse
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public string FileName { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public FileStatus Status { get; set; }
    }
}
