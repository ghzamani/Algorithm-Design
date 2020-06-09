using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;
using Microsoft.SolverFoundation.Solvers;

namespace A11
{
    public class Q1CircuitDesign : Processor
    {
        public Q1CircuitDesign(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, 
                (Func<long, long, long[][], Tuple<bool, long[]>>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatAssignmentVerifier;

		public static List<long>[] graph;
		public static List<long>[] reverseGraph;

		public static long[] sccNum;
		//index i -> node i
		//sccNum[i] -> node i belonging to which scc

		public static bool[] visited;
		public static long[] previsit;

		//first item is index, second is the num of postvisit
		public static Tuple<long, long>[] postvisit;
		public static long clock;

		public static List<List<long>> sccomponents;

		public virtual Tuple<bool, long[]> Solve(long v, long c, long[][] cnf)
        {
			ImplicationGraph(v, cnf);
			SCCs(graph);

			for (int i = 0; i < sccomponents.Count; i++)
			{
				sccomponents[i] = sccomponents[i].OrderBy(x => x).ToList(); //sort to find whether x and ~x are both in scc
				for (int j = 0; j < sccomponents[i].Count - 1; j++)
				{
					if (sccomponents[i][j + 1] - sccomponents[i][j] == 1)
						if (sccomponents[i][j] % 2 == 0)
							return new Tuple<bool, long[]>(false, new long[0]);
				}
			}
					   
			List<long>[] metagraph = MetaGraph(graph, sccomponents);
			DFS(metagraph);

			postvisit = postvisit.OrderBy(x => x.Item2).ToArray();
			long[] result = new long[v];

			for (int i = 0; i < sccomponents.Count; i++)
			{
				foreach(var cmp in sccomponents[(int)postvisit[i].Item1])
				{
					if (result[cmp / 2] == 0) //not assigned yet
					{
						if (cmp % 2 == 0)//x
							result[cmp / 2] = (cmp + 2) / 2;
						else //~x
							result[cmp / 2] = -1 * (cmp + 2) / 2;
					}
				}
			}

			return new Tuple<bool, long[]>(true, result);

		}
					
		public static void ImplicationGraph (long v, long[][] cnf)
		{
			graph = new List<long>[2 * v];
			reverseGraph = new List<long>[2 * v];
			//adjacancy list
			//two nodes for each variable e.g:
			//variable 0 -> node 0 for positive, node 1 for negative
			//variable 1 -> node 2 for positive, node 3 for negative
			for (int i = 0; i < 2 * v; i++)
			{
				graph[i] = new List<long>();
				reverseGraph[i] = new List<long>();
			}		
			
			//~p or q
			//p -> q
			//~q -> ~p
			for (int i = 0; i < cnf.Length; i++)
			{
				long firstVar = (Math.Abs(cnf[i][0]) - 1) * 2;
				long secVar = (Math.Abs(cnf[i][1]) - 1) * 2;

				if (cnf[i][0] > 0)
				{
					if(cnf[i][1] > 0)
					{
						graph[firstVar + 1].Add(secVar);
						graph[secVar + 1].Add(firstVar);

						reverseGraph[secVar].Add(firstVar + 1);
						reverseGraph[firstVar].Add(secVar + 1);
					}
					else
					{
						graph[firstVar + 1].Add(secVar + 1);
						graph[secVar].Add(firstVar);

						reverseGraph[secVar + 1].Add(firstVar + 1);
						reverseGraph[firstVar].Add(secVar);
					}
				}

				else
				{
					if(cnf[i][1] > 0)
					{
						graph[firstVar].Add(secVar);
						graph[secVar + 1].Add(firstVar + 1);

						reverseGraph[secVar].Add(firstVar);
						reverseGraph[firstVar + 1].Add(secVar + 1);
					}
					else
					{
						graph[firstVar].Add(secVar + 1);
						graph[secVar].Add(firstVar + 1);

						reverseGraph[secVar + 1].Add(firstVar);
						reverseGraph[firstVar + 1].Add(secVar);
					}
				}
			}
		}

		public static List<long>[] MetaGraph(List<long>[] graph, List<List<long>> sccomponents)
		{
			List<long>[] metaGraph = new List<long>[sccomponents.Count];
			for (int i = 0; i < metaGraph.Length; i++)
			{
				metaGraph[i] = new List<long>();
			}

			for (int i = 0; i < sccomponents.Count; i++)
			{
				for (int j = 0; j < sccomponents[i].Count; j++)
				{
					long u = sccomponents[i][j];
					for (int k = 0; k < graph[u].Count; k++)
					{
						if (sccNum[u] != sccNum[graph[u][k]])
						{
							metaGraph[i].Add(sccNum[graph[u][k]]);
						}
					}
				}
			}

			return metaGraph;
		}
		
		public static void SCCs(List<long>[] graph)
		{
			DFS(reverseGraph);

			//reverse postOrder
			postvisit = postvisit.OrderByDescending(x => x.Item2).ToArray();
			
			long sccIdxCount = 0;
			sccomponents = new List<List<long>>();
			sccNum = new long[graph.Length];
			visited = new bool[graph.Length];
			for (int i = 0; i < postvisit.Length; i++)
			{
				if (!visited[postvisit[i].Item1])
				{
					//after sorting (descending) by postorder, need to run Explore on graph
					sccomponents.Add(new List<long>());
					Explore(postvisit[i].Item1, sccIdxCount);
					sccIdxCount++;
				}
			}			
		}

		public static void DFS (List<long>[] graph)
		{
			visited = new bool[graph.Length];
			postvisit = new Tuple<long, long>[graph.Length];
			previsit = new long[graph.Length];
			clock = 0;

			for (int i = 0; i < graph.Length; i++)
			{
				if (!visited[i])
					Explore(i, graph);
			}
		}

		public static void Explore(long v, List<long>[] graph)
		{
			visited[v] = true;
			Previsit(v);
			foreach (var u in graph[v])
				if (!visited[u])
					Explore(u, graph);				
			Postvisit(v);
		}
		
		//this version of explore also adds the components
		public static void Explore(long v, long sccIdxCount)
		{
			visited[v] = true;
			sccomponents[(int)sccIdxCount].Add(v); 
			sccNum[v] = sccIdxCount;
			
			foreach (var u in graph[v])
				if (!visited[u])
				{
					Explore(u, sccIdxCount);
				}
		}

		public static void Postvisit(long v)
		{
			postvisit[v] = new Tuple<long, long>(v, clock);
			clock++;
		}

		public static void Previsit(long v)
		{
			previsit[v] = clock;
			clock++;
		}
	}
}
