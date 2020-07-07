using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
	public class Q1MaxflowVertexCapacity : Processor
    {
		public Q1MaxflowVertexCapacity(string testDataName) : base(testDataName)
		{
		}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][],long[] , long, long, long>)Solve);

		public static long[] nodesWeight;
        public virtual long Solve(long nodeCount, 
            long edgeCount, long[][] edges, long[] nodeWeight, 
            long startNode , long endNode)
        {
			nodesWeight = nodeWeight;
			return Solve(nodeCount, edgeCount, edges, startNode, endNode);
        }


		public static long[] parent;
		public virtual long Solve(long nodeCount, long edgeCount, long[][] edges,
								long startNode, long endNode)
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
			long s = startNode - 1;
			long t = endNode - 1;
			while (BFS(residual, s, t)) //find shortest path in residual graph between s and t
			{
				//maximum flow of the path
				long flow = long.MaxValue;
				while (parent[t] != t)
				{
					u = parent[t];
					if (nodesWeight[u] > 0 && nodesWeight[t] > 0)
					{
						if (residual[u, t] < flow || nodesWeight[u] < flow || nodesWeight[t] < flow)
						{
							flow = Math.Min(Math.Min(residual[u, t], nodesWeight[u]), nodesWeight[t]);
						}
					}
					t = parent[t];
				}

				t = endNode - 1;
				//update residual capacities of the edges and 
				//reverse edges along the path				
				while (parent[t] != t)
				{
					u = parent[t];
					residual[u, t] -= flow;
					residual[t, u] += flow;
					//nodesWeight[u] -= flow;
					nodesWeight[t] -= flow;
					t = parent[t];
				}
				nodesWeight[t] -= flow;
				maxFlow += flow;
				t = endNode - 1;
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
					if (!visited[v] && residual[u, v] != 0 
						&& nodesWeight[u] != 0 && nodesWeight[v] != 0)
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
