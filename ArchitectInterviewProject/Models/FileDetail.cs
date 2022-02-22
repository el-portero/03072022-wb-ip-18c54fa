using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectInterviewProject.Models
{
    [JsonObject]
    public class FileDetail
    {
        [JsonProperty]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the uploaded file name.
        /// </summary>
        [JsonProperty]
        public string FileName { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public FileStatus Status { get; set; }

        /// <summary>
        /// The total number of rows in the file (excluding the header)
        /// </summary>
        [JsonProperty]
        public uint TotalRecordCount { get; set; }

        /// <summary>
        /// The total number of valid rows in the file.
        /// </summary>
        [JsonProperty]
        public uint ValidRecordCount { get; set; }

        /// <summary>
        /// The total number of invalid rows in the file.
        /// </summary>
        [JsonProperty]
        public uint InvalidRecordCount { get; set; }
    }
}
