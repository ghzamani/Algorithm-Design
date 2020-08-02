using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace A3
{
    public class Q2DetectingAnomalies
    {
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long nodeCount = long.Parse(input[0]);
		//	long edgeCount = long.Parse(input[1]);

		//	long[][] edges = new long[edgeCount][];			

		//	long j = 0;
		//	while (j < edgeCount)
		//	{
		//		input = Console.ReadLine().Split(' ');
		//		long u = long.Parse(input[0]) - 1;
		//		long v = long.Parse(input[1]) - 1;
		//		long w = long.Parse(input[2]);
		//		edges[j] = new long[3] { u, v, w };
		//		j++;
		//	}
		//	Q2DetectingAnomalies bellman = new Q2DetectingAnomalies();
		//	Console.WriteLine(bellman.Solve(nodeCount,edges));
		//}
		
		public long[] dist;

        public long Solve(long nodeCount, long[][] edges)
        {
			long[] dist = new long[nodeCount];

			long s = 0;
			for (int i = 0; i < nodeCount; i++)
				dist[i] = int.MaxValue;
			dist[s] = 0;

			for(int i = 0; i < nodeCount - 1; i++)
			{
				foreach(var edge in edges)
				{
					long u = edge[0];
					long v = edge[1];
					long w = edge[2];

					if (dist[v] > dist[u] + w)
						dist[v] = dist[u] + w;
				}
			}

			foreach(var edge in edges)
			{
				long u = edge[0];
				long v = edge[1];
				long w = edge[2];

				if (dist[v] > dist[u] + w)
					return 1;
			}
			return 0;
        }
    }
}
