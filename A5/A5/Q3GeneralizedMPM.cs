using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q3GeneralizedMPM : Processor
    {
        public Q3GeneralizedMPM(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
			List<long> result = new List<long>();

			for (int i = 0; i < n; i++)
			{
				patterns[i] += '$';
			}

			Dictionary<int, List<Tuple<char, int>>> trie = Q2MultiplePatternMatching.MakeTrie(n, patterns);

			for (int i = 0; i < text.Length; i++)
			{
				int key = 0;
				for (int j = i; j < text.Length; j++)
				{
					int? idx = Q1ConstructTrie.Contain(trie[key], text[j]);
					if (!idx.HasValue)
						break;
					else
					{
						key = trie[key][idx.Value].Item2;

						if (Q1ConstructTrie.Contain(trie[key], '$').HasValue) //pattern was found (reached a leaf)
						{
							if(!result.Contains(i))
								result.Add(i);
							break;
						}
					}
				}
			}

			return result.Count != 0 ? result.ToArray() : new long[] { -1 };
		}		
	}
}
