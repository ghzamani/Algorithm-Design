using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using GeoCoordinatePortable;
using Priority_Queue;

namespace A4
{
	public class Q3ComputeDistance : Processor
	{
		public Q3ComputeDistance(string testDataName) : base(testDataName) { }

		public static readonly char[] IgnoreChars = new char[] { '\n', '\r', ' ' };
		public static readonly char[] NewLineChars = new char[] { '\n', '\r' };
		private static double[][] ReadTree(IEnumerable<string> lines)
		{
			return lines.Select(line =>
				line.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
									 .Select(n => double.Parse(n)).ToArray()
							).ToArray();
		}
		public override string Process(string inStr)
		{
			return Process(inStr, (Func<long, long, double[][], double[][], long,
									long[][], double[]>)Solve);
		}
		public static string Process(string inStr, Func<long, long, double[][]
								  , double[][], long, long[][], double[]> processor)
		{
			var lines = inStr.Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
			long[] count = lines.First().Split(IgnoreChars,
											   StringSplitOptions.RemoveEmptyEntries)
										 .Select(n => long.Parse(n))
										 .ToArray();
			double[][] points = ReadTree(lines.Skip(1).Take((int)count[0]));
			double[][] edges = ReadTree(lines.Skip(1 + (int)count[0]).Take((int)count[1]));
			long queryCount = long.Parse(lines.Skip(1 + (int)count[0] + (int)count[1])
										 .Take(1).FirstOrDefault());
			long[][] queries = ReadTree(lines.Skip(2 + (int)count[0] + (int)count[1]))
										.Select(x => x.Select(z => (long)z).ToArray())
										.ToArray();

			return string.Join("\n", processor(count[0], count[1], points, edges,
								queryCount, queries));
		}


		public double[] Solve(long nodeCount,
							long edgeCount,
							double[][] points,
							double[][] edges,
							long queriesCount,
							long[][] queries)
		{
			bool[] visited;
			double[] pi;
			double[] dist;
			double[] result = new double[queriesCount];

			List<Edge>[] adjacencyList = new List<Edge>[nodeCount];
			for (int j = 0; j < nodeCount; j++)
			{
				adjacencyList[j] = new List<Edge>();
			}

			foreach (var e in edges)
			{
				double u = e[0] - 1;
				double v = e[1] - 1;
				double w = e[2];

				adjacencyList[(int)u].Add(new Edge(v, w));
			}

			int i = 0;
			foreach (var query in queries)
			{
				long source = query[0] - 1;
				long destination = query[1] - 1;

				result[i] = AStar(source, destination);
				i++;
			}

			return result;

			double AStar(long source, long destination)
			{
				if (source == destination)
					return 0;

				visited = new bool[nodeCount];
				dist = new double[nodeCount];

				pi = new double[nodeCount];

				//if ((int)points[0][0] != points[0][0])
				//{
				//	GeoCoordinate coordinate2 = new GeoCoordinate(points[destination][1], points[destination][0]);
				//	for (int j = 0; j < nodeCount; j++)
				//	{
				//		GeoCoordinate coordinate1 = new GeoCoordinate(points[j][1], points[j][0]);
				//		pi[j] = coordinate1.GetDistanceTo(coordinate2);
				//		dist[j] = int.MaxValue;
				//	}
				//}

				//else
				{
					for (int j = 0; j < nodeCount; j++)
					{
						pi[j] = Dist(points[destination], points[j]);
						dist[j] = int.MaxValue;
					}
				}
				dist[source] = 0;

				SimplePriorityQueue<long, double> heap = new SimplePriorityQueue<long, double>();
				for (int i = 0; i < nodeCount; i++)
				{
					heap.Enqueue(i, dist[i] + pi[i]); 
				}

				while (heap.Count != 0)
				{
					long u = heap.Dequeue();
					foreach (var edge in adjacencyList[u])
					{
						int v = (int)edge.V;

						if (!visited[v])
						{
							double w = edge.W;

							if (dist[v] > dist[u] + w)
							{
								dist[v] = dist[u] + w;
							}
							heap.UpdatePriority(v, dist[v] + pi[v]);
						}
					}
					visited[u] = true;

					if (visited[destination])
						return dist[destination] >= int.MaxValue ? -1 : dist[destination];
				}

				return -1;
			}
		}

		public class Edge
		{
			public double V;
			public double W;

			public Edge(double v, double w)
			{
				this.V = v;
				this.W = w;
			}
		}

		public static double Dist(double[] u, double[] v) =>
			Math.Sqrt(Math.Pow(u[0] - v[0], 2) + Math.Pow(u[1] - v[1], 2));
	}
}