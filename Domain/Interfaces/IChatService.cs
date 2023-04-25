using Domain.Models;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IChatService
    {
        Task<BusinessResult<ChatResponse>> CompletionsAsync(ChatRequest request);
    }
}
