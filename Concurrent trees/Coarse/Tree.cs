#region

using System.Threading;

#endregion

namespace Concurrent_trees.Coarse
{
    public class Tree
    {
        private readonly Mutex _mutex = new Mutex();

        private readonly Node fakeRoot = new Node();

        private Node root;

        public bool Find(int key)
        {
            _mutex.WaitOne();

            if (root == null)
            {
                _mutex.ReleaseMutex();
                return false;
            }

            var node = root;

            while (true)
            {
                if (node.Key == key)
                {
                    _mutex.ReleaseMutex();
                    return true;
                }

                var next = node.Key > key ? node.Left : node.Right;

                if (next == null)
                {
                    _mutex.ReleaseMutex();
                    return false;
                }

                node = next;
            }
        }

        public void Insert(int key)
        {
            if (Find(key)) return;

            _mutex.WaitOne();

            var newNode = new Node(key);

            if (root == null)
            {
                root = newNode;
                root.Parent = fakeRoot;
                fakeRoot.Left = root;
                fakeRoot.Right = root;
                _mutex.ReleaseMutex();
                return;
            }

            var node = root;

            while (true)
                if (node.Key > key)
                {
                    if (node.Left == null)
                    {
                        node.Left = newNode;
                        node.Left.Parent = node;
                        _mutex.ReleaseMutex();
                        return;
                    }

                    node = node.Left;
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = newNode;
                        node.Right.Parent = node;
                        _mutex.ReleaseMutex();
                        return;
                    }

                    node = node.Right;
                }
        }
    }
}