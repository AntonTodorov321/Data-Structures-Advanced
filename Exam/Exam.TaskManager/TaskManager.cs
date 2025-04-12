namespace Exam.TaskManager
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class TaskManager : ITaskManager
    {
        private LinkedList<Task> taskQueue;
        private HashSet<Task> executedTask;
        private Dictionary<string, Task> allTasks;

        public TaskManager()
        {
            this.taskQueue = new LinkedList<Task>();
            this.executedTask = new HashSet<Task>();
            this.allTasks = new Dictionary<string, Task>();
        }

        public void AddTask(Task task)
        {
            this.taskQueue.AddLast(task);
            allTasks.Add(task.Id, task);
        }

        public bool Contains(Task task)
        {
            return this.allTasks.ContainsKey(task.Id);
        }

        public void DeleteTask(string taskId)
        {
            if (!this.allTasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            var task = this.allTasks[taskId];

            if (this.executedTask.Contains(task))
            {
                this.executedTask.Remove(task);
            }
            else
            {
                this.taskQueue.Remove(task);
            }

            this.allTasks.Remove(taskId);
        }

        public Task ExecuteTask()
        {
            if (this.taskQueue.Count == 0)
            {
                throw new ArgumentException();
            }

            var task = this.taskQueue.First();

            this.taskQueue.RemoveFirst();
            this.executedTask.Add(task);

            return task;
        }

        public IEnumerable<Task> GetAllTasksOrderedByEETThenByName()
        {
            return this.allTasks.Values
                .OrderByDescending(task => task.EstimatedExecutionTime)
                .ThenBy(task => task.Name);
        }

        public IEnumerable<Task> GetDomainTasks(string domain)
        {
            var tasks = this.taskQueue.Where(t => t.Domain == domain).ToList();

            if (tasks.Count == 0)
            {
                throw new ArgumentException();
            }

            return tasks;
        }

        public Task GetTask(string taskId)
        {
            if (!this.allTasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            return this.allTasks[taskId];
        }

        public IEnumerable<Task> GetTasksInEETRange(int lowerBound, int upperBound)
        {
            return this.taskQueue
                .Where(t => t.EstimatedExecutionTime >= lowerBound
                    && t.EstimatedExecutionTime <= upperBound);
        }

        public void RescheduleTask(string taskId)
        {
            if (!this.allTasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            var task = this.allTasks[taskId];
            this.executedTask.Remove(task);
            this.taskQueue.AddLast(task);
        }

        public int Size()
        {
            return this.allTasks.Count();
        }
    }
}
