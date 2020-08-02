using System;

namespace A3
{
    public class Q4GCD
    {
		//static void Main(string[] args)
		//{
		//	string str = Console.ReadLine();
		//	var nums = str.Split(' ');

		//	Console.WriteLine(Solve(long.Parse(nums[0]),long.Parse(nums[1])));
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
