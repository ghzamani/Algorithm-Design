using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
	public class Q1ShortestPath : Processor
	{
		public Q1ShortestPath(string testDataName) : base(testDataName) { }

		public override string Process(string inStr) =>
			TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

		public long Solve(long NodeCount, long[][] edges,
						  long StartNode, long EndNode)
		{
			//saving graph in adjacency list
			List<long>[] adjList = new List<long>[NodeCount];
			for (int i = 0; i < NodeCount; i++)
				adjList[i] = new List<long>();
			for (int i = 0; i < edges.Length; i++)
			{
				adjList[edges[i][0] - 1].Add(edges[i][1]);
				adjList[edges[i][1] - 1].Add(edges[i][0]);
			}

			long[] dist = new long[NodeCount];
			for (long i = 0; i < NodeCount; i++)
				dist[i] = long.MaxValue;
			dist[StartNode - 1] = 0;

			Queue<long> q = new Queue<long>();
			q.Enqueue(StartNode);
			while (q.Count != 0)
			{
				long u = q.Dequeue();
				foreach(var v in adjList[u - 1])
				{
					if (dist[v - 1] == long.MaxValue)
					{
						q.Enqueue(v);
						dist[v - 1] = dist[u - 1] + 1;
					}
					if (dist[EndNode - 1] != long.MaxValue)
						return dist[EndNode - 1];
				}
			}
			return -1;
		}
	}
}
