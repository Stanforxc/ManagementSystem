using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Domain.Entities;
using Services.Interfaces;
using Infrastructure.Data.UOW;
using Infrastructure.Data.Data;

using ThreadMessaging;
using ThreadMessaging.Messages;

namespace Services
{
    public class HistoryServices : IHistoryServices
    {
        private readonly UOW _uow;
        public HistoryServices(UOW uow)
        {
            _uow = uow;
        }

        public IEnumerable<HistoryEntity> allHistory()
        {
            var histories = _uow.HistoryRepository.GetAll().ToList();
            Mapper.Initialize(x => x.CreateMap<history,HistoryEntity>());
            return Mapper.Map<ICollection<history>, ICollection<HistoryEntity>>(histories);
        }

        public string createHistory(HistoryEntity historyEntity)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.Initialize(x => x.CreateMap<HistoryEntity, history>());
                var history = Mapper.Map<HistoryEntity, history>(historyEntity);
                
                if(new DeliverSystem(history).deliver() == "haha")
                {
                    _uow.HistoryRepository.Insert(history);
                }
                _uow.Commit();
                scope.Complete();
                return "success";
            }

        }

        //#region Example 2 Variables
        ///// <summary>
        ///// Queues for storing messages from the threads in the same order as they are received
        ///// </summary>
        //private Queue<int> thread1Queue;
        //private Queue<int> thread2Queue;
        //private Queue<int> thread3Queue;

        ///// <summary>
        ///// The number of messages expected from each thread
        ///// </summary>
        //private int numberOfExpectedMessages;
        //#endregion

       
    }
    internal class DeliverSystem
    {
        private history _history;
        private ThreadManager _threadMgr;
        private Dictionary<string, IThread> _threads;

        public DeliverSystem(history his)
        {
            _history = his;
            _threads = new Dictionary<string, IThread>();
            _threadMgr = ThreadManager.GetThreadManager();
        }
        public string deliver()
        {
            try
            {
                History1 work3 = new History1("Thread3", _threadMgr.SendMessage);
                IThread thread3 = _threadMgr.CreateThread(work3.ThreadName, work3.MessageHandler);
                _threads.Add(work3.ThreadName, thread3);

                History1 worker2 = new History1("Thread2", thread3.SendMessage);
                IThread thread2 = _threadMgr.CreateThread(worker2.ThreadName, worker2.MessageHandler);
                _threads.Add(worker2.ThreadName, thread2);

                History1 worker1 = new History1("Thread1", thread2.SendMessage);
                IThread thread1 = _threadMgr.CreateThread(worker1.ThreadName, worker1.MessageHandler);
                _threads.Add(worker1.ThreadName, thread1);

                _threadMgr.ReceivedMessage += new Action<BaseMessage>(threadMgr_NewMessage);

                _threadMgr.StartAllThreads();

                Message1 msg = new Message1(1000);
                thread1.SendMessage(msg);

                return "haha";
            }
            catch
            {
                return "-1";
            }
        }

        public void threadMgr_NewMessage(BaseMessage msg)
        {
            if (msg != null)
            {
                switch (msg.MsgType)
                {
                    // Thread 3 has finished processing and sent the message on to us
                    case MessageType.Message1:
                        // Clean up
                        _threadMgr.ReceivedMessage -= threadMgr_NewMessage;
                        _threadMgr.StopAllThreads();
                        _threadMgr.ClearAllThreads();
                        _threads.Clear();
                        //loopTestUpdateForm();
                        break;
                }
            }
        }
    }

    public class History1
    {
        /// <summary>
        /// Name of this thread
        /// </summary>
        public readonly string ThreadName;

        /// <summary>
        /// Delegate used to send the message on to the next thread
        /// </summary>
        private Action<BaseMessage> sendMsg;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="threadName"></param>
        /// <param name="sendMsg"></param>
        public History1(string threadName, Action<BaseMessage> sendMsg)
        {
            ThreadName = threadName;
            this.sendMsg = sendMsg;
        }

        /// <summary>
        /// Received message handler
        /// </summary>
        /// <param name="msg"></param>
        public void MessageHandler(BaseMessage msg)
        {
            switch (msg.MsgType)
            {
                case MessageType.Message1:
                    {
                        Message1 msg1 = (Message1)msg;

                        // Sleep for the requested time
                        System.Threading.Thread.Sleep(msg1.DelayInMilliSeconds);

                        // And then send the message on to the next thread
                        sendMsg(msg1);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Class to handle Example 2, the load/stress and dropped message test
    /// This class is used to encapsulate all the objects maintained by the thread.
    /// </summary>
    public class History2
    {
        /// <summary>
        /// Thread name
        /// </summary>
        public readonly string ThreadName;

        /// <summary>
        /// Ascending sequence number sent out in successive messages
        /// </summary>
        private int messageSequenceNumber;

        /// <summary>
        /// The number of messages to send
        /// </summary>
        private int numberOfMessagesToSend;

        /// <summary>
        /// Delegate used to send the sequence of messages back to the thread running the test
        /// </summary>
        private Action<BaseMessage> sendMsg;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="threadName"></param>
        /// <param name="numberOfMessagesToSend"></param>
        /// <param name="sendMsg"></param>
        public History2(string threadName, int numberOfMessagesToSend, Action<BaseMessage> sendMsg)
        {
            ThreadName = threadName;
            messageSequenceNumber = 0;
            this.numberOfMessagesToSend = numberOfMessagesToSend;
            this.sendMsg = sendMsg;
        }

        /// <summary>
        /// Received message handler
        /// </summary>
        /// <param name="msg"></param>
        public void MessageHandler(BaseMessage msg)
        {
            switch (msg.MsgType)
            {
                case MessageType.Message2A:
                    {
                        // Drop into a loop sending out all the requested messages back to back
                        while (messageSequenceNumber < numberOfMessagesToSend)
                        {
                            Message2B outboundMsg = new Message2B(messageSequenceNumber, ThreadName);
                            sendMsg(outboundMsg);
                            messageSequenceNumber++;
                        }
                    }
                    break;
            }
        }
    }


}
