using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        /// <summary>
        /// Implement BetterBWMatching algorithm
        /// </summary>
        /// <param name="text"> A string BWT(Text) </param>
        /// <param name="n"> Number of patterns </param>
        /// <param name="patterns"> Collection of n strings Patterns </param>
        /// <returns> A list of integers, where the i-th integer corresponds
        /// to the number of substring matches of the i-th member of Patterns
        /// in Text. </returns>
        public long[] Solve(string text, long n, String[] patterns)
        {
			long[] result = new long[patterns.Length];

			int idx = 0;
			Dictionary<char, long[]> count = new Dictionary<char, long[]>();
			char[] chars = new char[5] { '$', 'A', 'C', 'G', 'T'};
			for (int i = 0; i < chars.Length; i++)
			{
				count.Add(chars[i], new long[text.Length + 1]);
				count[chars[i]][idx] = 0;
			}
			idx++;

			Dictionary<char, long> firstOccurrence = new Dictionary<char, long>(5);

			char[] firstColumn = text.ToCharArray();
			Array.Sort(firstColumn);

			for (int i = 0; i < text.Length; i++)
			{
				if (firstOccurrence.Count != 5)
				{
					if (!firstOccurrence.ContainsKey(firstColumn[i]))
						firstOccurrence.Add(firstColumn[i], i);
				}
				ModifyCount(text[i]);
			}

			for (int i = 0; i < patterns.Length; i++)
				result[i] = BWMatching(firstOccurrence, text, patterns[i], count);			

			void ModifyCount(char ch)
			{
				for (int i = 0; i < chars.Length; i++)
				{
					count[chars[i]][idx] = count[chars[i]][idx - 1];
				}
				count[ch][idx]++;
				idx++;
			}			

			return result;
        }

		public static long BWMatching(Dictionary<char, long> firstOccurrence, string lastColumn,
			string pattern, Dictionary<char, long[]> count)
		{
			long top = 0;
			long bottom = lastColumn.Length - 1;
			while (top <= bottom)
			{
				if (pattern.Length != 0)
				{
					char symbol = pattern.Last();
					if (!firstOccurrence.ContainsKey(symbol))
						return 0;
					pattern = pattern.Remove(pattern.Length - 1);

					top = firstOccurrence[symbol] + count[symbol][top];
					bottom = firstOccurrence[symbol] + count[symbol][bottom + 1] - 1;					
				}
				else return bottom - top + 1;
			}
			return 0;
		}
    }
}
