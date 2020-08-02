using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4
{
    public class Q2MaximizingLoot
    {
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long n = long.Parse(input[0]);
		//	long capacity = long.Parse(input[1]);

		//	long[] weights = new long[n];
		//	long[] vals = new long[n];

		//	int i = 0;
		//	while (i < n)
		//	{
		//		input = Console.ReadLine().Split(' ');
		//		vals[i] = long.Parse(input[0]);
		//		weights[i] = long.Parse(input[1]);
		//		i++;
		//	}
		//	Console.WriteLine(Math.Round(Solve(capacity,weights,vals),3));
		//}

		public static double Solve(long capacity, long[] weights, long[] values)
        {
			double result = 0;
			List<Tuple<long, long, double>> knapsackItems = new List<Tuple<long, long, double>>();
			
			for (int i = 0; i < weights.Length; i++)
				knapsackItems.Add(Tuple.Create(weights[i], values[i], (double)values[i] / weights[i]));

			knapsackItems = knapsackItems.OrderByDescending(i => i.Item3).ToList();

			while(capacity > 0)
			{
				if(knapsackItems[0].Item1 > capacity)
				{
					result += ((double)knapsackItems[0].Item2 / knapsackItems[0].Item1) * capacity;
					return result;
				}

				capacity -= knapsackItems[0].Item1;
				knapsackItems[0] = new Tuple<long, long, double>
					(0, knapsackItems[0].Item2, knapsackItems[0].Item3);
				result += knapsackItems[0].Item2;
				knapsackItems.RemoveAt(0);
			}
			return result;
        }
		
    }
}
