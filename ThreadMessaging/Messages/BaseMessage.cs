namespace ThreadMessaging.Messages
{
    public enum MessageType
    {
        StopThread,
        Message1,
        Message2A,
        Message2B
    }
    public abstract partial class BaseMessage
    {
        public readonly MessageType MsgType;
        public BaseMessage(MessageType msgType)
        {
            this.MsgType = msgType;
        }
    }
}
