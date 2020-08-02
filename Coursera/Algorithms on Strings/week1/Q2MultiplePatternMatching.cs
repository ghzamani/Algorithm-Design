using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    public class Q2MultiplePatternMatching
    {
		//static void Main(string[] args)
		//{
		//	string text = Console.ReadLine();

		//	long n = long.Parse(Console.ReadLine());
		//	string[] patterns = new string[n];

		//	long i = 0;
		//	while (i < n)
		//	{
		//		patterns[i] = Console.ReadLine();
		//		i++;
		//	}
		//	long[] result = Solve(text, n, patterns);
		//	if (result.Length != 0)
		//	{
		//		foreach (var r in result)
		//			Console.Write(r + " ");
		//		return;
		//	}
		//	Console.WriteLine();
		//}
		public static long[] Solve(string text, long n, string[] patterns)
        {
			List<long> result = new List<long>();

			for (int i = 0; i < n; i++)
			{
				patterns[i] += '$';
			}

			Dictionary<int, List<Tuple<char, int>>> trie = MakeTrie(n, patterns);

			for (int i = 0; i < text.Length; i++)
			{
				int key = 0;
				for (int j = i; j < text.Length; j++)
				{
					int? idx = Contain(trie[key], text[j]);
					if (!idx.HasValue)
						break;
					else
					{
						key = trie[key][idx.Value].Item2;
						
						if(Contain(trie[key],'$').HasValue) //pattern was found (reached a leaf)
						{ 
							result.Add(i);
							break;
						}
					}
				}
			}

			return result.Count != 0 ? result.ToArray() : new long[0];
        }

		public static Dictionary<int, List<Tuple<char, int>>> MakeTrie(long n, string[] patterns)
		{
			Dictionary<int, List<Tuple<char, int>>> trie = new Dictionary<int, List<Tuple<char, int>>>();

			int nodeCount = 1;
			for (int i = 0; i < n; i++)
			{
				int key = 0;
				for (int j = 0; j < patterns[i].Length; j++)
				{
					if (!trie.ContainsKey(key))
						trie.Add(key, new List<Tuple<char, int>>());

					int? idx = Contain(trie[key], patterns[i][j]);
					if (!idx.HasValue) // if the character is not in the node's children
					{
						trie[key].Add(new Tuple<char, int>(patterns[i][j], nodeCount));
						trie.Add(nodeCount, new List<Tuple<char, int>>());
						key = nodeCount;
						nodeCount++;
					}

					else key = trie[key][idx.Value].Item2;
				}
			}
			return trie;
		}

		public static int? Contain(List<Tuple<char, int>> list, char x)
		{
			for (int i = 0; i < list.Count; i++)
				if (list[i].Item1 == x)
					return i; //returns the index in children list

			return null;
		}
	}
}
