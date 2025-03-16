namespace Demo
{
    using System;

    using _01.Two_Three;

    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<IntDemo>();
            tree.Insert(new IntDemo(30));
            tree.Insert(new IntDemo(50));
            tree.Insert(new IntDemo(80));
            tree.Insert(new IntDemo(15));
            tree.Insert(new IntDemo(17));
            tree.Insert(new IntDemo(90));
            tree.Insert(new IntDemo(70));
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
