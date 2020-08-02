using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace A7
{
	public class Q2PartitioningSouvenirs
	{


		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] souvenirs = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
		//	Console.WriteLine(Solve(n,souvenirs));
		//}
		public static long Solve(long souvenirsCount, long[] souvenirs)
		{
			if (souvenirsCount < 3)
				return 0;

			long sum = 0;
			foreach (var souvenir in souvenirs)
				sum += souvenir;

			if (sum % 3 != 0)
				return 0;

			sum /= 3;
			if (IsFound(souvenirs, souvenirsCount - 1, sum, sum, sum, new Dictionary<string, bool>()))
				return 1;

			return 0;
		}

		public static bool IsFound(long[] nums, long n, long s1, long s2, long s3, Dictionary<string, bool> dic)
		{
			if ((s1 == 0) && (s2 == 0) && (s3 == 0))
				return true;
			if (n < 0)
				return false;

			string dicKey = s1.ToString() + s2.ToString() + s3.ToString();
			if (!dic.ContainsKey(dicKey))
			{
				bool b1 = false;
				if (s1 - nums[n] >= 0)
					b1 = IsFound(nums, n - 1, s1 - nums[n], s2, s3, dic);

				bool b2 = false;
				if ((b1 == false) && (s2 - nums[n] >= 0))
					b2 = IsFound(nums, n - 1, s1, s2 - nums[n], s3, dic);

				bool b3 = false;
				if ((b1 == false) && (b2 == false) && (s3 - nums[n] >= 0))
					b3 = IsFound(nums, n - 1, s1, s2, s3 - nums[n], dic);

				dic.Add(dicKey, b1 || b2 || b3);
			}
			//return dic.GetValueOrDefault(dicKey);
			bool b = false;
			return dic.TryGetValue(dicKey, out b);
		}
	}
}
