using ServicesChatGPT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChatGPT.Models.Completion
{
    public class ChatCompletionResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created { get; set; }
        public List<Choice> Choices { get; set; }
        public Usage Usage { get; set; }
    }
}
