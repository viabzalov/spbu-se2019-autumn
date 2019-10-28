using System;
using System.Collections.Generic;

namespace Producer_consumer_problem
{
    internal class Program
    {
        private static void Main()
        {
            var producers = new List<Producer<int>>();
            var consumers = new List<Consumer<int>>();

            for (var i = 0; i < Info.ProducersCount; ++i) producers.Add(new Producer<int>());

            for (var i = 0; i < Info.ConsumersCount; ++i) consumers.Add(new Consumer<int>());

            Console.ReadKey();

            for (var i = 0; i < Info.ProducersCount; ++i) producers[i].Stop();

            for (var i = 0; i < Info.ConsumersCount; ++i) consumers[i].Stop();
        }
    }
}