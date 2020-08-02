using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A6
{
    public class Q3EditDistance
    {
		//static void Main(string[] args)
		//{
		//	long res = Solve(Console.ReadLine(), Console.ReadLine());
		//	Console.WriteLine(res);
		//}
		public static long Solve(string str1, string str2)
        {
			long[,] distance = new long[str2.Length + 1, str1.Length + 1];

			//first row
			for (int i = 0; i <= str1.Length; i++)
				distance[0, i] = i;

			//first column
			for (int i = 0; i <= str2.Length; i++)
				distance[i, 0] = i;

			//i for row
			for(int i = 1; i <= str2.Length; i++)
			{
				//j for column
				for(int j = 1; j <= str1.Length; j++)
				{
					long insertion = distance[i, j - 1] + 1;
					long deletion = distance[i - 1, j] + 1;
					long matchOrNot = distance[i - 1, j - 1];

					if (str1[j - 1] != str2[i - 1])
						matchOrNot++;

					distance[i, j] = Math.Min(matchOrNot
						, Math.Min(insertion, deletion));				
				}
			}
			return distance[str2.Length, str1.Length];
        }
    }
}
