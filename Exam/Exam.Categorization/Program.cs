namespace Exam.Categorization
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            var categorizer = new Categorizator();

            categorizer.AddCategory(new Category("1", "1", null));
            categorizer.AddCategory(new Category("2", "2", null));
            categorizer.AddCategory(new Category("3", "3", null));
            categorizer.AddCategory(new Category("4", "4", null));

            categorizer.AssignParent("3", "1");
            categorizer.AssignParent("1", "4");
            categorizer.AssignParent("4", "2");

            Console.WriteLine(string.Join(", ", categorizer.GetTop3CategoriesOrderedByDepthOfChildrenThenByName()));
        }
    }
}
