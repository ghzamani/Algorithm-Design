using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11
{
	public class Q2FunParty : Processor
	{
		public Q2FunParty(string testDataName) : base(testDataName) { }

		public override string Process(string inStr) =>
		TestTools.Process(inStr, (Func<long, long[], long[][], long>)Solve);

		long[] D;
		public virtual long Solve(long n, long[] funFactors, long[][] hierarchy)
		{
			D = new long[n];
			for (int i = 0; i < n; i++)
				D[i] = long.MaxValue;			

			Node[] nodes = new Node[n];
			for (int i = 0; i < n; i++)
				nodes[i] = new Node(funFactors[i]);			

			for (int i = 0; i < hierarchy.Length; i++)
			{
				long u = hierarchy[i][0] - 1;
				long v = hierarchy[i][1] - 1;

				nodes[u].Children.Add(v);
				nodes[v].Children.Add(u);
			}
			return FunParty(nodes, 0, -1);
		}

		public virtual long FunParty(Node[] nodes, long v, long parent)
		{
			if (D[v] == long.MaxValue)
			{
				//every node has at least one children (in this question, parent and children are not definded so children might be parent)
				if (nodes[v].Children.Count == 1)
				{
					D[v] = nodes[v].FunFactor;
				}
				else
				{
					long children_Fun = 0;
					long nodeAndGrandchilren_Fun = nodes[v].FunFactor;

					foreach (var u in nodes[v].Children)
					{
						if (u != parent)
						{
							children_Fun += FunParty(nodes, u, v);

							foreach (var w in nodes[u].Children)
								if (w != v)
									nodeAndGrandchilren_Fun += FunParty(nodes, w, u);
						}
					}

					D[v] = Math.Max(children_Fun, nodeAndGrandchilren_Fun);
				}
			}
			return D[v];
		}
	}

	public class Node
	{
		public long FunFactor;
		public List<long> Children;

		public Node(long fun)
		{
			FunFactor = fun;
			Children = new List<long>();
		}
	}
}
