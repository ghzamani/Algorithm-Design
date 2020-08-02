using System;
using System.Collections.Generic;

namespace A3
{
    public class Q8FibonacciPartialSum
    {
		//static void Main(string[] args)
		//{
		//	string str = Console.ReadLine();
		//	long a = long.Parse(str.Split(' ')[0]);
		//	long b = long.Parse(str.Split(' ')[1]);
		//	Console.WriteLine(Solve(a, b));
		//}

		public static long Solve(long a, long b)
		{
			if (a == b)
				return Solve2(a) % 10;

			if (b < a)
			{
				long tmp = a;
				a = b;
				b = tmp;
			}
			//a should be smaller than b

			List<long> remainders = new List<long>();
			//دنباله پیزانو
			remainders.Add(0);
			remainders.Add(1);

			long sumOfPisano = 0;
			//last digit of sum of numbers in pisano period

			for (long i = 2; ; i++)
			{
				long rem = (remainders[(int)i - 1] + remainders[(int)i - 2]) % 10;

				if ((rem == 1) && (remainders[(int)i - 1] == 0))
				{
					remainders.RemoveAt((int)i - 1);
					break;
				}
				remainders.Add(rem);
			}
			long remaindersLength = remainders.Count;
			remainders.ForEach(x => sumOfPisano += x);
			sumOfPisano %= 10;

			long aCount = (long)(a / remaindersLength);			
			long bCount = (long)(b / remaindersLength);
			//a,b dar chandomin daste az pisano gharar migirand

			long senCountA = (long)(a % remaindersLength);
			long senCountB = (long)(b % remaindersLength);
			//a,b chandomin jomle az donbaleye khod hastand			

			long result = (bCount - aCount) * sumOfPisano % 10;
			for (long i = senCountA; i < remaindersLength; i++)
				result += remainders[(int)i];
			for (long i = 0; i <= senCountB; i++)
				result += remainders[(int)i];
			return result % 10;						
		}

		public static long Solve2(long n)
		{
			long[] fib = new long[n + 2];
			fib[0] = 0;
			fib[1] = 1;

			for (long i = 2; i <= n; i++)
			{
				fib[i] = fib[i - 1] + fib[i - 2];
			}
			return fib[n];
		}
    }
}
