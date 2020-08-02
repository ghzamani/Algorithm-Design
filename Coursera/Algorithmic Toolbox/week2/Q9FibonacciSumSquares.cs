using System;
using System.Collections.Generic;

namespace A3
{
    public class Q9FibonacciSumSquares
    {
		//static void Main(string[] args)
		//{
		//	Console.WriteLine(Solve(long.Parse(Console.ReadLine())));
		//}


		public static long Solve(long n)
        {
			long nthFibMod = Solve2(n, 10);
			long nMinusOneFibMod = Solve2(n-1, 10);

			return (nthFibMod + nMinusOneFibMod) * nthFibMod % 10;			
        }

		public static long Solve2(long a, long b)
		{
			List<long> remainders = new List<long>();
			//دنباله پیزانو با مد 
			//b
			remainders.Add(0);
			remainders.Add(1);

			for (int i = 2; ; i++)
			{
				long rem = (remainders[i - 1] + remainders[i - 2]) % b;

				if ((rem == 1) &&
					(remainders[i - 1] == 0))
				{
					remainders.RemoveAt(i - 1);
					break;
				}
				remainders.Add(rem);
			}

			return remainders[(int)(a % remainders.Count)];
		}
	}
}
