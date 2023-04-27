using Domain.Interfaces;
using Domain.Models;
using Domain.ValueObjects;
using ServicesChatGPT.Enums;
using ServicesChatGPT.Interfaces;
using ServicesChatGPT.Models.Completion;
using ServicesChatGPT.Models.Shared;
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
    }
}
