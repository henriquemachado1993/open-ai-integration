using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServicesChatGPT.Models.Error
{
    public class RootError
    {
        [JsonPropertyName("error")]
        public Error error { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("message")]
        public string message { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }
    }
}
