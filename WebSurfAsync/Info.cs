using System;
using System.Collections.Generic;
using System.Threading;

namespace WebSurfAsync
{
    public class Info
    {
        private static readonly SortedDictionary<string, int> LinePositions = new SortedDictionary<string, int>();
        private static int LastPosition;
        private static readonly Mutex _mutex = new Mutex();

        public static void Print(string url, int pageSize)
        {
            _mutex.WaitOne();

            if (!LinePositions.ContainsKey(url))
            {
                LinePositions.Add(url, LastPosition + 1);
                LastPosition += 1;
            }

            Console.SetCursorPosition(0, LinePositions[url]);
            Console.WriteLine($"{url.Substring(0, Math.Min(url.Length, 100)),100} - " +
                              $"{(pageSize == 0 ? "awaiting.." : pageSize.ToString()),10}");

            _mutex.ReleaseMutex();
        }
    }
}