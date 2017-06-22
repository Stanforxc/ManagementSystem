using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Top;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Service
{
    public class MessageService
    {
        // 存放要计算的数值的字段

        static string phone = "13301856183";

        static ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", "23476788", "d7b403e513c48d1eebefb376050b8f85");

        static object lockObj = new object();

        public static void send()
        {

            lock (lockObj) {
                // 获取线程池的最大线程数和维护的最小空闲线程数

                int maxThreadNum, portThreadNum;

                int minThreadNum;

                ThreadPool.GetMaxThreads(out maxThreadNum, out portThreadNum);

                ThreadPool.GetMinThreads(out minThreadNum, out portThreadNum);


                ThreadPool.QueueUserWorkItem(new WaitCallback(real_send), phone);
            }

        }

        private static void real_send(object para) {
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.SmsType = "normal";
            req.SmsFreeSignName = "阿里大于";
            req.RecNum = phone;
            req.SmsTemplateCode = "SMS_72615007";
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }

    }
}
