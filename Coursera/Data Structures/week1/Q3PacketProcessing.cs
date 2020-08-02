using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A8
{
	public class Q3PacketProcessing 
	{
		//public static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split();
		//	long bufferSize = long.Parse(input[0]);
		//	long packets = long.Parse(input[1]);

		//	long[] arrivals = new long[packets];
		//	long[] processing = new long[packets];

		//	for (int i = 0; i < packets; i++)
		//	{
		//		input = Console.ReadLine().Split();
		//		arrivals[i] = long.Parse(input[0]);
		//		processing[i] = long.Parse(input[1]);
		//	}

		//	if (packets == 0)
		//	{
		//		Console.WriteLine();
		//		return;
		//	}

		//	long[] result = Solve(bufferSize, arrivals, processing);
		//	for (int i = 0; i < packets; i++)
		//	{
		//		Console.WriteLine(result[i]);
		//	}
		//}
		public static long[] Solve(long bufferSize,
			long[] arrivalTimes,
			long[] processingTimes)
		{
			long[] result = Enumerable.Repeat((long)-1, arrivalTimes.Length).ToArray();

			if (arrivalTimes.Length == 0)
				return result;

			long totalTimePassed = arrivalTimes[0];

			Queue<long> buffer = new Queue<long>();
			long i = 0;
			while (i < arrivalTimes.Length)
			{
				while (buffer.Count > 0)
				{
					long j = buffer.Dequeue();
					result[j] = Math.Max(totalTimePassed, arrivalTimes[j]);
					totalTimePassed = result[j] + processingTimes[j];

					while (i < arrivalTimes.Length && buffer.Count < bufferSize)
					{
						if (totalTimePassed <= arrivalTimes[i])
							buffer.Enqueue(i);
						
						i++;
					}
				}

				while (i < arrivalTimes.Length && buffer.Count < bufferSize)
				{
					if (totalTimePassed <= arrivalTimes[i])
						buffer.Enqueue(i);
					
					i++;
				}
			}
			while (buffer.Count > 0)
			{
				long j = buffer.Dequeue();
				result[j] = Math.Max(totalTimePassed, arrivalTimes[j]);
				totalTimePassed = result[j] + processingTimes[j];
			}
			return result;
		}
	}
}