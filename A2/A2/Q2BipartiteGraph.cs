using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A2
{
	public class Q2BipartiteGraph : Processor
	{
		public Q2BipartiteGraph(string testDataName) : base(testDataName)
		{}

		public override string Process(string inStr) =>
			TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

		public long Solve(long NodeCount, long[][] edges)
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

			int[] NodesColor = new int[NodeCount];
			//no color (not seen yet) -> 0
			//other values -> 1, -1
			
			for(int i = 0; i < NodeCount; i++)
			{
				//if not visited
				if (NodesColor[i] == 0)
				{
					long StartNode = i + 1;
					NodesColor[StartNode - 1] = 1;

					Queue<long> q = new Queue<long>();
					q.Enqueue(StartNode);
					while (q.Count != 0)
					{
						long u = q.Dequeue();
						foreach (var v in adjList[u - 1])
						{
							if (NodesColor[v - 1] == 0)
							{
								if (NodesColor[u - 1] == 1)
									NodesColor[v - 1] = -1;
								else NodesColor[v - 1] = 1;
								q.Enqueue(v);
							}

							else if (NodesColor[u - 1] == NodesColor[v - 1])
								return 0;
						}
					}
				}
			}

			return 1;
		}

	}
}
