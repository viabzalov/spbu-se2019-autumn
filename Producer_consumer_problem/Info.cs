using System;

namespace Producer_consumer_problem
{
    public class Info
    {
        public const int ProducersCount = 4;
        public const int ConsumersCount = 4;
        public const int WaitTimeInSeconds = 1;

        public static void Print(string name, int id, int bufferSize, string action) =>
            Console.WriteLine("{0, 8} (id= {1, 2}) " +
                              "{2, 8} " +
                              "(buffer size= {3, 2})", name, id, action, bufferSize);
    }
}