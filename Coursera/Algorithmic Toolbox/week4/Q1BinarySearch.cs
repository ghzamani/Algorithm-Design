using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A5
{
    public class Q1BinarySearch
    {
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long aLength = long.Parse(input[0]);
		//	long[] a = input.Select(x => long.Parse(x)).Skip(1).ToArray();

		//	input = Console.ReadLine().Split(' ');
		//	long bLength = long.Parse(input[0]);
		//	long[] b = input.Select(x => long.Parse(x)).Skip(1).ToArray();

		//	long[] result = Solve(a, b);
		//	foreach (var num in result)
		//		Console.Write(num + " ");
		//}
		public static long[] Solve(long []a, long[] b) 
        {
			for(int i = 0; i < b.Length; i++)
			{
				b[i] = BinarySearch(a, b[i], 0, a.Length - 1);
			}

			return b;
        }

		public static int BinarySearch (long[] nums,long key, int low, int high)
		{
			if (low > high)
				return -1;

			int mid = (low + high) / 2;

			if (nums[mid] == key)
				return mid;

			if (key < nums[mid])
				return BinarySearch(nums, key, low, mid - 1);

			if (key > nums[mid])
				return BinarySearch(nums, key, mid + 1, high);

			return 0;
		}
    }
}
