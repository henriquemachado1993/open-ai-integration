using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChatResponse
    {
        /// <summary>
        /// Pergunta
        /// </summary>
        public QuestionAnswer Question { get; set; }

        /// <summary>
        /// Resposta
        /// </summary>
        public QuestionAnswer Answer { get; set; }

    }

    public class QuestionAnswer
    {
        public DateTime Date { get; set; }
        public string Messagem { get; set; }
    }
}
