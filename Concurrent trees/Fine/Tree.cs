namespace Concurrent_trees.Fine
{
    public class Tree
    {
        private readonly Node fakeRoot = new Node();

        private Node root;

        public bool Find(int key)
        {
            fakeRoot.Lock();

            if (root == null)
            {
                fakeRoot.Unlock();
                return false;
            }

            root.Lock();

            var node = root;

            while (true)
            {
                if (node.Key == key)
                {
                    node.Parent.Unlock();
                    node.Unlock();
                    return true;
                }

                var next = node.Key > key ? node.Left : node.Right;

                if (next == null)
                {
                    node.Parent.Unlock();
                    node.Unlock();
                    return false;
                }

                node.Parent.Unlock();
                next.Lock();
                node = next;
            }
        }

        public void Insert(int key)
        {
            if (Find(key)) return;

            fakeRoot.Lock();

            var newNode = new Node(key);

            if (root == null)
            {
                root = newNode;
                root.Parent = fakeRoot;
                fakeRoot.Left = root;
                fakeRoot.Right = root;
                fakeRoot.Unlock();
                return;
            }

            root.Lock();

            var node = root;

            while (true)
                if (node.Key > key)
                {
                    if (node.Left == null)
                    {
                        node.Left = newNode;
                        node.Left.Parent = node;
                        node.Parent.Unlock();
                        node.Unlock();
                        return;
                    }

                    node.Parent.Unlock();
                    node.Left.Lock();
                    node = node.Left;
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = newNode;
                        node.Right.Parent = node;
                        node.Parent.Unlock();
                        node.Unlock();
                        return;
                    }

                    node.Parent.Unlock();
                    node.Right.Lock();
                    node = node.Right;
                }
        }
    }
}