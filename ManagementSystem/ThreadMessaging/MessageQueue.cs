using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ThreadMessaging.Messages;

namespace ThreadMessaging
{
    internal class MessageQueue
    {
        private Queue<BaseMessage> msgQueue;
        private ManualResetEvent messageArrivedEvent;
        private object mutexObject;
        private readonly string threadName;
        public MessageQueue(string threadName)
        {
            msgQueue = new Queue<BaseMessage>();
            messageArrivedEvent = new ManualResetEvent(false);
            mutexObject = new object();
            this.threadName = threadName;
        }

        public void SendMessage(BaseMessage msg)
        {
            lock (mutexObject)
            {
                msgQueue.Enqueue(msg);
                messageArrivedEvent.Set();
            }
        }
        public BaseMessage GetMessage()
        {
            BaseMessage msg = null;
            if (msgQueue.Count == 0)
            {
                messageArrivedEvent.WaitOne();
            }
            lock (mutexObject)
            {
                msg = msgQueue.Dequeue();
                messageArrivedEvent.Reset();
            }
            return msg;
        }

        public BaseMessage GetMessage(int waitTime)
        {
            BaseMessage msg = null;
            if (msgQueue.Count > 0 || messageArrivedEvent.WaitOne(waitTime) == true)
            {
                lock (mutexObject)
                {
                    msg = msgQueue.Dequeue();
                    messageArrivedEvent.Reset();
                }
            }
            return msg;
        }
    }
}
