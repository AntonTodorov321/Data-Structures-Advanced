namespace AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public Node(T element)
            {
                this.Value = element;
                this.Level = 1;
            }

            public T Value { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }

            public int Level { get; set; }
        }

        private Node root;

        public int Count()
        {
            return this.Count(this.root);
        }

        public void Insert(T element)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) == 0)
                {
                    return true;
                }
                else if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return false;
        }

        public void InOrder(Action<T> action)
        {
            this.InOrder(this.root, action);
        }

        public void PreOrder(Action<T> action)
        {
            this.PreOrder(this.root, action);
        }

        public void PostOrder(Action<T> action)
        {
            this.PostOrder(this.root, action);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + this.Count(node.Right) + this.Count(node.Right);
        }

        private void InOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.InOrder(node.Left, action);

            action(node.Value);

            this.InOrder(node.Right, action);
        }

        private void PreOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            action(node.Value);
            this.PreOrder(node.Left, action);
            this.PreOrder(node.Right, action);
        }

        private void PostOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.PostOrder(node.Left, action);
            this.PostOrder(node.Right, action);
            action(node.Value);
        }
    }
}