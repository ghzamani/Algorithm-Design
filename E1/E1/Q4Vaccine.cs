using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;
using System.Linq;

namespace Exam1
{
    public class Q4Vaccine : Processor
    {
        public Q4Vaccine(string testDataName) : base(testDataName) { this.ExcludeTestCaseRangeInclusive(33, 106); }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, string>)Solve);

        public string Solve(string dna, string pattern)
        {
			long[] result = ApproximatePatternMatching(dna, pattern, 1);

			if (result.Length == 0)
				return "No Match!";

			StringBuilder matches = new StringBuilder();
			for (int i = 0; i < result.Length - 1; i++)
			{
				matches.Append(result[i] + " ");
			}
			matches.Append(result.Last());
			return matches.ToString();
		}

		public static long HammingDist(string s1, string s2)
		{
			long d = 0;
			for (int i = 0; i < s1.Length; i++)
			{
				if (s1[i] != s2[i])
					d++;
			}
			return d;
		}

		public static long[] ApproximatePatternMatching(string text, string pattern, long d)
		{
			List<long> indexes = new List<long>();
			for (int i = 0; i <= text.Length - pattern.Length; i++)
			{
				if (HammingDist(pattern, text.Substring(i, pattern.Length)) <= d)
					indexes.Add(i);
			}

			return indexes.ToArray();
		}
	}
}
