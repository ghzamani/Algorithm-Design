using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace A12
{
 //   public class Q4OrderOfCourse
 //   {
	//	static void Main(string[] args)
	//	{
	//		var input = Console.ReadLine().Split(' ');
	//		long nodeCount = long.Parse(input[0]);
	//		long edgeCount = long.Parse(input[1]);
	//		long[][] edges = new long[edgeCount][];

	//		int i = 0;
	//		while (i < edgeCount)
	//		{
	//			var str = Console.ReadLine().Split(' ');
	//			edges[i] = new long[2] { long.Parse(str[0]), long.Parse(str[1]) };
	//			i++;
	//		}
	//		Q4OrderOfCourse order = new Q4OrderOfCourse();
	//		long[] result = order.Solve(nodeCount, edges);
	//		foreach (var r in result)
	//			Console.Write(r + " ");
	//	}

	//	public Dictionary<long, List<long>> graph;
	//	public bool[] visited;
	//	public long[] previsit;

	//	//first item is index, second is the num of postvisit
	//	public Tuple<long, long>[] postvisit;
	//	public long clock;
	//	public long[] Solve(long nodeCount, long[][] edges)
 //       {
	//		graph = MakeDiGraph(nodeCount, edges);
	//		visited = new bool[nodeCount];
	//		previsit = new long[nodeCount];
	//		postvisit = new Tuple<long, long>[nodeCount];
	//		clock = 0;

	//		for (int v = 1; v <= nodeCount; v++)
	//			if (!visited[v - 1])
	//				Explore(v);

	//		postvisit = postvisit.OrderByDescending(x => x.Item2).ToArray();

	//		long[] result = new long[nodeCount];
	//		for(int i = 0; i < nodeCount; i++)
	//			result[i] = postvisit[i].Item1;
			
	//		return result;
 //       }

	//	public void Explore(long v)
	//	{
	//		visited[v - 1] = true;
	//		Previsit(v);
	//		foreach (var u in graph[v])
	//			if (visited[u - 1] == false)
	//				Explore(u);
	//		Postvisit(v);
	//	}

	//	private void Postvisit(long v)
	//	{
	//		postvisit[v - 1] = new Tuple<long, long>(v, clock);
	//		clock++;
	//	}

	//	private void Previsit(long v)
	//	{
	//		previsit[v - 1] = clock;
	//		clock++;
	//	}

	//	public static Dictionary<long, List<long>> MakeDiGraph(long nodeCount, long[][] edges)
	//	{
	//		Dictionary<long, List<long>> graph =
	//			new Dictionary<long, List<long>>((int)nodeCount);

	//		for (int i = 1; i <= nodeCount; i++)
	//			graph.Add(i, new List<long>());

	//		foreach (var e in edges)
	//			graph[e[0]].Add(e[1]);

	//		return graph;
	//	}
	//}
}
