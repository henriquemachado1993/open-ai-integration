namespace ChatGPTIntegration.Models.Chat
{
    public class QuestionAnswerModel
    {
        public DateTime Date { get; set; }
        public string Messagem { get; set; }
        /// <summary>
        /// É o usuário da resposta?
        /// </summary>
        public bool IsReplyUser { get; set; }
    }
}
