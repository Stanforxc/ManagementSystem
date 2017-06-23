using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadMessaging.Messages
{
    public class Message1 : BaseMessage
    {
        public readonly int DelayInMilliSeconds;

        public Message1(int delayInMilliSeconds)
            : base(MessageType.Message1)
        {
            DelayInMilliSeconds = delayInMilliSeconds;
        }
    }
}
