using System;
using System.Collections.Generic;
using System.Linq;

namespace A9
{
    public class Q1ConvertIntoHeap 
    {
		//public static void Main(string[] args)
		//{
		//	Console.ReadLine();
		//	long[] nums = Console.ReadLine().Split().Select(x => long.Parse(x)).ToArray();

		//	Tuple<long, long>[] answers = Solve(nums);
		//	Console.WriteLine(answers.Length);
		//	for (int i = 0; i < answers.Length; i++)
		//	{
		//		Console.WriteLine(answers[i].Item1 + " " + answers[i].Item2);
		//	}
		//}

		static List<Tuple<long, long>> result;

		public static Tuple<long, long>[] Solve(long[] array)
        {
			result = new List<Tuple<long, long>>();
			for (int i = array.Length / 2 - 1; i >= 0; i--)
				SiftDown(array, i);
			
			return result.ToArray();
        }

		public static void SiftDown(long[] array, long i)
		{
			long minIdx;
			long leftIdx = 2 * i + 1;
			long rightIdx = leftIdx + 1;

			if (leftIdx < array.Length)
			{
				if(rightIdx>= array.Length)
					minIdx = IndexOfMinElement(array, i, leftIdx, null);

				else minIdx = IndexOfMinElement(array, i, leftIdx, rightIdx);

				if (minIdx != i)
				{
					result.Add(new Tuple<long, long>(i, minIdx));
					Swap(array, i, minIdx);
					SiftDown(array, minIdx);
				}
			}			
		}

		public static void Swap(long[] array, long i, long minIdx)
		{
			long tmp = array[i];
			array[i] = array[minIdx];
			array[minIdx] = tmp;
		}

		public static long IndexOfMinElement(long[] nums, long i, long l, long? r)
		{
			long result = i;

			if (nums[l] < nums[result])
				result = l;

			if(r.HasValue)
				if (nums[r.Value] < nums[result])
					result = r.Value;

			return result;
		}
    }
}
