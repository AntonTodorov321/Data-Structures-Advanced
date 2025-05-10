namespace TaskManager
{
    using System.Collections.Generic;

    public class Task
    {
        public Task()
        {
            this.DependencyTasks = new HashSet<Task>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Assignee { get; set; }

        public int Priority { get; set; }

        public HashSet<Task> DependencyTasks { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}