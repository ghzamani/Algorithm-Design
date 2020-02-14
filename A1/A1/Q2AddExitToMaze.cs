using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

		public long cc;
		public bool[] visited;
		public Dictionary<long, List<long>> graph;
		public long Solve(long nodeCount, long[][] edges)
		{
			stack = new Stack<long>();
			cc = 0;
			graph = Q1MazeExit.MakeGraph(nodeCount, edges);
			visited = new bool[nodeCount];

			for (int v = 1; v <= nodeCount; v++)
			{
				if (!visited[v - 1])
				{
					cc++;
					Explore(v);
				}
			}
			return cc;
		}


		public Stack<long> stack;
		//non-recursive explore
		public void Explore(long v)
		{
			stack.Push(v);
			while (stack.Count != 0)
			{
				long current = stack.Pop();
				if (visited[current - 1])
					continue;

				visited[current - 1] = true;
				foreach (var e in graph[current])
					stack.Push(e);
			}
		}
	}
}
