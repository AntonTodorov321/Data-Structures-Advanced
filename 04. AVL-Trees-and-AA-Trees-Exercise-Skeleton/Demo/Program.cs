namespace Demo
{
    using AVLTree;
    using System;

    class Program
    {
        static void Main()
        {
            var tree = new AVL<int>();

            tree.Insert(50);
            tree.Insert(10);
            tree.Insert(30);

            tree.EachInOrder(Console.WriteLine);
        }
    }
}
