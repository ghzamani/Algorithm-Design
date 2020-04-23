using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q2Outbreak : Processor
    {
        public Q2Outbreak(string testDataName) : base(testDataName) { }

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
			//carrier and safe are vice versa
			long pointsCount = N + M;
			double[][] edges = new double[pointsCount * (pointsCount - 1) / 2][];
			int k = 0;
			int i = 0;
			long[][] points = new long[pointsCount][];
			for (k = 0; k < pointsCount && i < N; k++)
			{
				points[k] = new long[2] { carrier[i, 0], carrier[i, 1] };
				i++;
			}
			i = 0;
			for (; k < pointsCount && i < M; k++)
			{
				points[k] = new long[2] { safe[i, 0], safe[i, 1] };
				i++;
			}

			for (i = 0; i < pointsCount - 1; i++)
			{
				for (int j = i + 1; j < pointsCount; j++)
				{
					edges[k] = new double[] { i, j, Dist(points[i], points[j]) };
				}
			}

			parent = new long[pointsCount];
			rank = new long[pointsCount];

			for (i = 0; i < pointsCount; i++)
				parent[i] = i; //make set

			edges = edges.OrderBy(x => x[2]).ToArray();

			long count = 0;
			foreach (var edge in edges)
			{
				long u = (long)edge[0];
				long v = (long)edge[1];
				if (Find(u) != Find(v))
				{
					count++;
					//if (pointsCount - (clusterCount - 1) == count)
					//	return Math.Round(edge[2], 6);
					if (count == pointsCount - 1)
						return edge[2];
					Union(u, v);
				}
			}

			return 0;
        }

		public static double Dist(long[] u, long[] v) =>
			Math.Sqrt(Math.Pow(u[0] - v[0], 2) + Math.Pow(u[1] - v[1], 2));

		public double Solve(long pointCount, long[][] points, long clusterCount)
		{
			long count = 0;

			double[][] edges = new double[pointCount * (pointCount - 1) / 2][];

			long k = 0;
			for (int i = 0; i < pointCount - 1; i++)
			{
				for (int j = i + 1; j < pointCount; j++)
				{
					//edges[k] = new double[] { i, j, Dist(points[i], points[j]) };
					k++;
				}
			}




			parent = new long[pointCount];
			rank = new long[pointCount];

			for (int i = 0; i < pointCount; i++)
				parent[i] = i; //make set

			edges = edges.OrderBy(x => x[2]).ToArray();

			foreach (var edge in edges)
			{
				long u = (long)edge[0];
				long v = (long)edge[1];
				if (Find(u) != Find(v))
				{
					count++;
					if (pointCount - (clusterCount - 1) == count)
						return Math.Round(edge[2], 6);
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
