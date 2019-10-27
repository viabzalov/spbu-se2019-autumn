using System;
using System.Threading;

namespace Producer_consumer_problem
{
    public class Producer<T> where T : new()
    {
        private static Thread myThread = new Thread(PutData);

        public void Stop() => myThread.Abort();

        private static void PutData()
        {
            while (myThread.ThreadState != ThreadState.StopRequested)
            {
                Temp<T>.mPut.WaitOne();
                Temp<T>.buffer.Enqueue(new T());
                Thread.Sleep((int) TimeSpan.FromSeconds(Program.WaitTime).TotalMilliseconds);
                Temp<T>.ready.Release();
                Temp<T>.mPut.ReleaseMutex();
            }
        }
    }
}