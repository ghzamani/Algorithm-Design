using System;
using System.Collections.Generic;
using System.Linq;

namespace A9
{
    public class Q4ParallelProcessing
    {
		//public static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split();
		//	long n = long.Parse(input[0]);
		//	//long m = long.Parse(input[1]);

		//	long[] jobDuration = Console.ReadLine().Split().Select(x => long.Parse(x)).ToArray();
		//	Q4ParallelProcessing q4 = new Q4ParallelProcessing();
		//	Tuple<long, long>[] result = q4.Solve(n, jobDuration);
		//	for (int i = 0; i < result.Length; i++)
		//	{
		//		Console.WriteLine(result[i].Item1 + " " + result[i].Item2);
		//	}
		//}
        public Tuple<long, long>[] Solve(long threadCount, long[] jobDuration)
        {
			Tuple<long, long>[] threadsAndTimes = new Tuple<long, long>[jobDuration.Length];

			List<Tuple<long, long>> minHeapByTime = new List<Tuple<long, long>>((int) threadCount);
			for(int i = 0; i < threadCount; i++)
			{
				minHeapByTime.Add(new Tuple<long, long>(i, 0));
			}

			for(long i = 0; i < jobDuration.Length; i++)
			{
				threadsAndTimes[i] = ExtractMin(minHeapByTime);

				Tuple<long, long> tuple = new Tuple<long, long>(threadsAndTimes[i].Item1,
					threadsAndTimes[i].Item2 + jobDuration[i]);

				Insert(minHeapByTime, tuple);
			}
			return threadsAndTimes;
        }

		public void Insert(List<Tuple<long, long>> minHeapByTime, Tuple<long, long> tuple)
		{
			minHeapByTime.Add(tuple);
			SiftUp(minHeapByTime, minHeapByTime.Count - 1);
		}

		public void SiftUp(List<Tuple<long, long>> minHeapByTime, long i)
		{
			if (i == 0)
				return;

			long parent = (i - 1) / 2;
			
			if(minHeapByTime[(int)i].Item2 < minHeapByTime[(int)parent].Item2)
			{
				Swap(minHeapByTime, i, parent);
				parent = i;
				SiftUp(minHeapByTime, parent);
			}
			else
			{
				if((minHeapByTime[(int)i].Item2 == minHeapByTime[(int)parent].Item2) &&
					(minHeapByTime[(int)i].Item1 < minHeapByTime[(int)parent].Item1))
				{
					parent = i;
					SiftUp(minHeapByTime, parent);
					Swap(minHeapByTime, i, parent);
				}
			}
		}

		public Tuple<long, long> ExtractMin(List<Tuple<long, long>> minHeapByTime)
		{
			//replace the root with last element
			//(minHeapByTime[0], minHeapByTime[minHeapByTime.Count - 1]) =
			//	(minHeapByTime[minHeapByTime.Count - 1], minHeapByTime[0]);
			Tuple<long, long> tmp = minHeapByTime[0];
			minHeapByTime[0] = minHeapByTime[minHeapByTime.Count - 1];
			minHeapByTime[minHeapByTime.Count - 1] = tmp;

			//saving last element in t and then removing it
			Tuple<long, long> t = minHeapByTime[minHeapByTime.Count - 1];
			minHeapByTime.RemoveAt(minHeapByTime.Count - 1);

			SiftDown(minHeapByTime, 0);
			return t;
		}


		public void SiftDown(List<Tuple<long, long>> minHeap, long i)
		{
			long minIdx;
			long leftIdx = 2 * i + 1;
			long rightIdx = leftIdx + 1;

			if (leftIdx < minHeap.Count)
			{
				if (rightIdx >= minHeap.Count)
					minIdx = IndexOfMinElement(minHeap, i, leftIdx, null);

				else minIdx = IndexOfMinElement(minHeap, i, leftIdx, rightIdx);

				if (minIdx != i)
				{
					Swap(minHeap, i, minIdx);
					SiftDown(minHeap, minIdx);
				}
			}
		}

		public static void Swap(List<Tuple<long, long>> heap, long i, long idx)
		{
			var tmp = heap[(int)i];
			heap[(int)i] = heap[(int)idx];
			heap[(int)idx] = tmp;
		}

		public static long IndexOfMinElement(List<Tuple<long, long>> nums, long i, long l, long? r)
		{
			long result = i;

			if (nums[(int)l].Item2 < nums[(int)result].Item2)
				result = l;

			else
			{
				if((nums[(int)l].Item2 == nums[(int)result].Item2) &&
					(nums[(int)l].Item1 < nums[(int)result].Item1))				
					result = l;				
			}

			if (r.HasValue)
			{
				if (nums[(int)r.Value].Item2 < nums[(int)result].Item2)
					result = r.Value;

				else
				{
					if ((nums[(int)r].Item2 == nums[(int)result].Item2) &&
						(nums[(int)r].Item1 < nums[(int)result].Item1))
						result = r.Value;
				}
			}

			return result;
		}
	}
}
