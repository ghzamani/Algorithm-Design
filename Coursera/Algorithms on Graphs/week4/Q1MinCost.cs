using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Q1MinCost
    {
		//static void Main(string[] args)
		//{
		//	var input = Console.ReadLine().Split(' ');
		//	long nodeCount = long.Parse(input[0]);
		//	long edgeCount = long.Parse(input[1]);

		//	List<Tuple<long, long>>[] graphEdges = new List<Tuple<long, long>>[nodeCount];
		//	//adjacency list of graph
		//	//first item -> v
		//	//second item -> weight	
		//	for (int i = 0; i < nodeCount; i++)
		//	{
		//		graphEdges[i] = new List<Tuple<long, long>>();
		//	}

		//	long j = 0;
		//	while (j < edgeCount)
		//	{
		//		input = Console.ReadLine().Split(' ');
		//		long u = long.Parse(input[0]) - 1;
		//		long v = long.Parse(input[1]) - 1;
		//		long w = long.Parse(input[2]);
		//		graphEdges[u].Add(new Tuple<long, long>(v, w));
		//		j++;
		//	}

		//	input = Console.ReadLine().Split(' ');
		//	long start = long.Parse(input[0]) - 1;
		//	long end = long.Parse(input[1]) - 1;

		//	Q1MinCost dijk = new Q1MinCost();
		//	Console.WriteLine(dijk.Solve(nodeCount, graphEdges, start, end));
		//}

		public List<Tuple<long, long>> Heap;
		public long Solve(long nodeCount, List<Tuple<long, long>>[] graphEdges, long startNode, long endNode)
        {	
			long[] dist = new long[nodeCount];
			for (int i = 0; i < nodeCount; i++)
				dist[i] = int.MaxValue;
			dist[startNode] = 0;

			//making min-heap that has the distances (first item) with index of the node (second item)
			Heap = new List<Tuple<long, long>>();
			Heap.Add(new Tuple<long, long>(0, startNode));
			for (int i = 0; i < nodeCount; i++)
			{
				if(i != startNode)
					Heap.Add(new Tuple<long, long>(int.MaxValue, i));
			}

			while(Heap.Count != 0)
			{
				long u = ExtractMin();
				foreach(var x in graphEdges[u])
				{
					if(dist[x.Item1] > dist[u] + x.Item2)
					{
						dist[x.Item1] = dist[u] + x.Item2;
						ChangePriority(x.Item1, dist[x.Item1]);
					}
				}
			}
			return dist[endNode] != int.MaxValue ? dist[endNode] : -1;
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

			if(i!= minIdx)
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

		public void ChangePriority(long item1, long p)
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

			long oldp = Heap[(int)i].Item1;
			Heap[(int)i] = new Tuple<long, long>(p, item1);
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
