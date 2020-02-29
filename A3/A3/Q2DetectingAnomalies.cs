using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

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
					long u = edge[0] - 1;
					long v = edge[1] - 1;
					long w = edge[2];

					if (dist[v] > dist[u] + w)
						dist[v] = dist[u] + w;
				}
			}

			foreach(var edge in edges)
			{
				long u = edge[0] - 1;
				long v = edge[1] - 1;
				long w = edge[2];

				if (dist[v] > dist[u] + w)
					return 1;
			}
			return 0;
        }
    }
}
