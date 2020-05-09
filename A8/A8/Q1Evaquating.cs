using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
	public class Q1Evaquating : Processor
	{
		public Q1Evaquating(string testDataName) : base(testDataName) { }

		public override string Process(string inStr) =>
			TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

		public static long[] parent;
		public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
		{
			//adjacency matrix of graph
			long[,] graph = new long[nodeCount, nodeCount];
			for (int i = 0; i < edges.Length; i++)
			{
				long x = edges[i][0] - 1;
				long y = edges[i][1] - 1;
				long capacity = edges[i][2];

				graph[x, y] += capacity;
			}

			//keep the edges in the same direction of graph edges in residual graph
			//residual = capacity - flow
			long[,] residual = new long[nodeCount, nodeCount];

			//first fill Gr with capacities
			for (int i = 0; i < nodeCount; i++)
			{
				for (int j = 0; j < nodeCount; j++)
				{
					residual[i, j] = graph[i, j];
				}
			}

			parent = new long[nodeCount];
			long maxFlow = 0;

			long u;
			long s = 0;
			long t = nodeCount - 1;
			while (BFS(residual, s, t)) //find shortest path in residual graph between s and t
			{
				//maximum flow of the path
				long flow = long.MaxValue;
				while (parent[t] != t)
				{
					u = parent[t];
					if (residual[u, t] < flow)
						flow = residual[u, t];
					t = parent[t];
				}

				t = nodeCount - 1;
				//update residual capacities of the edges and 
				//reverse edges along the path				
				while (parent[t] != t)
				{
					u = parent[t];
					residual[u, t] -= flow;
					residual[t, u] += flow;
					t = parent[t];
				}

				maxFlow += flow;
				t = nodeCount - 1;
			}
			return maxFlow;
		}

		public static bool BFS(long[,] residual, long source, long dest)
		{
			bool[] visited = new bool[parent.Length];
			
			visited[source] = true;
			parent[source] = source;

			Queue<long> queue = new Queue<long>();
			queue.Enqueue(source);
			while (queue.Count != 0)
			{
				long u = queue.Dequeue();
				for (int v = 0; v < parent.Length; v++)
				{
					if (!visited[v] && residual[u, v] != 0)
					{
						queue.Enqueue(v);
						parent[v] = u;
						visited[v] = true;
					}
				}
				if (visited[dest])
					break;
			}

			return visited[dest];
		}

	}
	 //remember to delete this
	public class MaxFlow
	{
		public int V;
		public MaxFlow(int v)
		{
			V = v;
		}
		public long fordFulkerson(long[,] graph, long s, long t)
		{
			long u, v;

			// Create a residual graph and fill  
			// the residual graph with given  
			// capacities in the original graph as 
			// residual capacities in residual graph 

			// Residual graph where rGraph[i,j]  
			// indicates residual capacity of  
			// edge from i to j (if there is an  
			// edge. If rGraph[i,j] is 0, then  
			// there is not) 
			long[,] rGraph = new long[V, V];

			for (u = 0; u < V; u++)
				for (v = 0; v < V; v++)
					rGraph[u, v] = graph[u, v];

			// This array is filled by BFS and to store path 
			long[] parent = new long[V];

			long max_flow = 0; // There is no flow initially 

			// Augment the flow while tere is path from source 
			// to sink 
			while (bfs(rGraph, s, t, parent))
			{
				// Find minimum residual capacity of the edhes 
				// along the path filled by BFS. Or we can say 
				// find the maximum flow through the path found. 
				long path_flow = long.MaxValue;
				for (v = t; v != s; v = parent[v])
				{
					u = parent[v];
					//path_flow = Math.Min(path_flow, rGraph[u, v]);
					if (rGraph[u, v] < path_flow)
						path_flow = rGraph[u, v];
				}

				// update residual capacities of the edges and 
				// reverse edges along the path 
				for (v = t; v != s; v = parent[v])
				{
					u = parent[v];
					rGraph[u, v] -= path_flow;
					rGraph[v, u] += path_flow;
				}

				// Add path flow to overall flow 
				max_flow += path_flow;
			}

			// Return the overall flow 
			return max_flow;
		}

		bool bfs(long[,] rGraph, long s, long t, long[] parent)
		{
			// Create a visited array and mark  
			// all vertices as not visited 
			bool[] visited = new bool[V];
			for (int i = 0; i < V; ++i)
				visited[i] = false;

			// Create a queue, enqueue source vertex and mark 
			// source vertex as visited 
			List<long> queue = new List<long>();
			queue.Add(s);
			visited[s] = true;
			parent[s] = -1;

			// Standard BFS Loop 
			while (queue.Count != 0)
			{
				long u = queue[0];
				queue.RemoveAt(0);

				for (int v = 0; v < V; v++)
				{
					if (visited[v] == false && rGraph[u, v] > 0)
					{
						queue.Add(v);
						parent[v] = u;
						visited[v] = true;
					}
				}
			}

			// If we reached sink in BFS  
			// starting from source, then 
			// return true, else false 
			return (visited[t] == true);
		}
	}
}

