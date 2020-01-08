using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Print_Tower
{
    static class BWorker
    {
        private static Thread BWorkerThread;

        public static void StartWorking(ThreadStart t)
        {
            BWorkerThread = new Thread(t);
            BWorkerThread.Start();
        }
    }
}
