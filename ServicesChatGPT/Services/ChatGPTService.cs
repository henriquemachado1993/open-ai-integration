using Domain.ValueObjects;
using ServicesChatGPT.Helper;
using ServicesChatGPT.Interfaces;
using ServicesChatGPT.Models.Completion;
using System.Net.Http;
using Domain.Extensions;

namespace ServicesChatGPT.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly string _baseUrl = ChatGPTHelper.GetBaseUrl();

        private static HttpClient _httpClient;
        private static HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());

        public async Task<BusinessResult<ChatCompletionResponse>> CompletionsAsync(ChatCompletionRequest request)
        {
            var response = await HttpClient.PostAsync(
                $"{_baseUrl}/v1/chat/completions",
                request,
                ChatGPTHelper.GetJsonMediaTypeFormatter(),
                ChatGPTHelper.GetHeader()
            );

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsJsonAsync<ChatCompletionResponse>();
                return BusinessResult<ChatCompletionResponse>.CreateValidResult(result);
            }
            else
            {
                var contentError = string.Empty;
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var streamReader = new StreamReader(responseStream))
                    contentError = await streamReader.ReadToEndAsync();

                return BusinessResult<ChatCompletionResponse>.CreateInvalidResult(response.StatusCode, $"Error: {contentError}");
            }
        }
    }
}
