using System;
using System.Collections.Generic;
using System.Text;

namespace A11
{
	public class Q4SetWithRangeSums
	{
		//public static void Main(string[] args)
		//{
		//	int n = int.Parse(Console.ReadLine());
		//	string[] lines = new string[n];
		//	for (int i = 0; i < n; i++)
		//	{
		//		lines[i] = Console.ReadLine();
		//	}

		//	Q4SetWithRangeSums q4 = new Q4SetWithRangeSums(" ");
		//	string[] result = q4.Solve(lines);
		//	for (int i = 0; i < result.Length; i++)
		//	{
		//		Console.WriteLine(result[i]);
		//	}
		//}

		public Q4SetWithRangeSums(string testDataName)
		{
			CommandDict =
						new Dictionary<char, Func<string, string>>()
						{
							['+'] = Add,
							['-'] = Del,
							['?'] = Find,
							['s'] = Sum
						};
		}

		public readonly Dictionary<char, Func<string, string>> CommandDict;

		public const long M = 1000000001;

		public long X = 0;
		public SplayTree splayTree;
		public string[] Solve(string[] lines)
		{
			X = 0;
			splayTree = new SplayTree();

			List<string> result = new List<string>();
			foreach (var line in lines)
			{
				char cmd = line[0];
				string args = line.Substring(1).Trim();
				var output = CommandDict[cmd](args);
				if (null != output)
					result.Add(output);
			}
			return result.ToArray();
		}

		private long Convert(long i)
			=> i = (i + X) % M;

		private string Add(string arg)
		{
			long i = Convert(long.Parse(arg));
			splayTree.Insert(i);

			return null;
		}

		private string Del(string arg)
		{
			long i = Convert(long.Parse(arg));
			splayTree.Delete(i);

			return null;
		}

		private string Find(string arg)
		{
			long i = Convert(int.Parse(arg));
			if(splayTree.Root != null)
			{
				Pair n = splayTree.Find(splayTree.Root, i);
				splayTree.Root = splayTree.Splay(n.Item2); 
				if (splayTree.Root.Key == i)
					return "Found";		
			}
			return "Not found";
		}

		private string Sum(string arg)
		{
			var toks = arg.Split();
			long l = Convert(long.Parse(toks[0]));
			long r = Convert(long.Parse(toks[1]));
			
			long sum = 0;
			sum = splayTree.SumRange(l, r);
			X = sum;
			return sum.ToString();
		}
	}
	public class Node
	{
		public long Key;
		public long Sum;

		public Node Parent;
		public Node LeftChild;
		public Node RightChild;

		public Node(long key, long sum, Node left = null, Node right = null, Node parent = null)
		{
			Key = key;
			Sum = sum;
			LeftChild = left;
			RightChild = right;
			Parent = parent;
		}

		//default cons
		public Node() { }
	}

	//easier than tuple, cause tuple is immutable
	public class Pair
	{
		public Node Item1;
		public Node Item2;
		public Pair()
		{
		}
		public Pair(Node i1, Node i2)
		{
			this.Item1 = i1;
			this.Item2 = i2;
		}
	}
	public class SplayTree
	{
		public Node Root = null;

		public void Update(Node N)
		{
			if (N != null)
			{
				N.Sum = N.Key;

				if (N.LeftChild != null)
				{
					N.Sum += N.LeftChild.Sum;
					N.LeftChild.Parent = N;
				}

				if (N.RightChild != null)
				{
					N.Sum += N.RightChild.Sum;
					N.RightChild.Parent = N;
				}
			}
		}

		public void InnerRotate(Node N)
		{
			Node parent = N.Parent;

			if (parent != null)
			{
				Node grandparent = N.Parent.Parent;
				Node tmp;

				if (parent.LeftChild == N)
				{
					tmp = N.RightChild;
					N.RightChild = parent;
					parent.LeftChild = tmp;
				}
				else
				{
					tmp = N.LeftChild;
					N.LeftChild = parent;
					parent.RightChild = tmp;
				}

				Update(parent);
				Update(N);
				N.Parent = grandparent;
				if (grandparent != null)
				{
					if (grandparent.LeftChild == parent)
						grandparent.LeftChild = N;
					
					else grandparent.RightChild = N;					
				}
			}
		}

		public void ZigZig(Node N)
		{
			InnerRotate(N.Parent);
			InnerRotate(N);
		}
		public void ZigZag(Node N)
		{
			InnerRotate(N);
			InnerRotate(N);
		}
		public void Rotation(Node N)
		{
			if ((N.Parent.LeftChild == N && N.Parent.Parent.LeftChild == N.Parent) ||
				(N.Parent.RightChild == N && N.Parent.Parent.RightChild == N.Parent))
				ZigZig(N);			
			
			else ZigZag(N);			
		}

		public Node Splay(Node N)
		{
			if (N != null)
			{
				while (N.Parent != null)
				{
					if (N.Parent.Parent == null)
					{
						InnerRotate(N);
						break;
					}
					Rotation(N);
				}
				return N; //root
			}

			return null;
		}
				

		// if not found --> result = the node with the smallest bigger key
		//if biggest key --> result = null
		public Pair Find(Node root, long key)
		{
			Node N = root;
			Node last = root;
			Node next = null;
			while (N != null)
			{
				if (N.Key >= key && (next == null || N.Key < next.Key))
					next = N;
				
				last = N;
				if (N.Key == key)
					break;
				
				if (N.Key < key)
					N = N.RightChild;
				
				else N = N.LeftChild;				
			}
			// calls splay for the deepest visited node.
			root = Splay(last);
			return new Pair(next, root);
		}

		public Pair Split(Node root, long key)
		{
			Pair result = new Pair();
			Pair nodes = Find(root, key);

			root = nodes.Item2;
			result.Item2 = nodes.Item1;

			if (result.Item2 == null)
			{
				result.Item1 = root;
				return result;
			}

			result.Item2 = Splay(result.Item2);
			result.Item1 = result.Item2.LeftChild;
			result.Item2.LeftChild = null;

			if (result.Item1 != null)
				result.Item1.Parent = null;
			
			Update(result.Item1);
			Update(result.Item2);
			return result;
		}

		public Node Merge(Node left, Node right)
		{
			if (left == null) return right;
			if (right == null) return left;

			while (right.LeftChild != null)
				right = right.LeftChild;
			
			right = Splay(right);
			right.LeftChild = left;
			Update(right);
			return right;
		}

		public void Insert(long key)
		{
			Node left, right;
			Node newNode = null;
			Pair nodes = Split(Root, key);

			left = nodes.Item1;
			right = nodes.Item2;

			if (right == null || right.Key != key)
				newNode = new Node(key, key);
			
			Root = Merge(Merge(left, newNode), right);
		}

		public void Delete(long key)
		{
			Node l, m, r;
			Pair nodes = Split(Root, key);
			l = nodes.Item1;
			m = nodes.Item2;

			Pair tree = Split(m, key + 1);
			m = tree.Item1;
			r = tree.Item2;
			m = null;
			Root = Merge(l, r);
			Splay(Root);
		}

		public long SumRange(long l, long r)
		{
			if (l > r) return 0;

			long sum = 0;
			Node left, middle, right;

			Pair lAndM = Split(Root, l);
			left = lAndM.Item1;
			middle = lAndM.Item2;

			Pair mAndR = Split(middle, r + 1);
			middle = mAndR.Item1;
			right = mAndR.Item2;

			if (middle != null) 
				sum += middle.Sum;

			Node tmp = Merge(left, middle);
			Root = Merge(tmp, right);
			return sum;
		}
	}
}
