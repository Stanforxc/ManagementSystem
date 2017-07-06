using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ThreadMessaging.Messages;

namespace ThreadMessaging
{
    public class ThreadManager : IThreadManager
    {
        private static ThreadManager threadManager = null;
        public static ThreadManager GetThreadManager()
        {
            threadManager = new ThreadManager();
            return threadManager;
        }

        private Dictionary<string, IThread> threads;
        private ManagedThread clientReceiveThread;

        private ThreadManager()
        {
            threads = new Dictionary<string, IThread>();
            clientReceiveThread = new ManagedThread("ClientReceiveThread", messageHandler);
        }

        private void messageHandler(BaseMessage msg)
        {
            if(ReceivedMessage != null)
            {
                ReceivedMessage(msg);
            }
        }

        #region IThreadManager Implementation
        public event Action<BaseMessage> ReceivedMessage;

        public void ClearAllThreads()
        {
            threads = new Dictionary<string, IThread>();
        }

        public IThread CreateThread(string threadName, Action<BaseMessage> messageHandler)
        {
            ManagedThread thrd = new ManagedThread(threadName, messageHandler);
            threads.Add(threadName, thrd);
            return thrd;
        }

        public IThread CreateThread(string threadName, Action<BaseMessage> messageHandler, int waitTimeInMilliseconds)
        {
            ManagedThread thrd = new ManagedThread(threadName, messageHandler, waitTimeInMilliseconds);
            threads.Add(threadName, thrd);
            return thrd;
        }

        public List<IThread> GetThreads()
        {
            return threads.Values.ToList();
        }

        public void SendMessage(BaseMessage msg)
        {
            clientReceiveThread.SendMessage(msg);
        }

        public void StartAllThreads()
        {
            clientReceiveThread.StartThread();
            foreach (ManagedThread t in threads.Values)
                t.StartThread();
        }

        public void StopAllThreads()
        {
            foreach (ManagedThread t in threads.Values)
                t.StopThread();
            clientReceiveThread.StopThread();
        }
        #endregion
    }
}
