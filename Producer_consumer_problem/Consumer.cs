using System;
using System.Threading;

namespace Producer_consumer_problem
{
    public class Consumer<T> where T : new()
    {
        private static Thread myThread = new Thread(GetData);

        public void Stop() => myThread.Abort();
        
        /*
        public Consumer()
        {
            myThread = new Thread(GetData);
        }
        */

        private static void GetData()
        {
            while (myThread.ThreadState != ThreadState.StopRequested)
            {
                Temp<T>.ready.WaitOne();
                Temp<T>.mGet.WaitOne();
                Temp<T>.buffer.Dequeue();
                Thread.Sleep((int) TimeSpan.FromSeconds(Program.WaitTime).TotalMilliseconds);
                Temp<T>.mGet.ReleaseMutex();
            }
        }
    }
}