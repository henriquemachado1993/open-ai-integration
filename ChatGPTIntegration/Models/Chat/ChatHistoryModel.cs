namespace ChatGPTIntegration.Models.Chat
{
    public class ChatHistoryModel
    {
        public List<QuestionAnswerModel> Chat { get; set; } = new List<QuestionAnswerModel>();
    }
}
