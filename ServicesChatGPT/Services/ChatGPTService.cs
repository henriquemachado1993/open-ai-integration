using Domain.ValueObjects;
using ServicesChatGPT.Helper;
using ServicesChatGPT.Interfaces;
using ServicesChatGPT.Models.Completion;
using System.Net.Http;
using Domain.Extensions;
using Newtonsoft.Json;
using ServicesChatGPT.Models.Error;
using ServicesChatGPT.Models.Transcriptions;
using System.Net.Http.Headers;

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
                {
                    var jsonContentError = await streamReader.ReadToEndAsync();
                    var error = JsonConvert.DeserializeObject<RootError>(jsonContentError);
                    contentError = error?.error?.message;
                }
               
                return BusinessResult<ChatCompletionResponse>.CreateInvalidResult(response.StatusCode, $"Error: {contentError}");
            }
        }

        public async Task<BusinessResult<TranscriptionsResponse>> TranscriptionsAsync(TranscriptionsRequest request)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ChatGPTHelper.GetApiKey());

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(request.File), "file", request.FormFile.FileName);
            content.Add(new StringContent("whisper-1"), "model");

            var response = await HttpClient.PostAsync($"{_baseUrl}/v1/audio/transcriptions", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsJsonAsync<TranscriptionsResponse>();
                return BusinessResult<TranscriptionsResponse>.CreateValidResult(result);
            }
            else
            {
                var contentError = string.Empty;
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var streamReader = new StreamReader(responseStream))
                {
                    var jsonContentError = await streamReader.ReadToEndAsync();
                    var error = JsonConvert.DeserializeObject<RootError>(jsonContentError);
                    contentError = error?.error?.message;
                }

                return BusinessResult<TranscriptionsResponse>.CreateInvalidResult(response.StatusCode, $"Error: {contentError}");
            }
        }
    }
}
