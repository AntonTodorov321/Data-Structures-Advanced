﻿namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Height = 1;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

            public override string ToString()
            {
                return $"Value:[{Value}] Height:{this.Height}";
            }
        }

        public Node Root { get; private set; }

        public bool Contains(T element)
        {
            return this.Contains(this.Root, element) != null;
        }

        public void Delete(T element)
        {
            this.Root = this.Delete(this.Root, element);
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                return;
            }

            Node smallestNode = this.FindSmallestChild(this.Root);

            this.Root = this.Delete(this.Root, smallestNode.Value);
        }

        public void Insert(T element)
        {
            this.Root = this.Insert(this.Root, element);
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(node.Left, element);
            }
            else
            {
                node.Right = this.Insert(node.Right, element);
            }

            node = this.Balance(node);
            node.Height = Math.Max(this.Height(node.Left), this.Height(node.Right)) + 1;

            return node;
        }

        private Node Balance(Node node)
        {
            int balanceFactor = this.Height(node.Left) - this.Height(node.Right);

            if (balanceFactor > 1)
            {
                int childBalanceFactor = this.Height(node.Left.Left) - this.Height(node.Left.Right);
                if (childBalanceFactor < 0)
                {
                    node.Left = this.RotateLeft(node.Left);
                }

                node = this.RotateRight(node);
            }
            else if (balanceFactor < -1)
            {
                int childBalanceFactor =
                    this.Height(node.Right.Left) - this.Height(node.Right.Right);
                if (childBalanceFactor > 0)
                {
                    node.Right = this.RotateRight(node.Right);
                }

                node = this.RotateLeft(node);
            }

            return node;
        }

        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            node.Height = Math.Max(this.Height(node.Left), this.Height(node.Right)) + 1;
            return temp;
        }

        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;

            node.Height = Math.Max(this.Height(node.Left), this.Height(node.Right)) + 1;
            return temp;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
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

        private Node Contains(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }

            int compare = node.Value.CompareTo(element);

            if (compare > 0)
            {
                return this.Contains(node.Left, element);
            }
            else if (compare < 0)
            {
                return this.Contains(node.Right, element);
            }

            return node;
        }

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private Node Delete(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Value.CompareTo(element) > 0)
            {
                node.Left = this.Delete(node.Left, element);
            }
            else if (node.Value.CompareTo(element) < 0)
            {
                node.Right = this.Delete(node.Right, element);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                else if (node.Left == null)
                {
                    node = node.Right;
                }
                else if (node.Right == null)
                {
                    node = node.Left;
                }
                else
                {
                    Node temp = this.FindSmallestChild(node.Right);
                    node.Value = temp.Value;

                    node.Right = this.Delete(node.Right, temp.Value);
                }
            }

            node = this.Balance(node);
            node.Height = Math.Max(this.Height(node.Left), this.Height(node.Right)) + 1;

            return node;
        }

        private Node FindSmallestChild(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return this.FindSmallestChild(node.Left);
        }
    }
}
