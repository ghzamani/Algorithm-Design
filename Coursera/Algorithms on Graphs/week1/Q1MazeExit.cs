using System;
using System.Collections.Generic;

namespace A12
{
    public class Q1MazeExit
    {
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long nodeCount = long.Parse(input[0]);
		//	long edgeCount = long.Parse(input[1]);
		//	long[][] edges = new long[edgeCount][];

		//	int i = 0;
		//	while (i < edgeCount)
		//	{
		//		var str = Console.ReadLine().Split(' ');
		//		edges[i] = new long[2] { long.Parse(str[0]), long.Parse(str[1]) };
		//		i++;
		//	}
		//	input = Console.ReadLine().Split(' ');
		//	long start = long.Parse(input[0]);
		//	long end = long.Parse(input[1]);

		//	Console.WriteLine(Solve(nodeCount,edges,start,end));
		//}

		public static bool[] visited;
		public static Dictionary<long, List<long>> graph;
        public static long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
			visited = new bool[nodeCount];
			graph = MakeGraph(nodeCount, edges);

			//if the node doesn't have any edge so there wouldn't be any path
			if (graph[StartNode].Count == 0 ||
				graph[EndNode].Count == 0)
				return 0;

			Explore(StartNode);
			if (visited[EndNode - 1])
				return 1;
			return 0;
        }

		public static void Explore(long v)
		{
			visited[v - 1] = true;
			foreach(var u in graph[v])
				if (visited[u - 1] == false)
					Explore(u);			
		}

		public static Dictionary<long, List<long>> MakeGraph(long nodeCount, long[][] edges)
		{
			Dictionary<long, List<long>> graph = 
				new Dictionary<long, List<long>>((int)nodeCount);

			for (int i = 1; i <= nodeCount; i++)
				graph.Add(i, new List<long>());

			foreach (var e in edges)
			{
				graph[e[0]].Add(e[1]);
				graph[e[1]].Add(e[0]);
			}
			return graph;
		}
	}
}
