using System;
using System.Linq;

namespace A9
{
    public class Q2MergingTables
    {
		//public static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split();
		//	int n = int.Parse(input[0]);
		//	int m = int.Parse(input[1]);

		//	long[] tableSizes = Console.ReadLine().Split().Select(x => long.Parse(x)).ToArray();
		//	long[] sourceTables = new long[m];
		//	long[] targetTables = new long[m];
		//	for (int i = 0; i < m; i++)
		//	{
		//		input = Console.ReadLine().Split();
		//		sourceTables[i] = long.Parse(input[0]);
		//		targetTables[i] = long.Parse(input[1]);
		//	}

		//	//Q2MergingTables q2 = new Q2MergingTables();
		//	long[] result = Solve(tableSizes, targetTables, sourceTables);
		//	for (int i = 0; i < result.Length; i++)
		//	{
		//		Console.WriteLine(result[i]);
		//	}
		//}

		long[] parent;
        long[] tableSizes;
        long[] rank;

		public static long[] Solve(long[] tableSizes, long[] targetTables, long[] sourceTables)
		{
			long[] par = new long[tableSizes.Length + 1];

			long[] ans = new long[targetTables.Length];

			for (int i = 0; i < tableSizes.Length + 1; ++i)
			{
				par[i] = i;
			}

			long mx = Int64.MinValue;

			for (int i = 0; i < tableSizes.Length; ++i)
			{
				mx = Math.Max(mx, tableSizes[i]);
			}

			for (int i = 0; i < targetTables.Length; ++i)
			{
				targetTables[i] = find(targetTables[i], par);
				sourceTables[i] = find(sourceTables[i], par);
				if (targetTables[i] != sourceTables[i])
				{
					tableSizes[targetTables[i] - 1] += tableSizes[sourceTables[i] - 1];
					par[sourceTables[i]] = targetTables[i];
				}
				ans[i] = Math.Max(tableSizes[targetTables[i] - 1], mx);
				mx = ans[i];
			}
			return ans;
		}

		private static long find(long v, long[] par)
		{
			if (par[v] == v)
				return v;
			return par[v] = find(par[v], par);
		}

	}


}
