using System;
using System.Collections.Generic;

namespace A3
{
    public class Q7FibonacciSum
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	Console.WriteLine(Solve(n));
		//}

		public static long Solve(long n)
        {
			List<long> remainders = new List<long>();
			//دنباله پیزانو
			remainders.Add(0);
			remainders.Add(1);

			for(int i=2; ; i++)
			{
				long rem = (remainders[i - 1] + remainders[i - 2]) % 10;

				if((rem == 1) && (remainders[i-1] == 0))
				{
					remainders.RemoveAt(i - 1);
					break;
				}
				remainders.Add(rem);
			}


			int count = (int) (n / remainders.Count % 10);

			long sum = 0;
			foreach (var num in remainders)
				sum += num;
			sum = sum % 10 * count;

			int b = (int)((n + 1) % remainders.Count);
			for (int i = 0; i < b; i++)
			{
				sum += remainders[i];
			}
			return sum % 10;
		}
    }
}
