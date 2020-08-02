using System;
using System.Collections.Generic;

namespace A3
{
    public class Q6FibonacciMod
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
