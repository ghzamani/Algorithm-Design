using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace A6
{
    public class Q2PrimitiveCalculator
    {
		//static void Main(string[] args)
		//{
		//	long num = long.Parse(Console.ReadLine());

		//	long[] res = Solve(num);
		//	Console.WriteLine(res.Length-1);
		//	foreach (var r in res)
		//		Console.Write(res + " ");
		//}
		public static long[] Solve(long n)
        {
			long[] numbers = new long[n + 1];

			numbers[0] = numbers[1] = 1;
			for (long i = 2; i <= n; i++)
			{
				long min = numbers[i - 1]+ 1;

				if (i % 2 == 0)
				{
					long item2 = numbers[i / 2];
					if (item2 + 1 < min)
					{
						min = item2 + 1;
					}
				}

				if (i % 3 == 0)
				{
					long item2 = numbers[i / 3];
					if (item2 + 1 < min)
					{
						min = item2 + 1;
					}
				}
				numbers[i] = min;				
			}
					

			long[] result = new long[numbers[n]];
			result[result.Count() - 1] = n;
			long j = result.Count() - 2;

			for (long i = n; i > 1;)
			{
				if (i % 3 == 0)
				{
					if (numbers[i / 3] + 1 == numbers[i])
					{
						i /= 3;
						result[j] = i;
						j--;
						continue;
					}
				}

				if (i % 2 == 0)
				{
					if (numbers[i / 2] + 1 == numbers[i])
					{
						i /= 2;
						result[j] = i;
						j--;
						continue;
					}					
				}

				i--;
				result[j] = i;
				j--;
				continue;
			}

			return result;
		}
	}
}
