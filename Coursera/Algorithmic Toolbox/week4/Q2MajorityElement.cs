using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A5
{
    public class Q2MajorityElement
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] nums = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
		//	Console.WriteLine(Solve(n,nums));
		//}
		public static long Solve(long n, long[] a)
        {
			long m = Majority(a);

			if (m == -1)
				return 0;

			return 1;
        }

		public static long Majority(long[] nums)
		{
			if (nums.Length == 3)
			{
				if ((nums[0] == nums[1]) ||
					(nums[0] == nums[2]))
					return nums[0];

				if (nums[1] == nums[2])
					return nums[1];

				return -1;
			}

			if (nums.Length == 1)
				return nums[0];

			if (nums.Length == 2)
			{
				if (nums[0] == nums[1])
				{
					return nums[0];
				}
				else return -1;
			}			

			long firstPart = Majority(nums.Take(nums.Length / 2).ToArray());
			long secPart = Majority(nums.Skip(nums.Length / 2).ToArray());

			if ((firstPart == -1) && (secPart == -1))
				return -1;

			if((firstPart == -1) && (secPart != -1))
			{
				return secPart;
			}

			if ((firstPart != -1) && (secPart == -1))
			{
				return firstPart;
			}

			if (firstPart == secPart)
				return firstPart;

			return -1;
		}
    }
}
