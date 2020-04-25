using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, string pattern)
        {
			if (pattern.Length > text.Length)
				return new long[1] { -1 };

			List<long> indexes = new List<long>();

			string s = pattern + "$" + text;
			long[] prefixNums = ComputePrefixFunction(s);
			for (int i = pattern.Length + 1; i < s.Length; i++)
			{
				if (prefixNums[i] == pattern.Length)
					indexes.Add(i - 2 * pattern.Length);
			}

			return indexes.Count != 0 ? indexes.ToArray() : new long[1] { -1 };
        }

		public static long[] ComputePrefixFunction(string str)
		{
			long[] prefixNums = new long[str.Length];
			prefixNums[0] = 0;
			long border = 0;
			for (int i = 1; i < str.Length; i++)
			{
				while(border > 0 && str[i] != str[(int)border])
				{
					border = prefixNums[border - 1];
				}
				if (str[i] == str[(int)border])
					border++;
				else border = 0;
				prefixNums[i] = border;
			}
			return prefixNums;
		}
	}
}
