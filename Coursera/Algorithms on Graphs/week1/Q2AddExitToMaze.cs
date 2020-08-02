using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A12
{
    public class Q2AddExitToMaze
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
		//	Console.WriteLine(Solve(nodeCount,edges));
		//}

		public static long cc;
		public static bool[] visited;
		public static Dictionary<long, List<long>> graph;
		public static long Solve(long nodeCount, long[][] edges)
        {
			stack = new Stack<long>();
			cc = 0;
			graph = MakeGraph(nodeCount, edges);
			visited = new bool[nodeCount];

			for(int v = 1; v <= nodeCount; v++)
			{
				if (!visited[v - 1])
				{
					cc++;
					Explore(v);
				}
			}
			return cc;
        }


		public static Stack<long> stack;
		//non-recursive explore
		public static void Explore(long v)
		{
			stack.Push(v);
			while(stack.Count != 0)
			{
				long current = stack.Pop();
				if (visited[current - 1])
					continue;

				visited[current - 1] = true;
				foreach(var e in graph[current])
					stack.Push(e);				
			}			
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
