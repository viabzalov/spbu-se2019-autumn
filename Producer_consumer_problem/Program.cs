using System;
using System.Collections.Generic;

namespace Producer_consumer_problem
{
    class Program
    {
        public const int ProducersCount = 42;
        public const int ConsumersCount = 42;
        public const int WaitTime = 1;
        
        static void Main()
        {
            var producers = new List<Producer<int>>();
            var consumers = new List<Consumer<int>>();
            
            for (int i = 0; i < ProducersCount; ++i)
            {
                producers.Add(new Producer<int>());
            }

            for (int i = 0; i < ConsumersCount; ++i)
            {
                consumers.Add(new Consumer<int>());
            }

            Console.ReadKey();

            for (int i = 0; i < ProducersCount; ++i)
            {
                producers[i].Stop();
            }

            for (int i = 0; i < ConsumersCount; ++i)
            {
                consumers[i].Stop();
            }
        }
    }
}