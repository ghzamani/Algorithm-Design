using System;
using System.Collections.Generic;

namespace A2
{
	public class Q1ShortestPath
	{
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long nodeCount = long.Parse(input[0]);
		//	long edgeCount = long.Parse(input[1]);

		//	List<long>[] adjList = new List<long>[nodeCount];
		//	for (int i = 0; i < nodeCount; i++)
		//		adjList[i] = new List<long>();

		//	long j = 0;
		//	while (j < edgeCount)
		//	{
		//		input = Console.ReadLine().Split(' ');
		//		long u = long.Parse(input[0]) - 1;
		//		long v = long.Parse(input[1]) - 1;
		//		adjList[u].Add(v);
		//		adjList[v].Add(u);
		//		j++;
		//	}

		//	input = Console.ReadLine().Split(' ');
		//	long start = long.Parse(input[0]) - 1;
		//	long end = long.Parse(input[1]) - 1;
		//	Console.WriteLine(Solve(nodeCount,adjList,start,end));
		//}
		public static long Solve(long NodeCount, List<long>[] adjList,
						  long StartNode, long EndNode)
		{
			long[] dist = new long[NodeCount];
			for (long i = 0; i < NodeCount; i++)
				dist[i] = long.MaxValue;
			dist[StartNode] = 0;

			Queue<long> q = new Queue<long>();
			q.Enqueue(StartNode);
			while (q.Count != 0)
			{
				long u = q.Dequeue();
				foreach(var v in adjList[u])
				{
					if (dist[v] == long.MaxValue)
					{
						q.Enqueue(v);
						dist[v] = dist[u] + 1;
					}
					if (dist[EndNode] != long.MaxValue)
						return dist[EndNode];
				}
			}
			return -1;
		}
	}
}
