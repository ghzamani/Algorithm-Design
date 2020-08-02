using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace A4
{
    public class Q6MaximizeSalary
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	long[] nums = new long[n];

		//	var input = Console.ReadLine().Split(' ');
		//	for (int i = 0; i < n; i++)
		//	{
		//		nums[i] = long.Parse(input[i]);
		//	}
		//	Console.WriteLine(Solve(n,nums));
		//}


		public static string Solve(long n, long[] numbers)
        {
			string result = string.Empty;
			
			int idx = 0;
			List<long> nums = numbers.ToList();
			
			while(nums.Count != 0)
			{
				long biggestNum = 0;
				for (int i = 0; i < nums.Count ; i++)
				{
					if (MSD(nums[i]) > MSD(biggestNum))
					{
						biggestNum = nums[i];
						idx = i;
					}

					else
					{
						if (MSD(nums[i]) == MSD(biggestNum))
						{
							if (long.Parse(nums[i].ToString() + biggestNum.ToString())
								> long.Parse(biggestNum.ToString() + nums[i].ToString()))

							{
								biggestNum = nums[i];
								idx = i;
							}
						}
					}					
				}
				result += biggestNum;
				nums.RemoveAt(idx);
			}
			return result;
        }

		//returns the most significant digit of number
		public static int MSD (long n)
		{
			return int.Parse(n.ToString().First().ToString());
		}
    }
}

