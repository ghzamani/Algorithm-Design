using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace A4
{
    public class Q4CollectingSignatures
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] starts = new long[n];
		//	long[] ends = new long[n];

		//	int i = 0;
		//	while (i < n)
		//	{
		//		var input = Console.ReadLine().Split(' ');
		//		starts[i] = long.Parse(input[0]);
		//		ends[i] = long.Parse(input[1]);
		//		i++;
		//	}
		//}

		public static List<long> Solve(long tenantCount, long[] startTimes, long[] endTimes)
        {
			List<long> start = startTimes.ToList();
			List<long> end = endTimes.ToList();
			long result = 0;
			List<long> res = new List<long>();

			while(start.Count() != 0)
			{
				int minIndex = IndexOfMinStartTime(end);
				long endMinIndex = end[minIndex];
				List<int> toRemove = new List<int>();

				for(int i=0; i < tenantCount; i++)
					
					if ((start[i] <= endMinIndex) &&
						(end[i] >= endMinIndex))
						toRemove.Add(i);	
									
				foreach(var a in toRemove.OrderByDescending(x => x))
				{
					start.RemoveAt(a);
					end.RemoveAt(a);
				}
				res.Add(endMinIndex);
				result++;
				tenantCount -= toRemove.Count();
			}
            return res;
        }

		public static int IndexOfMinStartTime(List<long> start)
		{
			long min = long.MaxValue;
			int idx = 0;
			for(int i = 0; i < start.Count; i++)
			{
				if(start[i] < min)
				{
					min = start[i];
					idx = i;
				}
			}
			return idx;
		}
    }
}
