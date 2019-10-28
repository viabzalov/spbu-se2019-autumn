using System;
using System.Threading;

namespace Producer_consumer_problem
{
    public class Producer<T> where T : new()
    {
        private readonly int _id;

        private bool _isRunning = true;

        public Producer()
        {
            var myThread = new Thread(PutData);
            _id = myThread.ManagedThreadId;
            myThread.Start();
        }

        public void Stop() => _isRunning = false;

        private void PutData()
        {
            Info.Print("Producer", _id, Temp<T>.Size(), "started");
            
            while (_isRunning)
            {
                Temp<T>.mPut.WaitOne();
                
                Temp<T>.Buffer.Enqueue(new T());
                
                Info.Print("Producer", _id, Temp<T>.Size(), "adding");
                Thread.Sleep((int) TimeSpan.FromSeconds(Info.WaitTimeInSeconds).TotalMilliseconds);
                
                Temp<T>.ready.Release();
                Temp<T>.mPut.ReleaseMutex();
            }

            Info.Print("Producer", _id, Temp<T>.Size(), "ended");
        }
    }
}