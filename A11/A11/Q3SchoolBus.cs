using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11
{
    public class Q3SchoolBus : Processor
    {
        public Q3SchoolBus(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)=>
        TestTools.Process(inStr, (Func<long, long[][], Tuple<long, long[]>>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.TSPVerifier;

		public static List<int[]> subsets;
		public virtual Tuple<long, long[]> Solve(long nodeCount, long[][] edges)
        {
			//all subsets of a specific size
			subsets = new List<int[]>();
			//Subsets(nums, nums.Length, M, 0, new int[M], 0);


			long[,] graph = new long[nodeCount, nodeCount];
			for (int i = 0; i < edges.Length; i++)
			{
				long u = edges[i][0] - 1;
				long v = edges[i][1] - 1;
				long w = edges[i][2];

				graph[u, v] = w;
				graph[v, u] = w;
			}
			throw new NotImplementedException();
        }


		//public static long TSP()
		//{

		//}

		public static void Subsets(int[] array, int n, int r, int index, int[] data, int i)
		{
			if (index == r)
			{
				int[] a = new int[r];
				for (int idx = 0; idx < r; idx++)
				{
					a[idx] = data[idx];
				}
				subsets.Add(a);
				return;
			}

			if (i >= n)
				return;

			data[index] = array[i];
			Subsets(array, n, r, index + 1, data, i + 1);
			Subsets(array, n, r, index, data, i + 1);

		}
	}
}
