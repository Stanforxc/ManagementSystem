using System;
using System.Collections.Generic;
using ThreadMessaging.Messages;

namespace ThreadMessaging
{
    public interface IThread
    {
        string Name { get; }
        void StartThread();
        void StopThread();
        void SendMessage(BaseMessage msg);
    }
}
