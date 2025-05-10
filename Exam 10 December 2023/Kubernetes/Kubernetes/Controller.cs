namespace Kubernetes
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Controller : IController
    {
        private Dictionary<string, Pod> pods;

        public Controller()
        {
            this.pods = new Dictionary<string, Pod>();
        }

        public bool Contains(string podId)
        {
            return this.pods.ContainsKey(podId);
        }

        public void Deploy(Pod pod)
        {
            this.pods.Add(pod.Id, pod);
        }

        public Pod GetPod(string podId)
        {
            if (!this.pods.ContainsKey(podId))
            {
                throw new ArgumentException();
            }

            return this.pods[podId];
        }

        public void Uninstall(string podId)
        {
            if (!this.pods.ContainsKey(podId))
            {
                throw new ArgumentException();
            }

            this.pods.Remove(podId);
        }

        public void Upgrade(Pod pod)
        {
            if (this.pods.ContainsKey(pod.Id))
            {
                this.pods[pod.Id] = pod;
            }
        }

        public int Size()
        {
            return this.pods.Count;
        }

        public IEnumerable<Pod> GetPodsBetweenPort(int lowerBound, int upperBound)
        {
            return this.pods.Values.Where(p => p.Port >= lowerBound && p.Port <= upperBound);
        }

        public IEnumerable<Pod> GetPodsInNamespace(string @namespace)
        {
            return this.pods.Values.Where(p => p.Namespace == @namespace);
        }

        public IEnumerable<Pod> GetPodsOrderedByPortThenByName()
        {
            return this.pods.Values
                .OrderByDescending(p => p.Port)
                .ThenBy(p => p.Namespace);
        }
    }
}