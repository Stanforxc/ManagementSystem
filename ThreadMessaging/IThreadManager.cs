using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadMessaging.Messages;

namespace ThreadMessaging
{
    public interface IThreadManager
    {
        IThread CreateThread(string threadName, Action<BaseMessage> messageHandler);
        IThread CreateThread(string threadName,Action<BaseMessage> messageHandler,int waitTimeInMilliseconds);
        List<IThread> GetThreads();
        void StartAllThreads();
        void StopAllThreads();
        void ClearAllThreads();
        void SendMessage(BaseMessage msg);
        event Action<BaseMessage> ReceivedMessage;
    }
}
