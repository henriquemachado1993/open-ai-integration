using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Chat
{
    public class ChatHistory
    {
        public List<Chat> Chat { get; set; } = new List<Chat>();
    }

    public class Chat
    {
        public DateTime Date { get; set; }
        public string Messagem { get; set; }
        /// <summary>
        /// É o usuário da resposta?
        /// </summary>
        public bool IsReplyUser { get; set; }

        /// <summary>
        /// Identifica se é um áudio
        /// </summary>
        public bool IsAudio { get; set; }
        public FileAudio FileAudio { get; set; }
    }

    public class FileAudio
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string AudioInBase64 { get; set; }
    }
}
