using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace A4
{
    public class Q5MaximizeNumberOfPrizePlaces
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] nums = Solve(n);

		//	Console.WriteLine(nums.Length);
		//	foreach (var num in nums)
		//		Console.Write($"{num} ");
		//}

		public static long[] Solve(long n)
        {
			if (n < 3)
				return new long[] { n };

			List<long> sum = new List<long>();
			sum.Add(1);
			int i;
			for (i = 1; ; i++)
			{
				long a = sum[i - 1] + i + 1;
				sum.Add(a);
				if (n < sum[i])
					break;
			}

			long[] result = new long[i];
			for(int j = 0; j < i -1; j++)
			{
				result[j] = j + 1;
			}
			result[i - 1] = n - sum[i - 2];
			return result;
        }
    }
}

