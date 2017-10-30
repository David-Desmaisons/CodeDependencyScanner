namespace CodeVizualization.ViewModel.Infra
{
    public class Message
    {
        public string Type { get; }
        public string Title { get; }
        public string Content { get; }

        private Message(MessageType type, string title, string message)
        {
            Type = type.ToString().ToLower();
            Title = title;
            Content = message;
        }

        public static Message Error(string title, string message)
        {
            return new Message(MessageType.Error, title, message);
        }
    }

    public enum MessageType
    {
        Error,
        Warn,
        Success
    }
}
