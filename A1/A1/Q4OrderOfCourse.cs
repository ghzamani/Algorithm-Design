﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

		public Dictionary<long, List<long>> graph;
		public bool[] visited;
		public long[] previsit;

		//first item is index, second is the num of postvisit
		public Tuple<long, long>[] postvisit;
		public long clock;
		public long[] Solve(long nodeCount, long[][] edges)
		{
			graph = Q3Acyclic.MakeDiGraph(nodeCount, edges);
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


		public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
