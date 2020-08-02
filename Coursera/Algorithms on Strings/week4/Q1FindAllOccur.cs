using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    public class Q1FindAllOccur
    {
		//static void Main(string[] args)
		//{
		//	string pattern = Console.ReadLine();
		//	string text = Console.ReadLine();

		//	long[] indexes = Solve(text, pattern);
		//	if (indexes.Length == 0)
		//	{
		//		Console.WriteLine();
		//		return;
		//	}

		//	foreach (var index in indexes)
		//		Console.Write(index + " ");
		//}
        protected static long[] Solve(string text, string pattern)
        {
			if (pattern.Length > text.Length)
				return new long[0];

			List<long> indexes = new List<long>();

			string s = pattern + "$" + text;
			long[] prefixNums = ComputePrefixFunction(s);
			for (int i = pattern.Length + 1; i < s.Length; i++)
			{
				if (prefixNums[i] == pattern.Length)
					indexes.Add(i - 2 * pattern.Length);
			}

			return indexes.Count != 0 ? indexes.ToArray() : new long[0];
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
