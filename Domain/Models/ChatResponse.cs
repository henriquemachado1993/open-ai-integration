using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChatResponse
    {
        public string KeyChat { get; set; }
        public QuestionAnswer UserQuestion { get; set; }
        public QuestionAnswer ChatAnswer { get; set; }

    }

    public class QuestionAnswer
    {
        public DateTime Date { get; set; }
        public string Messagem { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
