using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) 
		{}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

		public List<Tuple<long, long>> Heap;

		public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
			//adjacency list of graph
			List<Tuple<long, long>>[] graphEdges = AdjListGraph(nodeCount, edges);
			//first item -> v
			//second item -> weight			

			startNode -= 1;
			endNode -= 1;

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

			if (dist[endNode] == int.MaxValue)
				return -1;
			return dist[endNode];
        }

		#region binary heap
		public long Parent(long i) => (i - 1) / 2;
		public long LeftChild(long i) => 2 * i + 1;
		public long RightChild(long i) => 2 * i + 2;
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

		public static List<Tuple<long, long>>[] AdjListGraph (long nodeCount, long[][] edges)
		{
			List<Tuple<long, long>>[] graphEdges = new List<Tuple<long, long>>[nodeCount];

			for (int i = 0; i < nodeCount; i++)
				graphEdges[i] = new List<Tuple<long, long>>();

			for (int i = 0; i < edges.Length; i++)
			{
				long u = edges[i][0];
				long v = edges[i][1];
				long w = edges[i][2];
				graphEdges[u - 1].Add(new Tuple<long, long>(v - 1, w));
			}
			return graphEdges;
		}
	}
}
