using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace A5
{
    public class Q5OrganizingLottery
    {
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long s = long.Parse(input[0]);
		//	//long p = long.Parse(input[1]);

		//	long[] starts = new long[s];
		//	long[] ends = new long[s];
		//	int i = 0;
		//	while (i < s)
		//	{
		//		input = Console.ReadLine().Split(' ');
		//		starts[i] = long.Parse(input[0]);
		//		ends[i] = long.Parse(input[1]);
		//		i++;
		//	}

		//	long[] points = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

		//	long[] result = Solve(points, starts, ends);

		//	foreach (var n in result)
		//		Console.Write(n + " ");
		//}
		public enum e
		{
			left = 0,
			point = 1,
			right = 2			
		}
        public static long[] Solve(long[] points, long[] startSegments, long[] endSegment)
        {
			long[] result = new long[points.Length];
			Tuple<long, e, int>[] array = new Tuple<long, e, int>[startSegments.Length + points.Length + endSegment.Length];
			int i = 0;
			for(i = 0; i < startSegments.Length; i++)
			{
				array[i] = Tuple.Create(startSegments[i], e.left, 0);
			}

			for (int k = 0; k < points.Length; k++)
			{
				array[i] = Tuple.Create(points[k], e.point, k);
				i++;
			}

			for (int j = 0; j < endSegment.Length; j++)
			{
				array[i] = Tuple.Create(endSegment[j], e.right,0);
				i++;
			}	

			array = array.OrderBy(x => x.Item1).ToArray();

			long leftCount = 0;
			long rightCount = 0;
			
			for (int j = 0; j < array.Length; j++)
			{

				if (array[j].Item2 == e.left)
					leftCount++;
					

				if (array[j].Item2 == e.right)
					rightCount++;
				
				if (array[j].Item2 == e.point)
					result[array[j].Item3] = leftCount - rightCount;
				
			}
			return result;
        }
				
	}
}
