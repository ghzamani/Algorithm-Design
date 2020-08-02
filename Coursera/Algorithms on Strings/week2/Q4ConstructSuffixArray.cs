using System;
using System.Collections.Generic;
using System.Linq;

namespace A6
{
    public class Q4ConstructSuffixArray
    {
		//static void Main(string[] args)
		//{
		//	string text = Console.ReadLine();
		//	long[] result = Solve(text);
		//	foreach (var r in result)
		//		Console.Write(r + " ");
		//}

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public static long[] Solve(string text)
        {
			long[] result = new long[text.Length];

			Tuple<string, long>[] suffixes = new Tuple<string, long>[text.Length];
			for (int i = 0; i < text.Length; i++)
				suffixes[i] = new Tuple<string, long>(text.Substring(i, text.Length - i), i);
			
			suffixes = suffixes.OrderBy(x => x.Item1).ToArray();

			for (int i = 0; i < text.Length; i++)
				result[i] = suffixes[i].Item2;
			
			return result;
        }
    }
}
