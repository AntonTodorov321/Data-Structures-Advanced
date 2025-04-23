namespace Exam.Categorization
{
    using System.Collections.Generic;

    public class Category
    {
        public Category(string id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Children = new HashSet<Category>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Depth { get; set; }

        public Category Parent { get; set; }

        public HashSet<Category> Children { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
