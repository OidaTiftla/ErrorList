namespace ErrorList {

    public class MessageItem : IMessageItem {
        public virtual string Description { get; set; }
        public virtual ErrorListLevel Level { get; set; }
    }
}