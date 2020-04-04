using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
	public class Q5ShortestNonSharedSubstring : Processor
	{
		public Q5ShortestNonSharedSubstring(string testDataName) : base(testDataName)
		{
		}

		public override string Process(string inStr) =>
		TestTools.Process(inStr, (Func<String, String, String>)Solve);

		private string Solve(string text1, string text2)
		{
			if (text1.Length <= 1800)
			{
				for (int i = 1; i < text1.Length; i++)
				{
					for (int j = 0; j <= text1.Length - i; j++)
					{
						if (!text2.Contains(text1.Substring(j, i)))
							return text1.Substring(j, i);
					}
				}
			}

			string result = string.Empty;
			Trie trie = new Trie(text1, text2);

			Node firstIndex = new Node();
			List<Node> sortedList = trie.Nodes.OrderBy(x => x.Height).ToList();
			foreach (var node in sortedList)
			{
				if (node.NotBelongingToBoth && node.Label != '#' && node.Label != '$')
				{
					firstIndex = node;
					break;
				}
			}

			while (firstIndex != trie.Nodes[0])
			{
				result = firstIndex.Label + result;
				firstIndex = trie.Nodes[firstIndex.Parent];
			}

			return result;
		}
	}

	public class Node
	{
		public int Num; 
		public int Parent;
		public char Label;
		public bool NotBelongingToBoth = true;
		public List<int> Children = new List<int>();
		public int Height;

		public Node(int n, int par, char label)
		{
			Num = n;
			Parent = par;
			Label = label;

		}
		public Node() { }
	}

	public class Trie
	{
		public List<Node> Nodes = new List<Node>();
		static int nodeCount;

		public Trie(string text1, string text2)
		{
			nodeCount = 1; //wherever a node was added to Nodes, this should be incremented
			Nodes.Add(new Node { Num = 0, Parent = 0, Height = 0, NotBelongingToBoth = false }); //root

			text1 += '#';
			text2 += '$';
			AddString(text1, text2);
		}

		public void AddString(string text1, string text2)
		{
			for (int i = 0; i < text1.Length - 1; i++)
			{
				int key = 0;
				for (int j = i; j < text1.Length; j++)
				{
					int? idx = Method2(key, text1[j]);

					if (!idx.HasValue) // if the character is not in the node's children
					{
						Node node = new Node(nodeCount, key, text1[j]);
						node.Height = Nodes[node.Parent].Height + 1;
						Nodes[key].Children.Add(nodeCount);
						Nodes.Add(node);
						key = nodeCount;
						nodeCount++;
					}

					else key = idx.Value;
				}
			}

			for (int i = 0; i < text2.Length - 1; i++)
			{
				int key = 0;
				for (int j = i; j < text2.Length; j++)
				{
					int? idx = Method2(key, text2[j]);

					if (!idx.HasValue) // if the character is not in the node's children
					{
						Node node = new Node(nodeCount, key, text2[j]);
						node.NotBelongingToBoth = false;
						node.Height = Nodes[node.Parent].Height + 1;

						Nodes[key].Children.Add(nodeCount);
						Nodes.Add(node);
						key = nodeCount;
						nodeCount++;
					}

					else
					{
						key = idx.Value;
						Nodes[key].NotBelongingToBoth = false;
					}
					Nodes[key].NotBelongingToBoth = false;

				}
			}

			int? Method2(int key, char ch)
			//چک اینکه استرینگ با بچه های کارنت نود اشتراک دارد یا نه
			//اگر دارد با کدام نود
			//اگر ندارد نال ریترن میشود
			{
				foreach (var child in Nodes[key].Children)
					if (Nodes[child].Label == ch)
						return child; //return the child index in Nodes list

				return null; //string has nothing in common with children
			}
		}
	}
}
