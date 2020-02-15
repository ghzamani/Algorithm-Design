using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

		public bool[] visited;
		public Dictionary<long, List<long>> graph;
		public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
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

		public void Explore(long v)
		{
			visited[v - 1] = true;
			foreach (var u in graph[v])
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
