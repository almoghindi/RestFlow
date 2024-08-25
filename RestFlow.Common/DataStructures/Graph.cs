using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.Common.DataStructures
{
    using System.Collections.Generic;

    public class CustomGraph<T>
    {
        private readonly Dictionary<T, List<T>> adjacencyList;

        public CustomGraph()
        {
            adjacencyList = new Dictionary<T, List<T>>();
        }

        public void AddVertex(T vertex)
        {
            if (!adjacencyList.ContainsKey(vertex))
            {
                adjacencyList[vertex] = new List<T>();
            }
        }

        public void AddEdge(T vertex1, T vertex2)
        {
            if (!adjacencyList.ContainsKey(vertex1) || !adjacencyList.ContainsKey(vertex2))
            {
                throw new ArgumentException("Vertex does not exist.");
            }

            adjacencyList[vertex1].Add(vertex2);
            adjacencyList[vertex2].Add(vertex1); // For undirected graph
        }

        public IEnumerable<T> GetNeighbors(T vertex)
        {
            if (adjacencyList.ContainsKey(vertex))
            {
                return adjacencyList[vertex];
            }
            throw new ArgumentException("Vertex does not exist.");
        }

        public void TraverseBFS(T startVertex, Action<T> action)
        {
            var visited = new HashSet<T>();
            var queue = new Queue<T>();
            queue.Enqueue(startVertex);
            visited.Add(startVertex);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                action(vertex);

                foreach (var neighbor in adjacencyList[vertex])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        public void TraverseDFS(T startVertex, Action<T> action)
        {
            var visited = new HashSet<T>();
            TraverseDFSRecursive(startVertex, action, visited);
        }

        private void TraverseDFSRecursive(T vertex, Action<T> action, HashSet<T> visited)
        {
            if (visited.Contains(vertex)) return;

            action(vertex);
            visited.Add(vertex);

            foreach (var neighbor in adjacencyList[vertex])
            {
                TraverseDFSRecursive(neighbor, action, visited);
            }
        }
    }
}
