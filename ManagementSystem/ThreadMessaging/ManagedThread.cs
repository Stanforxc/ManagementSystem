using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadMessaging.Messages;

namespace ThreadMessaging
{
    internal class ManagedThread : IThread
    {
        private MessageQueue msgQueue;
        private string name;
        private Action<BaseMessage> messageHandler;
        private int waitTimeInMilliseconds = -1;
        private Action<object> mainProcessingLoop;
        public ManagedThread(string name,Action<BaseMessage> messageHandler)
        {
            msgQueue = new MessageQueue(name);
            this.name = name;
            this.mainProcessingLoop = mainLoop;
            this.messageHandler = messageHandler;
        }

        public ManagedThread(string name,Action<BaseMessage> messageHandler,int waitTimeInMillisecond)
        {
            msgQueue = new MessageQueue(name);
            this.name = name;
            this.mainProcessingLoop = mainLoop;
            this.messageHandler = messageHandler;
            this.waitTimeInMilliseconds = waitTimeInMillisecond;
        }

        public void mainLoop(object state)
        {
            Thread.CurrentThread.Name = name;
            bool run = true;
            while(run == true)
            {
                BaseMessage msg = null;
                if(waitTimeInMilliseconds == -1)
                {
                    msg = msgQueue.GetMessage();
                }
                else
                {
                    msg = msgQueue.GetMessage(waitTimeInMilliseconds);
                }

                if(msg != null)
                {
                    switch (msg.MsgType)
                    {
                        case MessageType.StopThread:
                            run = false;
                            break;
                        default:
                            messageHandler(msg);
                            break;
                    }
                }
                else
                {
                    messageHandler(msg);
                }
            }
        }


        public string Name { get { return name; } }

        public void SendMessage(BaseMessage msg)
        {
            msgQueue.SendMessage(msg);
        }

        public void StartThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(mainProcessingLoop));
        }

        public void StopThread()
        {
            MessageStopThread stop = new MessageStopThread();
            msgQueue.SendMessage(stop);
        }
    }
}
