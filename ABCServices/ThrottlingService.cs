using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace ABCServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ThrottlingService : IThrottlingService
    {
        public void DoWork()
        {
            Console.WriteLine("Thread {0} processing request @{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            Thread.Sleep(1000);
        }
    }
}
