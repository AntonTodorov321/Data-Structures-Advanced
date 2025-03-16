namespace _01.RedBlackTree
{
    using System;

    public class RedBlackTree<T> where T : IComparable
    {
        private const bool Red = true;
        private const bool Black = false;

        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = Red;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
        }

        public Node root;

        private RedBlackTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public RedBlackTree()
        {

        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public RedBlackTree<T> Search(T element)
        {
            Node current = this.FindNode(element);

            return new RedBlackTree<T>(current);
        }

        public void Insert(T element)
        {
            this.root = this.Insert(this.root, element);
            this.root.Color = Black;
        }

        public void Delete(T key)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }


        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMax(this.root);

            if (this.root != null)
            {
                this.root.Color = Black;
            }
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMin(this.root);

            if (this.root != null)
            {
                this.root.Color = Black;
            }
        }

        private Node FindNode(T element)
        {
            Node node = this.root;

            while (node != null)
            {
                if (IsLesser(element, node.Value))
                {
                    node = node.Left;
                }
                else if (IsLesser(node.Value, element))
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }

            return node;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        public int Count()
        {
            return this.Count(this.root);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + this.Count(node.Left) + this.Count(node.Right);
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Color == Red;
        }

        private void ColorFlip(Node node)
        {
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color = !node.Right.Color;
        }

        private bool IsLesser(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        private bool AreEqual(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            if (IsLesser(element, node.Value))
            {
                node.Left = this.Insert(node.Left, element);
            }
            else
            {
                node.Right = this.Insert(node.Right, element);
            }

            if (IsRed(node.Right))
            {
                node = this.RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                this.ColorFlip(node);
            }

            return node;
        }

        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = temp.Left.Color;
            temp.Left.Color = Red;

            return temp;
        }

        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = temp.Right.Color;
            temp.Right.Color = Red;

            return temp;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return null;
            }

            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                node = this.MoveRedLeft(node);
            }

            node.Left = this.DeleteMin(node.Left);

            return this.FixUp(node);
        }

        private Node MoveRedLeft(Node node)
        {
            this.ColorFlip(node);

            if (this.IsRed(node.Right.Left))
            {
                node.Right = this.RotateRight(node.Right);
                node = this.RotateLeft(node);
                this.ColorFlip(node);
            }

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
            {
                node = this.RotateRight(node);
            }

            if (node.Right == null)
            {
                return null;
            }

            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
            {
                node = this.MoveRedRight(node);
            }

            node.Right = this.DeleteMax(node.Right);

            return this.FixUp(node);
        }

        private Node FixUp(Node node)
        {
            if (this.IsRed(node.Right))
            {
                node = this.RotateLeft(node);
            }

            if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }

            if (this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                this.ColorFlip(node);
            }

            return node;
        }

        private Node MoveRedRight(Node node)
        {
            this.ColorFlip(node);

            if (this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
                this.ColorFlip(node);
            }

            return node;
        }
    }
}