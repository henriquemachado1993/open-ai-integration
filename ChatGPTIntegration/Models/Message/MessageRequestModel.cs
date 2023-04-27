namespace ChatGPTIntegration.Models.Message
{
    public class MessageRequestModel
    {
        public string Message { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
