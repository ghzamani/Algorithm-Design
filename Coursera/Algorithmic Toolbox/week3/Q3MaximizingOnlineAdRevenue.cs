using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4
{
    public class Q3MaximizingOnlineAdRevenue
    {

		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] perclick = new long[n];
		//	long[] avgclicks = new long[n];

		//	var input = Console.ReadLine().Split(' ');
		//	var input2 = Console.ReadLine().Split(' ');
		//	for (int i = 0; i < n; i++)
		//	{
		//		perclick[i] = long.Parse(input[i]);
		//		avgclicks[i] = long.Parse(input2[i]);
		//	}
		//	Console.WriteLine(Solve(n,perclick,avgclicks));
		//}
		public static long Solve(long slotCount, long[] adRevenue, long[] averageDailyClick)
        {
			long res = 0;
			List<long> adList = adRevenue.ToList();
			adList.Sort();

			List<long> avgList = averageDailyClick.ToList();
			avgList.Sort();

			for(int i = 0; i < slotCount; i++)
			{
				res += adList[i] * avgList[i];
			}
			return res;

        }
    }
}
