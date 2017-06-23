using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadMessaging.Messages
{
    internal class MessageStopThread : BaseMessage
    {
        public MessageStopThread() : base(MessageType.StopThread)
        {
        }
    }
}
