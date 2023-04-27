using ChatGPTIntegration.Models.Message;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatGPTIntegration.Controllers
{
    public class HomeController : Controller
    {
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

        [HttpGet]
        public async Task<IActionResult> GetChatHistory()
        {
            return PartialView("_ChatHistory", await _chatHistoryService.GetAsync());
        }

        [HttpPost]
        public IActionResult SendMessage(MessageRequestModel request)
        {
            _chatHistoryService.AddAsync(new Domain.Models.Chat.Chat() { IsReplyUser = false, Date = DateTime.Now, Messagem = request.Message });

            // Pesquisa no chat
            var responseService = _chatService.CompletionsAsync(new ChatRequest() { Message = request.Message }).Result;
            
            // Trata retorno com erro
            if (!responseService.IsValid)
            {
                _chatHistoryService.AddAsync(new Domain.Models.Chat.Chat() { IsReplyUser = true, Messagem = string.Join(" | ", responseService.Messages.Select(x => x.Message)), Date = DateTime.Now });
                return PartialView("_ChatHistory", _chatHistoryService.GetAsync());
            }

            _chatHistoryService.AddAsync(new Domain.Models.Chat.Chat() { IsReplyUser = true, Messagem = responseService.Data.Answer.Messagem, Date = DateTime.Now });

            return PartialView("_ChatHistory", _chatHistoryService.GetAsync());
        }
    }
}