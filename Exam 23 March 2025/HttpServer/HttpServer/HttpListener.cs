namespace HttpServer
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class HttpListener : IHttpListener
    {
        private LinkedList<HttpRequest> pendingRequestList;
        private Dictionary<string, HttpRequest> pendingRequests;
        private Dictionary<string, HttpRequest> executedRequests;

        public HttpListener()
        {
            this.pendingRequestList = new LinkedList<HttpRequest>();
            this.pendingRequests = new Dictionary<string, HttpRequest>();
            this.executedRequests = new Dictionary<string, HttpRequest>();
        }

        public void AddPriorityRequest(HttpRequest request)
        {
            this.pendingRequestList.AddFirst(request);
            this.pendingRequests.Add(request.Id, request);
        }

        public void AddRequest(HttpRequest request)
        {
            this.pendingRequests.Add(request.Id, request);
            this.pendingRequestList.AddLast(request);
        }

        public void CancelRequest(string requestId)
        {
            if (!this.pendingRequests.ContainsKey(requestId))
            {
                throw new ArgumentException();
            }

            this.pendingRequests.Remove(requestId);
        }

        public bool Contains(string requestId)
        {
            return this.pendingRequests.ContainsKey(requestId);
        }

        public HttpRequest Execute()
        {
            if (this.pendingRequests.Count == 0)
            {
                throw new ArgumentException();
            }

            var request = this.pendingRequestList.First();
            this.executedRequests.Add(request.Id, request);
            this.pendingRequests.Remove(request.Id);
            this.pendingRequestList.Remove(request);

            return request;
        }

        public IEnumerable<HttpRequest> GetAllExecutedRequests()
        {
            return this.executedRequests.Values;
        }

        public IEnumerable<HttpRequest> GetByHost(string host)
        {
            return this.pendingRequests.Values.Where(r => r.Host == host);
        }

        public HttpRequest GetRequest(string requestId)
        {
            if (!this.pendingRequests.ContainsKey(requestId))
            {
                throw new ArgumentException();

            }

            return this.pendingRequests[requestId];
        }

        public HttpRequest RescheduleRequest(string requestId)
        {
            if (!this.executedRequests.ContainsKey(requestId))
            {
                throw new ArgumentException();
            }

            var request = this.executedRequests[requestId];
            this.pendingRequests.Add(requestId, request);
            this.pendingRequestList.AddLast(request);

            return request;
        }

        public int Size()
        {
            return this.pendingRequests.Count;
        }
    }
}
