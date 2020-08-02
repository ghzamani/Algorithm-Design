using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A12
{
    public class Q5StronglyConnected
    {
		//static void Main(string[] args)
		//{
			
		//}
		


		public Dictionary<long, List<long>> graph;
		public bool[] visited;
		public long Solve(long nodeCount, long[][] edges)
        {
			graph = Q4OrderOfCourse.MakeDiGraph(nodeCount, edges);
			visited = new bool[nodeCount];			

			long scc = 0;
			long[] result = new long[nodeCount];

			//generating the reverse graph
			foreach(var e in edges)
			{
				var tmp = e[0];
				e[0] = e[1];
				e[1] = tmp;
			}

			//reverse post order of reverse graph
			Q4OrderOfCourse q = new Q4OrderOfCourse();
			result = q.Solve(nodeCount, edges);

			//for v in {V} in reverse postorder
			foreach (var r in result)
			{
				if (!visited[r - 1])
				{
					Explore(r);
					scc++;
				}
			}
			return scc;
		}		

		public void Explore(long v)
		{
			visited[v - 1] = true;
			foreach (var u in graph[v])
				if (visited[u - 1] == false)
					Explore(u);
		}
	}


	public class Q4OrderOfCourse
	{
		public Dictionary<long, List<long>> graph;
		public bool[] visited;
		public long[] previsit;

		//first item is index, second is the num of postvisit
		public Tuple<long, long>[] postvisit;
		public long clock;
		public long[] Solve(long nodeCount, long[][] edges)
		{
			graph = MakeDiGraph(nodeCount, edges);
			visited = new bool[nodeCount];
			previsit = new long[nodeCount];
			postvisit = new Tuple<long, long>[nodeCount];
			clock = 0;

			for (int v = 1; v <= nodeCount; v++)
				if (!visited[v - 1])
					Explore(v);

			postvisit = postvisit.OrderByDescending(x => x.Item2).ToArray();

			long[] result = new long[nodeCount];
			for (int i = 0; i < nodeCount; i++)
				result[i] = postvisit[i].Item1;

			return result;
		}

		public void Explore(long v)
		{
			visited[v - 1] = true;
			Previsit(v);
			foreach (var u in graph[v])
				if (visited[u - 1] == false)
					Explore(u);
			Postvisit(v);
		}

		private void Postvisit(long v)
		{
			postvisit[v - 1] = new Tuple<long, long>(v, clock);
			clock++;
		}

		private void Previsit(long v)
		{
			previsit[v - 1] = clock;
			clock++;
		}

		public static Dictionary<long, List<long>> MakeDiGraph(long nodeCount, long[][] edges)
		{
			Dictionary<long, List<long>> graph =
				new Dictionary<long, List<long>>((int)nodeCount);

			for (int i = 1; i <= nodeCount; i++)
				graph.Add(i, new List<long>());

			foreach (var e in edges)
				graph[e[0]].Add(e[1]);

			return graph;
		}
	}
}
