using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServicesChatGPT.Models.Transcriptions
{
    public class TranscriptionsRequest
    {
        public Stream File { get; set; }
        public string Model { get; set; }

        [JsonIgnore]
        public IFormFile FormFile { get; set; }
    }
}
