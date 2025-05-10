namespace TaskManager
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Manager : IManager
    {
        private IDictionary<string, Task> tasks;
        private IDictionary<string, HashSet<Task>> taskDependent;

        public Manager()
        {
            this.tasks = new Dictionary<string, Task>();
            this.taskDependent = new Dictionary<string, HashSet<Task>>();
        }

        public void AddDependency(string taskId, string dependentTaskId)
        {
            if (!this.tasks.ContainsKey(taskId) || !this.tasks.ContainsKey(dependentTaskId))
            {
                throw new ArgumentException();
            }

            var task = this.tasks[taskId];
            var dependentTask = this.tasks[dependentTaskId];

            this.CheckDependencies(task, dependentTask);
            task.DependencyTasks.Add(dependentTask);

            if (!this.taskDependent.ContainsKey(dependentTaskId))
            {
                this.taskDependent.Add(dependentTaskId, new HashSet<Task>());
            }

            this.taskDependent[dependentTaskId].Add(task);
            this.GetAllDependent(task, taskDependent[dependentTaskId]);
        }

        private void GetAllDependent(Task task, HashSet<Task> tasks)
        {
            if (this.taskDependent.ContainsKey(task.Id))
            {
                foreach (var currentTask in this.taskDependent[task.Id])
                {
                    tasks.Add(currentTask);
                }
            }
        }

        public void AddTask(Task task)
        {
            if (this.tasks.ContainsKey(task.Id))
            {
                throw new ArgumentException();
            }

            this.tasks.Add(task.Id, task);
        }

        public bool Contains(string taskId)
        {
            return this.tasks.ContainsKey(taskId);
        }

        public Task Get(string taskId)
        {
            if (!this.tasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            return this.tasks[taskId];
        }

        public IEnumerable<Task> GetDependencies(string taskId)
        {
            var tasks = new List<Task>();

            if (!this.tasks.ContainsKey(taskId) || this.tasks[taskId].DependencyTasks.Count == 0)
            {
                return tasks;
            }

            var task = this.tasks[taskId];

            return this.GetDependenciesTasks(task, tasks);
        }

        public IEnumerable<Task> GetDependents(string taskId)
        {
            if (!this.taskDependent.ContainsKey(taskId) || this.taskDependent[taskId].Count == 0)
            {
                return new List<Task>();
            }

            return this.taskDependent[taskId];
        }

        public void RemoveDependency(string taskId, string dependentTaskId)
        {
            if (!this.tasks.ContainsKey(taskId)
                || !this.tasks.ContainsKey(dependentTaskId)
                || !this.tasks[taskId].DependencyTasks.Contains(this.tasks[dependentTaskId]))
            {
                throw new ArgumentException();
            }

            this.tasks[taskId].DependencyTasks.Remove(this.tasks[dependentTaskId]);
            this.taskDependent[dependentTaskId].Remove(this.tasks[taskId]);
        }

        public void RemoveTask(string taskId)
        {
            if (!this.tasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            this.tasks.Remove(taskId);
        }

        public int Size()
        {
            return this.tasks.Count;
        }

        private void CheckDependencies(Task task, Task dependentTask)
        {
            foreach (var currentTask in dependentTask.DependencyTasks)
            {
                if (dependentTask == task)
                {
                    throw new ArgumentException();
                }
            }
        }

        private IEnumerable<Task> GetDependenciesTasks(Task task, ICollection<Task> tasks)
        {
            if (task == null)
            {
                return tasks;
            }

            foreach (var currentTask in task.DependencyTasks)
            {
                tasks.Add(currentTask);
                this.GetDependenciesTasks(currentTask, tasks);
            }

            return tasks;
        }
    }
}
