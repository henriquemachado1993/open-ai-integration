using ServicesChatGPT.Interfaces;
using ServicesChatGPT.Models.Completion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChatGPT.Services
{
    public class ChatGPTService : IChatGPTService
    {
        public async Task<ChatCompletionResponse> CompletionsAsync(ChatCompletionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
