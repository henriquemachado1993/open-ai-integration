using Domain.ValueObjects;
using ServicesChatGPT.Models.Completion;

namespace ServicesChatGPT.Interfaces
{
    public interface IChatGPTService
    {
        Task<BusinessResult<ChatCompletionResponse>> CompletionsAsync(ChatCompletionRequest request);
    }
}
