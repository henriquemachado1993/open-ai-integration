using ChatGPTIntegration.Models.Message;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Chat;
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

        [HttpPost]
        public IActionResult SendMessageAudio(MessageRequestModel request)
        {
            _chatHistoryService.AddAsync(new Chat()
            {
                IsReplyUser = false,
                Date = DateTime.Now,
                IsAudio = true,
                Messagem = $"Arquivo enviado para transcrição: {request.FormFile?.FileName}",
                FileAudio = new FileAudio() { Name = request.FormFile?.FileName ?? "", Extension = request.FormFile?.ContentType ?? "", AudioInBase64 = ConvertIFormFileToBase64(request.FormFile) }
            });

            if (request.FormFile == null)
            {
                _chatHistoryService.AddAsync(new Chat() { IsError = true, IsReplyUser = true, Messagem = "Você deve anexar um arquivo de áudio. Formatos permitidos: mp3, mp4, mpeg, mpga, m4a, wav, or webm.", Date = DateTime.Now });
                return PartialView("_ChatHistory", _chatHistoryService.GetAsync());
            }

            // Pesquisa no chat
            var responseService = _chatService.TranscriptionsAsync(new ChatRequest() { FormFile = request.FormFile }).Result;

            // Trata retorno com erro
            if (!responseService.IsValid)
            {
                _chatHistoryService.AddAsync(new Chat() { IsError = true, IsReplyUser = true, Messagem = string.Join(" | ", responseService.Messages.Select(x => x.Message)), Date = DateTime.Now });
                return PartialView("_ChatHistory", _chatHistoryService.GetAsync());
            }

            _chatHistoryService.AddAsync(new Chat() { IsReplyUser = true, Messagem = responseService.Data.Answer.Messagem, Date = DateTime.Now });

            return PartialView("_ChatHistory", _chatHistoryService.GetAsync());
        }

        private string ConvertIFormFileToBase64(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();
            return Convert.ToBase64String(bytes);
        }
    }
}