using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A8
{
    public class Q2TreeHeight
    {
		//public static void Main(string[] args)
		//{
		//	long nodeCount = long.Parse(Console.ReadLine());
		//	long[] tree = Console.ReadLine().Split().Select(x => long.Parse(x)).ToArray();

		//	Console.WriteLine(Solve(nodeCount,tree));
		//}
        public static long Solve(long nodeCount, long[] tree)
        {
			Node[] nodes = new Node[nodeCount];
			for (int i = 0; i < nodeCount; i++)
				nodes[i] = new Node();			

			for(int i = 0; i < nodeCount; i++)
			{
				nodes[i].Key = tree[i];
				if(tree[i] != -1)
					nodes[tree[i]].Children.Add(i);
			}

			return Height(nodes);
        }

		public static long[] heights;		
		public static long Height(Node[] nodes)
		{
			heights = new long[nodes.Length];
			Queue<int> parent = new Queue<int>();
			//finding the root
			for(int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i].Key == -1)
				{
					heights[i] = 1;
					parent.Enqueue(i);
					break;
				}
			}

			while (heights.Contains(0))
			{
				foreach(var p in new Queue<int>(parent))
				{
					NextLevel(nodes[p], heights[p]);
					foreach(var ch in nodes[p].Children)
					{
						if (nodes[ch].Children.Count != 0)
							parent.Enqueue(ch);
					}
					parent.Dequeue();
				}
			}
			return heights.Max();
		}

		public static void NextLevel(Node n, long h)
		{
			foreach(var child in n.Children)
				heights[child] = 1 + h;			
		}
	}
	public class Node
	{
		public long Key;
		public List<int> Children = new List<int>();
	}	
}
