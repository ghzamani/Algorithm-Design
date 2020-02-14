using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

		public Dictionary<long, List<long>> graph;
		public bool[] visited;
		public long[] previsit;
		public long[] postvisit;
		public long clock;
		public long Solve(long nodeCount, long[][] edges)
		{
			graph = MakeDiGraph(nodeCount, edges);
			visited = new bool[nodeCount];
			previsit = new long[nodeCount];
			postvisit = new long[nodeCount];
			clock = 0;

			for (int v = 1; v <= nodeCount; v++)
				if (!visited[v - 1])
					Explore(v);

			//after applying DFS, should check for back edges
			//(u,v) --> post[v] > post[u] --> back edge
			foreach (var e in edges)
				if (postvisit[e[1] - 1] > postvisit[e[0] - 1])
					return 1;

			return 0;
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
			postvisit[v - 1] = clock;
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