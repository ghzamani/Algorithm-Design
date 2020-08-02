using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace A11
{
    public class Q3IsItBSTHard
    {
		//public static void Main(string[] args)
		//{
		//	int nodeCount = int.Parse(Console.ReadLine());
		//	long[][] nodes = new long[nodeCount][];
		//	for (int i = 0; i < nodeCount; i++)
		//	{
		//		nodes[i] = Console.ReadLine().Split().Select(x => long.Parse(x)).ToArray();
		//	}
		//	if (Solve(nodes))
		//	{
		//		Console.WriteLine("CORRECT");
		//		return;
		//	}
		//	Console.WriteLine("INCORRECT");
		//}
        public static bool Solve(long[][] nodes)
        {	
			Tuple<long, long>[] inOrder = InOrder(nodes);
			
			for(int i = 0; i < nodes.Length - 1; i++)
			{
				if (inOrder[i].Item1 > inOrder[i + 1].Item1)
					return false;
			}

			long[] parent = new long[nodes.Length];
			parent[0] = 0;
			for (int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i][1] != -1)
					parent[nodes[i][1]] = i;

				if (nodes[i][2] != -1)
					parent[nodes[i][2]] = i;
			}
			
			for (int i = 0; i < nodes.Length - 1; i++)
			{
				if(inOrder[i].Item1 == inOrder[i + 1].Item1)
				{
					long idx = inOrder[i + 1].Item2;
					long leftChildIdx = nodes[idx][1];
					if (leftChildIdx == inOrder[i].Item2)
						return false;

					long a = FindFisrtChild(parent, inOrder[i].Item2, inOrder[i + 1].Item2);
					if (nodes[inOrder[i + 1].Item2][1] == a)
						return false;
				}
			}
			return true;
        }

		public static long FindFisrtChild(long[] parent, long i, long j)
		{
			long copiedi = i;
			long p = parent[i];
			while (p != j)
			{
				i = p;
				p = parent[p];
				if (p == 0)
					break;
			}
			if (p != 0)
				return i;

			return FindFisrtChild(parent, j, copiedi);
		}

		public static Tuple<long,long>[] InOrder(long[][] nodes)
		{
			Tuple<long, long>[] result = new Tuple<long, long>[nodes.Length];
			Stack<Tuple<long, long>> stack = new Stack<Tuple<long, long>>();

			long current = 0;
			int i = 0;
			while (i != result.Length)
			{
				//while the node has left child loop
				while (current != -1)
				{
					stack.Push(new Tuple<long, long>(nodes[current][0], current));
					current = nodes[current][1]; //set current to index of left child
				}
				if ((current == -1) && (stack.Count != 0))
				{
					current = nodes[stack.Peek().Item2][2]; // set current to index of right child
					result[i] = stack.Pop();
					i++;
					continue;
				}
			}
			return result;
		}
	}
}
