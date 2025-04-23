namespace Exam.Categorization
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Categorizator : ICategorizator
    {
        private Dictionary<string, Category> categories;

        public Categorizator()
        {
            categories = new Dictionary<string, Category>();
        }

        public void AddCategory(Category category)
        {
            if (this.categories.ContainsKey(category.Id))
            {
                throw new ArgumentException();
            }

            category.Depth = 0;
            this.categories.Add(category.Id, category);
        }

        public void AssignParent(string childCategoryId, string parentCategoryId)
        {
            if (!this.categories.ContainsKey(childCategoryId)
                || !this.categories.ContainsKey(parentCategoryId))
            {
                throw new ArgumentException();
            }

            var childCategory = this.categories[childCategoryId];
            var parentCategory = this.categories[parentCategoryId];

            if (parentCategory.Children.Contains(childCategory))
            {
                throw new ArgumentException();
            }

            parentCategory.Children.Add(childCategory);
            childCategory.Parent = parentCategory;

            var ancestor = parentCategory;

            while (ancestor.Parent != null)
            {
                ancestor = ancestor.Parent;
            }

            this.UpdateParentDepth(ancestor);
        }

        public bool Contains(Category category)
        {
            return this.categories.ContainsKey(category.Id);
        }

        public IEnumerable<Category> GetChildren(string categoryId)
        {
            if (!this.categories.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            var children = new HashSet<Category>();

            this.GetAllChildren(categoryId, children);
            return children;
        }

        public IEnumerable<Category> GetHierarchy(string categoryId)
        {
            if (!this.categories.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            var stack = new Stack<Category>();
            this.GetAllParentRecursively(this.categories[categoryId], stack);

            return stack;
        }

        public IEnumerable<Category> GetTop3CategoriesOrderedByDepthOfChildrenThenByName()
        {
            return this.categories.Values
                .OrderByDescending(c => c.Depth)
                .ThenBy(c => c.Name)
                .Take(3);
        }

        public void RemoveCategory(string categoryId)
        {
            if (!this.categories.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            var category = this.categories[categoryId];
            this.categories.Remove(categoryId);

            this.RemoveChildrenRecursively(category);

            if (category.Parent != null)
            {
                category.Parent.Children.Remove(category);
            }
        }

        public int Size()
        {
            return this.categories.Count();
        }

        private void RemoveChildrenRecursively(Category category)
        {
            foreach (var child in category.Children)
            {
                this.RemoveChildrenRecursively(child);
                categories.Remove(child.Id);
            }
        }

        private void GetAllChildren(string categoryId, HashSet<Category> children)
        {
            foreach (var child in this.categories[categoryId].Children)
            {
                children.Add(child);
                this.GetAllChildren(child.Id, children);
            }
        }

        private void GetAllParentRecursively(Category category, Stack<Category> stack)
        {
            if (category == null)
            {
                return;
            }

            stack.Push(category);
            this.GetAllParentRecursively(category.Parent, stack);
        }

        private int UpdateParentDepth(Category node)
        {
            if (node == null)
            {
                return 0;
            }

            int depth = 0;

            foreach (var child in node.Children)
            {
                depth = Math.Max(depth, this.UpdateParentDepth(child));
            }

            node.Depth = depth + 1;
            return node.Depth;
        }
    }
}
