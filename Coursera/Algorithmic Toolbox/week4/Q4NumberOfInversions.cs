using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace A5
{
    public class Q4NumberOfInversions
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] nums = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

		//	Console.WriteLine(Solve(n,nums));
		//}
		public static long Solve(long n, long[] a)
        {
			long counter = 0;
			MergeSort(n, a, ref counter);
			return counter;			
        }

		public static long[] MergeSort(long n, long[] a, ref long inversions)
		{
			int mid = (int)n / 2;

			long[] sorted = new long[n];

			if (n == 1)
			{
				return a.Take(1).ToArray();
			}
			else
			{
				long[] firstPart = MergeSort(mid, a.ToList().Take(mid).ToArray(), ref inversions);
				long[] secPart = MergeSort(n - mid, a.ToList().Skip(mid).ToArray(), ref inversions);

				sorted = Merge(firstPart, secPart, ref inversions);				
			}		
			return sorted;
		}


		public static long[] Merge(long[] firstPart, long[] secPart, ref long res)
		{
			long[] result = new long[firstPart.Length + secPart.Length];
			int i = 0; //for iterating in firstPart
			int j = 0; //for iterating in secPart
			int k = 0; //for iterating in result

			while (i < firstPart.Length && j < secPart.Length)
			{
				if (firstPart[i] <= secPart[j])
				{
					result[k] = firstPart[i];
					i++;
				}
				else
				{
					result[k] = secPart[j];
					j++; 
					res = res + firstPart.Length - i;
				}
				k++;
			}

			for (int a = i; a < firstPart.Length; a++)
			{
				result[k] = firstPart[a];
				k++;
			}

			for (int b = j; b < secPart.Length; b++)
			{
				result[k] = secPart[b];
				k++;

			}

			return result;
		}
	}
}
