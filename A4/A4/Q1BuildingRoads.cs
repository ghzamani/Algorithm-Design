using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
	public class Q1BuildingRoads : Processor
	{
		public Q1BuildingRoads(string testDataName) : base(testDataName)
		{}

		public override string Process(string inStr) =>
			TestTools.Process(inStr, (Func<long, long[][], double>)Solve);

		public double Solve(long pointCount, long[][] points)
		{
			//dense graph of cities
			List<Tuple<long, double>>[] edges = new List<Tuple<long, double>>[pointCount];
			//first item -> neigh node
			//second item -> dist

			for (int i = 0; i < pointCount; i++)
				edges[i] = new List<Tuple<long, double>>((int)pointCount - 1);
						
			for (int i = 0; i < pointCount -1; i++)
			{
				for (int j = i+1; j < pointCount; j++)
				{
					double d = Dist(points[i], points[j]);
					edges[i].Add(new Tuple<long, double>(j, d));
					edges[j].Add(new Tuple<long, double>(i, d));
				}
			}

			//implementing Prim
			double[] cost = new double[pointCount];
			for (int i = 0; i < pointCount; i++)
				cost[i] = int.MaxValue;
			
			cost[0] = 0;

			SimplePriorityQueue<long, double> Q = new SimplePriorityQueue<long, double>();
			for (int i = 0; i < pointCount; i++)
				Q.Enqueue(i, cost[i]);			

			while (Q.Count != 0)
			{
				long v = Q.Dequeue();
				foreach (var z in edges[v])
				{
					if (Q.Contains(z.Item1) && cost[z.Item1] > z.Item2)
					{
						cost[z.Item1] = z.Item2;
						Q.UpdatePriority(z.Item1, cost[z.Item1]);
					}
				}
			}

			double result = 0;
			foreach (var c in cost)
				result += c;
			
			return Math.Round(result, 6);
		}

		public static double Dist(long[] u, long[] v) =>
			Math.Sqrt(Math.Pow(u[0] - v[0], 2) + Math.Pow(u[1] - v[1], 2));
	}
}
