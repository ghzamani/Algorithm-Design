//using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
	public class Q1BuildingRoads
	{
		//static void Main(string[] args)
		//{
		//	long pointCount = long.Parse(Console.ReadLine());
		//	long[][] points = new long[pointCount][];

		//	int j = 0;
		//	while (j < pointCount)
		//	{
		//		var input = Console.ReadLine().Split(' ');
		//		points[j] = new long[2] { long.Parse(input[0]), long.Parse(input[1]) };
		//		j++;
		//	}

		//	Q1BuildingRoads prim = new Q1BuildingRoads();
		//	Console.WriteLine(prim.Solve(pointCount, points));
		//}

		public static List<Tuple<double, long>> Heap;
		//making min-heap that has the distances (first item) with index of the node (second item)
		public double Solve(long pointCount, long[][] points)
		{
			//dense graph of cities
			List<Tuple<double, long>>[] edges = new List<Tuple<double, long>>[pointCount];
			//second item -> neigh node
			//first item -> dist

			for (int i = 0; i < pointCount; i++)
			{
				edges[i] = new List<Tuple<double, long>>((int)pointCount - 1);
			}
						
			for (int i = 0; i < pointCount -1; i++)
			{
				for (int j = i+1; j < pointCount; j++)
				{
					double d = Dist(points[i], points[j]);
					edges[i].Add(new Tuple<double, long>(d, j));
					edges[j].Add(new Tuple<double, long>(d, i));
				}
			}

			//implementing Prim
			double[] cost = new double[pointCount];
			for (int i = 0; i < pointCount; i++)
			{
				cost[i] = int.MaxValue;
			}
			
			cost[0] = 0;

			Q1BuildingRoads prim = new Q1BuildingRoads();
			
			Heap = new List<Tuple<double, long>>();
			Heap.Add(new Tuple<double, long>(cost[0], 0));
		
			for (int i = 1; i < pointCount; i++)
			{
				Heap.Add(new Tuple<double, long>(cost[i], i));
			}

			while (Heap.Count != 0)
			{
				long v = prim.ExtractMin();
				foreach (var z in edges[v])
				{
					if (Contain(z) && cost[z.Item2] > z.Item1)
					{
						cost[z.Item2] = z.Item1;
						
						prim.ChangePriority(z.Item2, cost[z.Item2]);
					}
				}
			}
			bool Contain(Tuple<double,long> tuple)
			{
				foreach (var h in Heap)
				{
					if (h.Item2 == tuple.Item2)
					{
						return true;
					}
				}
				return false;
			}
			double result = 0;
			//foreach (var c in cost)
			//	result += c;
			for (int i = 0; i < cost.Length; i++)
			{
				result = result + cost[i];
			}
			return Math.Round(result, 6);
		}

		public static double Dist(long[] u, long[] v)
		{
			return Math.Sqrt(Math.Pow(u[0] - v[0], 2) + Math.Pow(u[1] - v[1], 2));
		}


		#region binary heap
		public long Parent(long i) { return (i - 1) / 2; }
		public long LeftChild(long i) { return 2 * i + 1; }
		public long RightChild(long i) { return 2 * i + 2; }
		public void SiftDown(long i)
		{
			long minIdx = i;
			long l = LeftChild(i);
			if ((l < Heap.Count) && (Heap[(int)l].Item1 < Heap[(int)minIdx].Item1))
				minIdx = l;

			long r = RightChild(i);
			if ((r < Heap.Count) && (Heap[(int)r].Item1 < Heap[(int)minIdx].Item1))
				minIdx = r;

			if (i != minIdx)
			{
				Swap(i, minIdx);
				SiftDown(minIdx);
			}
		}

		public void SiftUp(long i)
		{
			while ((i > 0) && (Heap[(int)Parent(i)].Item1 > Heap[(int)i].Item1))
			{
				Swap(Parent(i), i);
				i = Parent(i);
			}
		}

		public void Swap(long i, long minIdx)
		{
			var tmp = Heap[(int)i];
			Heap[(int)i] = Heap[(int)minIdx];
			Heap[(int)minIdx] = tmp;
		}

		public void ChangePriority(long item1, double p)
		{
			long i = 0;
			for (int j = 0; j < Heap.Count; j++)
			{
				if (Heap[j].Item2 == item1)
				{
					i = j;
					break;
				}
			}

			double oldp = Heap[(int)i].Item1;
			Heap[(int)i] = new Tuple<double, long>(p, item1);
			if (p < oldp)
				SiftUp(i);
			else SiftDown(i);
		}

		public long ExtractMin()
		{
			//result is the vertex
			long res = Heap[0].Item2;
			Heap[0] = Heap[Heap.Count - 1];
			Heap.RemoveAt(Heap.Count - 1);
			SiftDown(0);
			return res;
		}
		#endregion
	}
}

