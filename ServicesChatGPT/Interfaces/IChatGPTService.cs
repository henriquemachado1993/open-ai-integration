using Domain.ValueObjects;
using ServicesChatGPT.Models.Completion;
using ServicesChatGPT.Models.Transcriptions;

namespace ServicesChatGPT.Interfaces
{
    public interface IChatGPTService
    {
        /// <summary>
        /// Cria uma resposta de modelo para a conversa de bate-papo fornecida.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BusinessResult<ChatCompletionResponse>> CompletionsAsync(ChatCompletionRequest request);
        /// <summary>
        /// Transcreve o áudio para o idioma de entrada.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BusinessResult<TranscriptionsResponse>> TranscriptionsAsync(TranscriptionsRequest request);
    }
}
