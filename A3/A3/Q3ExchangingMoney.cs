using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
	public class Q3ExchangingMoney : Processor
	{
		public Q3ExchangingMoney(string testDataName) : base(testDataName)
		{}

		public override string Process(string inStr) =>
			TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);

		public string[] Solve(long nodeCount, long[][] edges, long startNode)
		{
			string[] result = new string[nodeCount];
			long[] dist = new long[nodeCount];

			bool[] visited = new bool[nodeCount];
			//saving graph in adjacency list
			List<long>[] adjList = new List<long>[nodeCount];
			for (int i = 0; i < nodeCount; i++)
				adjList[i] = new List<long>();
			for (int i = 0; i < edges.Length; i++)
				adjList[edges[i][0] - 1].Add(edges[i][1] - 1);

			startNode -= 1;

			//check whether there's a path between startNode and other nodes
			Queue<long> q = new Queue<long>();
			q.Enqueue(startNode);
			visited[startNode] = true;

			while (q.Count != 0)
			{
				long u = q.Dequeue();
				foreach (var v in adjList[u])
				{
					if (!visited[v])
					{
						q.Enqueue(v);
						visited[v] = true;
					}
				}
			}

			for (int i = 0; i < nodeCount; i++)
			{
				if (!visited[i])
					result[i] = "*";

				dist[i] = int.MaxValue;
			}

			dist[startNode] = 0;
			for (int i = 0; i < nodeCount - 1; i++)
			{
				foreach (var edge in edges)
				{
					long u = edge[0] - 1;
					long v = edge[1] - 1;
					long w = edge[2];

					if (dist[v] > dist[u] + w)
						dist[v] = dist[u] + w;					
				}
			}

			List<long> negativeCycle = new List<long>();

			//iteration Vth
			for (int j = 0; j < nodeCount; j++)
			{
				for (int i = 0; i < edges.Length; i++)
				{
					long u = edges[i][0] - 1;
					long v = edges[i][1] - 1;
					long w = edges[i][2];

					if (result[v] == null)
					{
						if (dist[v] > dist[u] + w)
						{
							dist[v] = dist[u] + w;
							negativeCycle.Add(v);
						}
					}
				}
			}

			foreach (var node in negativeCycle)
				result[node] = "-";

			for (int i = 0; i < nodeCount; i++)
				if (result[i] == null)
					result[i] = dist[i].ToString();

			return result;
		}
	}
}
