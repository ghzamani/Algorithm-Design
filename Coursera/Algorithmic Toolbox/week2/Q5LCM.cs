using System;

namespace A3
{
    public class Q5LCM 
    {
		//static void Main(string[] args)
		//{
		//	string str = Console.ReadLine();
		//	long a = long.Parse(str.Split(' ')[0]);
		//	long b = long.Parse(str.Split(' ')[1]);
		//	Console.WriteLine(a * b / Solve(a,b));
		//}

		public static long Solve(long a, long b)
		{
			if (a < b)
			{
				long tmp = a;
				a = b;
				b = tmp;
			}
   

			if (b == 0)
				return a;

			return Solve(b, a % b);
		}
		
    }
}
