using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class Q2Clustering
    {
		//static void Main(string[] args)
		//{
		//	long pointCount = long.Parse(Console.ReadLine());
		//	long[][] points = new long[pointCount][];

		//	int j = 0;
		//	while (j < pointCount)
		//	{
		//		var input = Console.ReadLine().Split(' ');
		//		points[j] = new long[2] { long.Parse(input[0]), long.Parse(input[1]) };
		//		j++;
		//	}
		//	long clusterCount = long.Parse(Console.ReadLine());
		//	Q2Clustering clustering = new Q2Clustering();
		//	Console.WriteLine(clustering.Solve(pointCount,points,clusterCount));
		//}
		
		long[] parent;
		long[] rank;
		public double Solve(long pointCount, long[][] points, long clusterCount)
        {
			long count = 0;

			double[][] edges = new double[pointCount * (pointCount - 1) / 2][];			

			long k = 0;
			for (int i = 0; i < pointCount - 1; i++)
			{
				for (int j = i + 1; j < pointCount; j++)
				{
					edges[k] = new double[] { i, j, Dist(points[i], points[j]) };
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

		public static double Dist(long[] u, long[] v)
		{
			return Math.Sqrt(Math.Pow(u[0] - v[0], 2) + Math.Pow(u[1] - v[1], 2));
		}

		public long Find (long i)
		{
			while (i != parent[i])
				i = parent[i];
			return i;
		}

		public void Union (long i,long j)
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
