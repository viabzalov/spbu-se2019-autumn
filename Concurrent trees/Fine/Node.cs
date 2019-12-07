using System.Threading;

namespace Concurrent_trees.Fine
{
    public class Node
    {
        private readonly Mutex _mutex = new Mutex();
        private bool isLocked;

        public int Key;
        public Node Left;
        public Node Parent;
        public Node Right;

        public Node(int key)
        {
            Key = key;
        }

        public Node()
        {
        }

        public void Lock()
        {
            _mutex.WaitOne();
            isLocked = true;
        }

        public void Unlock()
        {
            isLocked = false;
            _mutex.ReleaseMutex();
        }
    }
}