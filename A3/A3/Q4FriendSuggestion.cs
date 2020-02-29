using Lucene.Net.Util;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
	public class Q4FriendSuggestion : Processor
	{
		public Q4FriendSuggestion(string testDataName) : base(testDataName)
		{
			//ExcludeTestCaseRangeInclusive(1, 3);
			//this.ExcludeTestCaseRangeInclusive(5, 50);
		}

		public override string Process(string inStr) =>
			TestTools.Process(inStr, (Func<long, long, long[][], long, long[][], long[]>)Solve);

		public long[] Solve(long NodeCount, long EdgeCount,
							  long[][] edges, long QueriesCount,
							  long[][] Queries)
		{
			long[] result = new long[QueriesCount];

			List<Tuple<long, long>>[] graphEdges = new List<Tuple<long, long>>[NodeCount];
			List<Tuple<long, long>>[] reverseGraphEdges = new List<Tuple<long, long>>[NodeCount];

			for (int i = 0; i < NodeCount; i++)
			{
				graphEdges[i] = new List<Tuple<long, long>>();
				reverseGraphEdges[i] = new List<Tuple<long, long>>();
			}

			for (int i = 0; i < edges.Length; i++)
			{
				long u = edges[i][0] - 1;
				long v = edges[i][1] - 1;
				long w = edges[i][2];

				graphEdges[u].Add(new Tuple<long, long>(v, w));
				reverseGraphEdges[v].Add(new Tuple<long, long>(u, w));
			}

			long[] dist, reverseDist, prev, reversePrev;
			List<long> proc, reverseProc;

			int idx = 0;
			foreach (var q in Queries)
			{
				dist = new long[NodeCount];
				reverseDist = new long[NodeCount];
				prev = new long[NodeCount];
				reversePrev = new long[NodeCount];

				for (int i = 0; i < NodeCount; i++)
					dist[i] = reverseDist[i] = int.MaxValue;

				long s = q[0] - 1;
				long t = q[1] - 1;
				if (s == t)
				{
					result[idx] = 0;
					idx += 1;
					continue;
				}

				dist[s] = 0;
				reverseDist[t] = 0;

				proc = new List<long>();
				reverseProc = new List<long>();

				//BinaryHeap distance = new BinaryHeap(s, NodeCount);
				//BinaryHeap reverseDistance = new BinaryHeap(t, NodeCount);
				SimplePriorityQueue<long, long> distanceHeap = new SimplePriorityQueue<long, long>();
				SimplePriorityQueue<long, long> reverseDistanceHeap = new SimplePriorityQueue<long, long>();
				for (int i = 0; i < NodeCount; i++)
				{
					distanceHeap.Enqueue(i, int.MaxValue);
					reverseDistanceHeap.Enqueue(i, int.MaxValue);
				}
				distanceHeap.UpdatePriority(s, 0);
				reverseDistanceHeap.UpdatePriority(t, 0);

				while (true)
				{
					//long v = distance.ExtractMin();
					long v = distanceHeap.Dequeue();
					Process(v, graphEdges, dist, prev, proc, distanceHeap);
					if (reverseProc.Contains(v))
					{
						result[idx] = ShortestPath(s, dist, prev, proc, t, reverseDist, reversePrev, reverseProc);
						break;
					}

					//long reverseV = reverseDistanceHeap.ExtractMin();
					long reverseV = reverseDistanceHeap.Dequeue();
					Process(reverseV, reverseGraphEdges, reverseDist, reversePrev, reverseProc, reverseDistanceHeap);
					if (proc.Contains(reverseV))
					{
						//result[idx] = ShortestPath(t, reverseDist, reversePrev, reverseProc, s, dist, prev, proc);
						result[idx] = ShortestPath(s, dist, prev, proc, t, reverseDist, reversePrev, reverseProc);
						break;
					}
				}
				idx += 1;
			}

			return result;
		}

		public void Process(long u, List<Tuple<long, long>>[] graphEdges, long[] dist, long[] prev, List<long> proc, SimplePriorityQueue<long, long> heap)
		{
			foreach (var e in graphEdges[u])
				Relax(u, e, dist, prev, heap);
			proc.Add(u);
		}

		public void Relax(long u, Tuple<long, long> e, long[] dist, long[] prev, SimplePriorityQueue<long, long> heap)
		{
			long v = e.Item1;
			long w = e.Item2;

			if (dist[v] > dist[u] + w)
			{
				dist[v] = dist[u] + w;
				prev[v] = u;
				//heap.ChangePriority(v, dist[v]);
				heap.UpdatePriority(v, dist[v]);
			}
		}

		public long ShortestPath(long s, long[] dist, long[] prev, List<long> proc,
								long t, long[] distR, long[] prevR, List<long> procR)
		{
			long distance = int.MaxValue;
			long uBest = 0;

			foreach (var u in proc.Concat(procR))
			{
				if (dist[u] + distR[u] < distance)
				{
					uBest = u;
					distance = dist[u] + distR[u];
				}
			}
			return distance != int.MaxValue ? distance : -1;
		}
	}

	/*public class BinaryHeap
	{
		public List<Tuple<long, long>> Heap;

		public BinaryHeap(long root, long nodeCount)
		{
			//making min-heap that has the distances (first item) with index of the node (second item)
			Heap = new List<Tuple<long, long>>();
			Heap.Add(new Tuple<long, long>(0, root));
			for (int i = 0; i < nodeCount; i++)
			{
				if (i != root)
					Heap.Add(new Tuple<long, long>(int.MaxValue, i));
			}
		}

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
	}*/
}
