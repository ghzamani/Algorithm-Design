using System;
using System.Collections.Generic;
using System.Linq;
namespace A2
{
	public class Q2BipartiteGraph
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
		//	Console.WriteLine(CheckBipartite(adjList));
		//}
		
		public static long CheckBipartite(List<long>[] adjList)
		{
			color[] nodesColors = new color[adjList.Length];
			long[] dist = new long[adjList.Length];
			for (int i = 0; i < dist.Length; i++)
			{
				dist[i] = long.MaxValue;
			}
			
			for (int i = 0; i < nodesColors.Length; i++)
			{
				if (nodesColors[i] == color.green)
				{
					dist[i] = 0;
					//do bfs and give color to all nodes
					//there shouldn't be an edge between same colors
					Queue<long> q = new Queue<long>();
					q.Enqueue(i);
					nodesColors[i] = color.blue; //blue for even dists
												 //green for not visited dists
					while (q.Count != 0)
					{
						long u = q.Dequeue();

						color otherColor;
						if (dist[u] % 2 == 0)
						{
							otherColor = color.red;
						}
						else otherColor = color.blue;

						foreach (var v in adjList[u])
						{
							if (dist[v] == long.MaxValue)
							{
								q.Enqueue(v);
								dist[v] = dist[u] + 1;
								nodesColors[v] = otherColor;
							}
							else //v was visited before
							{
								if (nodesColors[u] == nodesColors[v])
									return 0;
							}
						}
					}
				}
			}
			return 1;
		}
		public enum color
		{
			green,
			red,
			blue
		}
	}
}
