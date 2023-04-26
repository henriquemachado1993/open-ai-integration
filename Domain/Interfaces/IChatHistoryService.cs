using Domain.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChatHistoryService
    {
        Task<ChatHistory> AddAsync(Chat chat);
        Task<ChatHistory> GetAsync();
    }
}
