using System;
using System.Collections.Generic;
using System.Linq;

namespace RestFlow.Common.DataStructures
{
    public class CustomGraph<T>
    {
        private readonly Dictionary<int, T> vertices;
        private readonly Dictionary<int, List<int>> adjacencyList;
        private readonly Func<T, int> getId;

        public CustomGraph(Func<T, int> getIdFunction)
        {
            vertices = new Dictionary<int, T>();
            adjacencyList = new Dictionary<int, List<int>>();
            getId = getIdFunction;
        }
        public void AddNode(T vertex)
        {
            int id = getId(vertex);
            if (!vertices.ContainsKey(id))
            {
                vertices[id] = vertex;
                adjacencyList[id] = new List<int>();
            }
        }

        public void RemoveNode(int id)
        {
            if (vertices.ContainsKey(id))
            {
                foreach (var adjacent in adjacencyList[id])
                {
                    adjacencyList[adjacent].Remove(id);
                }
                adjacencyList.Remove(id);
                vertices.Remove(id);
            }
        }

        public void AddEdge(int vertex1Id, int vertex2Id)
        {
            if (!adjacencyList.ContainsKey(vertex1Id) || !adjacencyList.ContainsKey(vertex2Id))
            {
                throw new ArgumentException("One or both vertices do not exist.");
            }

            adjacencyList[vertex1Id].Add(vertex2Id);
            adjacencyList[vertex2Id].Add(vertex1Id);
        }

        public void RemoveEdge(int vertex1Id, int vertex2Id)
        {
            if (adjacencyList.ContainsKey(vertex1Id) && adjacencyList.ContainsKey(vertex2Id))
            {
                adjacencyList[vertex1Id].Remove(vertex2Id);
                adjacencyList[vertex2Id].Remove(vertex1Id);
            }
        }

        public T GetNodeById(int id)
        {
            if (vertices.ContainsKey(id))
            {
                return vertices[id];
            }
            return default;
        }

        public IEnumerable<T> GetAllNodes()
        {
            return vertices.Values.ToList();
        }

        public IEnumerable<T> GetNeighbors(int id)
        {
            if (!adjacencyList.ContainsKey(id))
            {
                throw new ArgumentException("Vertex does not exist.");
            }

            return adjacencyList[id].Select(neighborId => vertices[neighborId]);
        }

        public void TraverseBFS(int startVertexId, Action<T> action)
        {
            if (!vertices.ContainsKey(startVertexId))
            {
                throw new ArgumentException("Start vertex does not exist.");
            }

            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(startVertexId);
            visited.Add(startVertexId);

            while (queue.Count > 0)
            {
                var vertexId = queue.Dequeue();
                action(vertices[vertexId]);

                foreach (var neighborId in adjacencyList[vertexId])
                {
                    if (!visited.Contains(neighborId))
                    {
                        visited.Add(neighborId);
                        queue.Enqueue(neighborId);
                    }
                }
            }
        }

        public void TraverseDFS(int startVertexId, Action<T> action)
        {
            if (!vertices.ContainsKey(startVertexId))
            {
                throw new ArgumentException("Start vertex does not exist.");
            }

            var visited = new HashSet<int>();
            TraverseDFSRecursive(startVertexId, action, visited);
        }

        private void TraverseDFSRecursive(int vertexId, Action<T> action, HashSet<int> visited)
        {
            if (visited.Contains(vertexId)) return;

            action(vertices[vertexId]);
            visited.Add(vertexId);

            foreach (var neighborId in adjacencyList[vertexId])
            {
                TraverseDFSRecursive(neighborId, action, visited);
            }
        }
    }
}
