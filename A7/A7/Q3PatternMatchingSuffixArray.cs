using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
	public class Q3PatternMatchingSuffixArray : Processor
	{
		public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
		{
			this.VerifyResultWithoutOrder = true;
		}

		public override string Process(string inStr) =>
		TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

		protected virtual long[] Solve(string text, long n, string[] patterns)
		{
			List<long> result = new List<long>();
			HashSet<long> results = new HashSet<long>();

			long[] suffixArray = Q2CunstructSuffixArray.BuildSuffixArray(text + "$");
			foreach (var pattern in patterns)
			{
				long[] t = SuffixArrayPatternMatching(text, pattern, suffixArray);
				long start = t[0];
				long end = t[1];

				if (start > end)
					continue;

				while (start < end) //if start > end -> pattern was not found
				{
					//if (!result.Contains(suffixArray[start]))
					//	result.Add(suffixArray[start]);
					results.Add(suffixArray[start]);
					start++;
				}

				if (pattern.Length <= text.Length - suffixArray[start])
				{
					if (pattern == text.Substring((int)suffixArray[start], pattern.Length))
						//if (!result.Contains(suffixArray[start]))
						//	result.Add(suffixArray[start]);
						results.Add(suffixArray[start]);
				}
			}

			//return result.Count != 0 ? result.ToArray() : new long[1] { -1 };
			return results.Count != 0 ? results.ToArray() : new long[1] { -1 };

		}

		public static long[] SuffixArrayPatternMatching(string text, string pattern, long[] suffixArray)
		{
			long min = 0;
			long max = text.Length;
			long mid;

			while (min < max)
			{
				mid = (min + max) / 2;

				if (string.Compare(pattern, text.Substring((int)suffixArray[mid])) == 1) //(pattern > text.Substring(suffixArray[mid]))
					min = mid + 1;
				else max = mid;				
			}
			long start = min;

			max = text.Length;
			while (min < max)
			{
				mid = (min + max) / 2;
				if (pattern.Length <= text.Length - suffixArray[mid])
				{
					if (string.Compare(pattern, text.Substring((int)suffixArray[mid], pattern.Length)) == -1) //(pattern < text.Substring(suffixArray[mid]))
						max = mid;
					else min = mid + 1;
				}
				else
				{
					if (string.Compare(pattern, text.Substring((int)suffixArray[mid])) == -1) //(pattern < text.Substring((int)suffixArray[mid]))
						max = mid;
					else min = mid + 1;
				}
			}
			long end = max;
			return new long[2] { start, end } ;
		}		
	}
}
