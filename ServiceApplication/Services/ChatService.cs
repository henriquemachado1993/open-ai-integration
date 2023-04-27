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
            request.UserInfo = new UserInfo() { Name = "Usuário XPTO" };

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
                            Name = request.UserInfo.Name,
                        }
                    }
                });

            if (!response.IsValid)
                return BusinessResult<ChatResponse>.CreateInvalidResult(response.Messages.Select(x => x.Message));

            var responseChatGpt = response.Data.Choices?.Select(x => x.Message)?.FirstOrDefault()?.Content ?? string.Empty;

            var chatResponse = new ChatResponse()
            {
                UserQuestion = new QuestionAnswer() { Messagem = request.Message, UserInfo = new UserInfo() { Name = request.UserInfo.Name } },
                ChatAnswer = new QuestionAnswer() { Messagem = responseChatGpt, UserInfo = new UserInfo() { Name = "ChatGPT" } }
            };

            return BusinessResult<ChatResponse>.CreateValidResult(chatResponse);
        }
    }
}
