using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A5
{
    public class Q3ImprovingQuickSort
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] nums = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();

		//	long[] result = Solve(n, nums);
		//	foreach (var number in result)
		//		Console.Write(number + " ");
		//}
		public static long[] Solve(long n, long[] a)
        {
			QuickSort(a, 0, a.Length - 1);
			return a;
        }

		public static void QuickSort(long[] a, int l, int r)
		{
			if (l >= r)
				return;

			int m1, m2;
			Tuple<int,int> t = Partition3(a, l, r);
			m1 = t.Item1;
			m2 = t.Item2;
			QuickSort(a, l, m1 - 1);
			QuickSort(a, m2 + 1, r);
		}

		public static Tuple<int,int> Partition3(long[] a, int l, int r)
		{			
			int m1 = l;
			int i = l;
			int m2 = r;
			long pivot = a[l];
			while (i <= m2)
			{
				if(a[i] < pivot)
				{
					Swap(ref a[m1], ref a[i]);
					m1++;
					i++;
				}
				else
				{
					if (a[i] > pivot)
					{
						Swap(ref a[i], ref a[m2]);
						m2--;
					}
					else i++;
				}
			}
			return new Tuple<int, int>(m1, m2);
		}

		private static void Swap(ref long v1, ref long v2)
		{
			long hold = v1;
			v1 = v2;
			v2 = hold;			
		}
	}
}
