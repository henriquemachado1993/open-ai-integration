using ChatGPTIntegration.Models;
using ChatGPTIntegration.Models.Chat;
using ChatGPTIntegration.Models.Message;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatGPTIntegration.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Eu mesmo
        /// </summary>
        private UserInfo ISelf = new UserInfo() { Id = "Me123", Name = "Henrique machado" };

        private readonly IChatService _chatService;
        private readonly IChatHistoryService _chatHistoryService;

        public HomeController(IChatService chatService, IChatHistoryService chatHistoryService)
        {
            _chatService = chatService;
            _chatHistoryService = chatHistoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Home/GetChatHistory")]
        public async Task<IActionResult> GetChatHistory()
        {
            return PartialView("_ChatHistory", await _chatHistoryService.GetAsync());
        }

        [HttpPost]
        public IActionResult SendMessage(MessageRequestModel request)
        {
            _chatHistoryService.AddAsync(new Domain.Models.Chat.Chat() { IsReplyUser = false, Date = new DateTime(), Messagem = request.Message });

            var responseChatGPT = _chatService.CompletionsAsync(new ChatRequest() { Message = request.Message, UserInfo = ISelf }).Result;
            
            if (!responseChatGPT.IsValid)
            {
                _chatHistoryService.AddAsync(new Domain.Models.Chat.Chat() { IsReplyUser = true, Messagem = string.Join(" | ", responseChatGPT.Messages.Select(x => x.Message)), Date = new DateTime() });

                return PartialView("_ChatHistory", _chatHistoryService.GetAsync());
            }

            // Colocar uma validação de erro na resposta
            _chatHistoryService.AddAsync(new Domain.Models.Chat.Chat() { IsReplyUser = true, Messagem = responseChatGPT.Data.ChatAnswer.Messagem, Date = new DateTime() });

            return PartialView("_ChatHistory", _chatHistoryService.GetAsync());
        }
    }
}