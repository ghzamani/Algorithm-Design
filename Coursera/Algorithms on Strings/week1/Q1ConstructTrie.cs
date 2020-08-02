using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    public class Q1ConstructTrie
    {
		//static void Main(string[] args)
		//{
		//	long n = long.Parse(Console.ReadLine());
		//	string[] patterns = new string[n];

		//	long i = 0;
		//	while (i < n)
		//	{
		//		patterns[i] = Console.ReadLine();
		//		i++;
		//	}
		//	string[] result = Solve(n, patterns);
		//	foreach(var r in result)
		//		Console.WriteLine(r);
		//}

		public static List<string> result;
		public static string[] Solve(long n, string[] patterns)
        {
			result = new List<string>();
			Dictionary<int, List<Tuple<char, int>>> trie = MakeTrie(n, patterns);
					
			return result.ToArray();
        }

		public static Dictionary<int, List<Tuple<char, int>>> MakeTrie (long n, string[] patterns)
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
						result.Add(key + "->" + nodeCount + ":" + patterns[i][j]);
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
