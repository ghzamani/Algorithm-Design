using System;

namespace A3
{
    public class Q3FibonacciLastDigit
    {
		//static void Main(string[] args)
		//{
		//	Console.WriteLine(Solve(long.Parse(Console.ReadLine())));
		//}


		public static long Solve(long n)
        {
			int[] fib = new int[n + 1];

			if (n == 0)
				return 0;
			
			fib[0] = 0;
			fib[1] = 1;

			for(int i = 2; i <= n; i++)
			{
				fib[i] = (fib[i - 1] + fib[i - 2]) % 10;
			}

			return fib[n];
		}
    }
}
