using System.Collections.Generic;
using System.Threading;

namespace Producer_consumer_problem
{
    public class Temp<T>
    {
        public static readonly Queue<T> Buffer = new Queue<T>();

        public static Semaphore ready = new Semaphore(0, int.MaxValue);
        public static Mutex mPut = new Mutex();
        public static Mutex mGet = new Mutex();

        public static int Size() => Buffer.Count;
    }
}