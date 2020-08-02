using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace A7
{
    public class Q1MaximumGold 
    {
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long capacity = long.Parse(input[0]);
		//	long[] goldBars = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
		//	Console.WriteLine(Solve(capacity,goldBars));
		//}
		public static long Solve(long W, long[] goldBars)
        {
			long[,] values = new long[goldBars.Length + 1, W + 1];

			for (int i = 0; i <= W; i++)
				values[0, i] = 0;
			for (int i = 1; i <= goldBars.Length; i++)
				values[i, 0] = 0;

			for(int i = 1; i <= goldBars.Length; i++)
			{
				for(int j = 1; j <= W; j++)
				{
					if (j >= goldBars[i - 1])
					{
						values[i, j] = Math.Max(values[i - 1, j - goldBars[i - 1]] + goldBars[i - 1],
							values[i - 1, j]);
					}
					else values[i, j] = values[i - 1, j];
				}
			}
			return values[goldBars.Length, W];
        }
    }
}
