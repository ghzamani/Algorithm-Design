using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        public Q2Airlines(string testDataName) : base(testDataName) 
		{ }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);


		public static long[] parent;
		public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
			long[] result = new long[flightCount];
			long nodeCount = flightCount + crewCount + 2;
			long[,] Gprime = new long[nodeCount, nodeCount];

			//there's edge from s to all nodes of U
			for (int i = 1; i <= flightCount; i++)
			{
				Gprime[0, i] = 1;
			}

			//edges between flight -> crew
			for (int i = 1; i <= flightCount; i++)
			{
				for (int j = 0; j < crewCount; j++)
				{
					Gprime[i, j + 1 + flightCount] = info[i - 1][j];
				}
			}

			//there's edge from all nodes of V to t
			for (int i = 0; i < crewCount; i++)
			{
				Gprime[i + flightCount + 1, nodeCount - 1] = 1;
			}
					   			 
			long[,] residual = new long[nodeCount, nodeCount];

			//first fill Gr with capacities
			for (int i = 0; i < nodeCount; i++)
			{
				for (int j = 0; j < nodeCount; j++)
				{
					residual[i, j] = Gprime[i, j];
				}
			}

			parent = new long[nodeCount];

			long u;
			long s = 0;
			long t = nodeCount - 1;
			while (BFS(residual, s, t)) //find shortest path in residual graph between s and t
			{
				long flow = 1; 				
				while (parent[t] != t)
				{
					u = parent[t];
					residual[u, t] -= flow;
					residual[t, u] += flow;
					t = parent[t];
				}

				t = nodeCount - 1;
				parent = new long[nodeCount];
			}
					   			 
			//in residual, edges between crew -> flight which are one, are the answers
			for (long i = flightCount + 1; i < nodeCount - 1; i++)
			{
				for (int j = 1; j < flightCount + 1; j++)
				{
					if(residual[i,j] == 1)
					{
						result[j - 1] = i - flightCount;
					}
				}
			}

			for (int i = 0; i < result.Length; i++)
			{
				if (result[i] == 0)
					result[i] = -1;
			}

			return result;
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
