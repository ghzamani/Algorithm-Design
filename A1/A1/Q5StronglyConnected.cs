using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

		public Dictionary<long, List<long>> graph;
		public bool[] visited;
		public long Solve(long nodeCount, long[][] edges)
		{
			graph = Q3Acyclic.MakeDiGraph(nodeCount, edges);
			visited = new bool[nodeCount];

			long scc = 0;
			long[] result = new long[nodeCount];

			//generating the reverse graph
			foreach (var e in edges)
			{
				var tmp = e[0];
				e[0] = e[1];
				e[1] = tmp;
			}

			//reverse post order of reverse graph
			Q4OrderOfCourse q = new Q4OrderOfCourse(" ");
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
}
