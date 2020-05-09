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
}

