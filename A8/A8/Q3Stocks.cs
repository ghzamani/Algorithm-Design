using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q3Stocks : Processor
    {
        public Q3Stocks(string testDataName) : base(testDataName)
		{ }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

		public static long[] parent;
        public virtual long Solve(long stockCount, long pointCount, long[][] matrix)
        {
			long nodeCount = 2 * stockCount + 2;
			long[,] graph = new long[nodeCount, nodeCount];

			//there's edge from s to all nodes of U
			for (int i = 1; i <= stockCount; i++)
			{
				graph[0, i] = 1;
			}

			//if all points of a chart is less than all points of another chart
			//then there's an edge between them
			for (int i = 0; i < stockCount; i++)
			{
				for (int j = 0; j < stockCount; j++)
				{
					if (j == i)
						continue;

					if (CheckTwoArray(matrix[i], matrix[j]))
					{
						graph[i + 1, j + stockCount + 1] = 1;
					}
				}
			}

			//there's edge from all nodes of V to t
			for (int i = 0; i < stockCount; i++)
			{
				graph[i + stockCount + 1, nodeCount - 1] = 1;
			}

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
					   			 
			return stockCount - maxFlow;
        }

		//checks arr1[0] < arr2[0], ... , arr1[n] < arr2[n]
		//if true returns true
		public static bool CheckTwoArray (long[] arr1, long[] arr2)
		{
			for (int i = 0; i < arr1.Length; i++)
			{
				if (arr1[i] >= arr2[i])
					return false;
			}
			return true;
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
