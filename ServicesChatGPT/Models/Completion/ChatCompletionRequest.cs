using ServicesChatGPT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChatGPT.Models.Completion
{
    public class ChatCompletionRequest
    {
        /// <summary>
        /// ID of the model to use. See the model endpoint compatibility (https://platform.openai.com/docs/models/model-endpoint-compatibility) table for details on which models work with the Chat API.
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// A list of messages describing the conversation so far.
        /// </summary>
        public List<Message> Messages { get; set; }
    }
}
