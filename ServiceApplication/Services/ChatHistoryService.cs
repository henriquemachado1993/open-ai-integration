using Domain.Interfaces;
using Domain.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApplication.Services
{
    public class ChatHistoryService : IChatHistoryService
    {
        private ChatHistory _chatHistory;

        public ChatHistoryService()
        {
            _chatHistory = new ChatHistory() { Chat = new List<Chat>() };
        }

        public async Task<ChatHistory> AddAsync(Chat chat)
        {
            _chatHistory.Chat.Add(chat);
            return _chatHistory;
        }

        public async Task<ChatHistory> GetAsync()
        {
            return _chatHistory;
        }
    }
}
