namespace ErrorList {

    public class MessageItem : IMessageItem {
        public string Description { get; set; }
        public ErrorListLevel Level { get; set; }
    }
}