namespace Demo
{
    using System;

    using _01.Two_Three;

    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<IntDemo>();
            tree.Insert(new IntDemo(50));
            tree.Insert(new IntDemo(100));
            tree.Insert(new IntDemo(57));
            tree.Insert(new IntDemo(75));
            tree.Insert(new IntDemo(23));
            tree.Insert(new IntDemo(150));
            tree.Insert(new IntDemo(130));
            tree.Insert(new IntDemo(19));
            tree.Insert(new IntDemo(90));
            tree.Insert(new IntDemo(2));
            tree.Insert(new IntDemo(8));
            tree.Insert(new IntDemo(68));
            tree.Insert(new IntDemo(34));
            tree.Insert(new IntDemo(49));
            tree.Insert(new IntDemo(88));
            tree.Insert(new IntDemo(99));
            tree.Insert(new IntDemo(55));
            tree.Insert(new IntDemo(17));
        }

        private class IntDemo : IComparable<IntDemo>
        {
            public int Value { get; set; }

            public IntDemo(int value)
            {
                this.Value = value;
            }

            public int CompareTo(IntDemo other)
            {
                return this.Value.CompareTo(other.Value);
            }

            public override string ToString()
            {
                return this.Value.ToString();
            }
        }
    }
}
