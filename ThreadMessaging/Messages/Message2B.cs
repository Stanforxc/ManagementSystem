using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadMessaging.Messages
{
    public class Message2B :BaseMessage
    {
        public readonly int SequenceNumber;

        /// <summary>
        /// The name of the managed thread sending this message
        /// </summary>
        public readonly string ThreadName;

        /// <summary>
        /// Constructor
        /// </summary>
        public Message2B(int sequenceNumber, string threadName)
            : base(MessageType.Message2B)
        {
            SequenceNumber = sequenceNumber;
            ThreadName = threadName;
        }
    }
}
