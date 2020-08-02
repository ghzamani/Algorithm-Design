using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace A3
{
	public class Q3ExchangingMoney
	{
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long nodeCount = long.Parse(input[0]);
		//	long edgeCount = long.Parse(input[1]);

		//	List<long>[] adjList = new List<long>[nodeCount];
		//	for (int i = 0; i < nodeCount; i++)
		//		adjList[i] = new List<long>();

		//	long[][] edges = new long[edgeCount][];

		//	long j = 0;
		//	while (j < edgeCount)
		//	{
		//		input = Console.ReadLine().Split(' ');
		//		long u = long.Parse(input[0]) - 1;
		//		long v = long.Parse(input[1]) - 1;
		//		long w = long.Parse(input[2]);
		//		adjList[u].Add(v);

		//		edges[j] = new long[3] { u, v, w };
		//		j++;
		//	}
		//	long start = long.Parse(Console.ReadLine()) - 1;
		//	string[] result = Solve(nodeCount, adjList, edges, start);
		//	foreach(var r in result)
		//		Console.WriteLine(r);
		//}

		public static string[] Solve(long nodeCount, List<long>[] adjList, long[][] edges, long startNode)
		{
			string[] result = new string[nodeCount];
			long[] dist = new long[nodeCount];

			bool[] visited = new bool[nodeCount];

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
					long u = edge[0];
					long v = edge[1];
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
					long u = edges[i][0];
					long v = edges[i][1];
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
