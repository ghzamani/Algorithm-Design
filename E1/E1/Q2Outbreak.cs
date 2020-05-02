using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q2Outbreak : Processor
    {
        public Q2Outbreak(string testDataName) : base(testDataName)
		{
			ExcludeTestCaseRangeInclusive(5, 8);
			ExcludeTestCaseRangeInclusive(22, 25);
			ExcludeTestCaseRangeInclusive(69, 75);
			ExcludeTestCaseRangeInclusive(77, 78);
			ExcludeTestCases(80);
			ExcludeTestCaseRangeInclusive(82, 83);
			ExcludeTestCaseRangeInclusive(85, 87);
			ExcludeTestCases(97);
			ExcludeTestCaseRangeInclusive(117, 118);
			ExcludeTestCases(126);
			ExcludeTestCases(131);
			ExcludeTestCases(133);
			ExcludeTestCases(135);
		}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string>)Solve);

        public static Tuple<int, int, int[,], int[,]> ProcessQ2(string[] data)
        {
            var temp = data[0].Split();
            int N = int.Parse(temp[0]);
            int M = int.Parse(temp[1]);
            int[,] carriers = new int[N, 2];
            int[,] safe = new int[M, 2];
            for (int i = 0; i < N; i++)
            {
                carriers[i, 0] = int.Parse(data[i + 1].Split()[0]);
                carriers[i, 1] = int.Parse(data[i + 1].Split()[1]);
            }

            for (int i = 0; i < M; i++)
            {
                safe[i, 0] = int.Parse(data[i + N + 1].Split()[0]);
                safe[i, 1] = int.Parse(data[i + N + 1].Split()[1]);
            }
            return Tuple.Create(N, M, carriers, safe);
        }
        public string Solve(string[] input)
        {
            var data = ProcessQ2(input);
            return Solve(data.Item1,data.Item2,data.Item3,data.Item4).ToString();
        }

		long[] parent;
		long[] rank;
		public double Solve(int N, int M, int[,] carrier, int[,] safe)
        {
			//make list of edges
			//each safe has a edge to all carriers and other safes
			List<double[]> edges = new List<double[]>();
			for (int i = 0; i < M; i++)
			{
				for (int j = 0; j < N; j++)
				{
					double dist = Math.Sqrt(Math.Pow(safe[i, 0] - carrier[j, 0], 2) + Math.Pow(safe[i, 1] - carrier[j, 1], 2));
					edges.Add(new double[3] { i, j + M, dist });
				}
				for (int j = i + 1; j < M; j++)
				{
					double dist = Math.Sqrt(Math.Pow(safe[i, 0] - safe[j, 0], 2) + Math.Pow(safe[i, 1] - safe[j, 1], 2));
					edges.Add(new double[3] { i, j, dist });
				}
			}

			int pointsCount = N + M;
			parent = new long[pointsCount];
			rank = new long[pointsCount];

			for (int i = 0; i < pointsCount; i++)
				parent[i] = i; //make set

			edges = edges.OrderBy(x => x[2]).ToList();

			long count = 0;
			foreach (var edge in edges)
			{
				long u = (long)edge[0];
				long v = (long)edge[1];
				if (Find(u) != Find(v))
				{
					count++;
					if (count == pointsCount - 1)
						return Math.Round(edge[2] ,6);
					Union(u, v);
				}
			}
			return 0;
		}
		
		public long Find(long i)
		{
			while (i != parent[i])
				i = parent[i];
			return i;
		}

		public void Union(long i, long j)
		{
			long iId = Find(i);
			long jID = Find(j);

			if (iId == jID)
				return;

			if (rank[iId] > rank[jID])
				parent[jID] = iId;
			else
			{
				parent[iId] = jID;
				if (rank[iId] == rank[jID])
					rank[jID]++;
			}
		}
	}
}
