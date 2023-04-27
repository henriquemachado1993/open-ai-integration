using Domain.Interfaces;
using Domain.Models;
using Domain.ValueObjects;
using ServicesChatGPT.Enums;
using ServicesChatGPT.Interfaces;
using ServicesChatGPT.Models.Completion;
using ServicesChatGPT.Models.Shared;
using ServicesChatGPT.Models.Transcriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApplication.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatGPTService _chatGPTService;

        public ChatService(IChatGPTService chatGPTService)
        {
            _chatGPTService = chatGPTService;
        }

        public async Task<BusinessResult<ChatResponse>> CompletionsAsync(ChatRequest request)
        {
            var response = await _chatGPTService.CompletionsAsync(
                new ChatCompletionRequest()
                {
                    Model = "gpt-3.5-turbo",
                    Messages = new List<Message>()
                    {
                        new Message()
                        {
                            Role = Role.user.ToString(),
                            Content = request.Message,
                        }
                    }
                });

            if (!response.IsValid)
                return BusinessResult<ChatResponse>.CreateInvalidResult(response.Messages.Select(x => x.Message));

            var responseChatGpt = response.Data.Choices?.Select(x => x.Message)?.FirstOrDefault()?.Content ?? "Nada foi respondido pelo ChatGPT";

            var chatResponse = new ChatResponse()
            {
                Question = new QuestionAnswer() { Messagem = request.Message },
                Answer = new QuestionAnswer() { Messagem = responseChatGpt }
            };

            return BusinessResult<ChatResponse>.CreateValidResult(chatResponse);
        }

        public async Task<BusinessResult<ChatResponse>> TranscriptionsAsync(ChatRequest request)
        {
            // verificar se formato que foi passado está de acordo
            var listFormtsValid = new List<string>()
            {
                ".mp3", ".mp4", ".mpeg", ".mpga", ".m4a",".wav" ,".webm"
            };
            var extensionFile = Path.GetExtension(request.FormFile.FileName);
            if (!listFormtsValid.Contains(extensionFile))
                return BusinessResult<ChatResponse>.CreateInvalidResult("Não foi possível realizar a transcrição. Formatos de arquivos permitidos: mp3, mp4, mpeg, mpga, m4a, wav, or webm");
            
            var response = await _chatGPTService.TranscriptionsAsync(
                new TranscriptionsRequest()
                {
                    Model = "whisper-1",
                    File = request.FormFile.OpenReadStream(),
                    FormFile = request.FormFile
                });

            if (!response.IsValid)
                return BusinessResult<ChatResponse>.CreateInvalidResult(response.Messages.Select(x => x.Message));

            var responseChatGpt = response.Data.Text ?? "Nada foi respondido pelo ChatGPT";

            var chatResponse = new ChatResponse()
            {
                Question = new QuestionAnswer() { Messagem = request.Message },
                Answer = new QuestionAnswer() { Messagem = $"Transcrição: {responseChatGpt}" }
            };

            return BusinessResult<ChatResponse>.CreateValidResult(chatResponse);
        }
    }
}
