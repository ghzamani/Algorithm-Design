using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
	public class Q3PatternMatchingSuffixArray
	{
		//static void Main(string[] args)
		//{
		//	string text = Console.ReadLine();
		//	long n = long.Parse(Console.ReadLine());
		//	string[] patterns = Console.ReadLine().Split(' ');

		//	long[] result = Solve(text, n, patterns);
		//	if (result.Length == 0)
		//	{
		//		Console.WriteLine();
		//		return;
		//	}

		//	foreach (var r in result)
		//		Console.Write(r + " ");
		//}
		protected static long[] Solve(string text, long n, string[] patterns)
		{
			//List<long> result = new List<long>();
			HashSet<long> results = new HashSet<long>();

			long[] suffixArray = BuildSuffixArray(text + "$");
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
			return results.Count != 0 ? results.ToArray() : new long[0];

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
					if (string.Compare(pattern, text.Substring((int)suffixArray[mid])) == -1) //(pattern < text.Substring(suffixArray[mid]))
						max = mid;
					else min = mid + 1;
				}
			}
			long end = max;
			return new long[2] { start, end } ;
		}

		public static long[] SortCharacters(string s)
		{
			long[] order = new long[s.Length];

			List<char> alphabet = new List<char> { '$', 'A', 'C', 'G', 'T' };

			Dictionary<char, long> count = new Dictionary<char, long>();
			for (int i = 0; i < alphabet.Count; i++)
				count.Add(alphabet[i], 0);

			for (int i = 0; i < s.Length; i++)
				count[s[i]]++;

			count = count.Where(x => x.Value != 0).ToDictionary(x => x.Key, x => x.Value);
			//if there 's a zero value -> must be omitted
			for (int i = alphabet.Count - 1; i >= 0; i--)
			{
				if (!count.ContainsKey(alphabet[i]))
					alphabet.Remove(alphabet[i]);
			}

			for (int i = 1; i < alphabet.Count; i++)
				count[alphabet[i]] += count[alphabet[i - 1]];

			for (int i = s.Length - 1; i >= 0; i--)
			{
				char c = s[i];
				count[c]--;
				order[count[c]] = i;
			}
			return order;
		}

		public static long[] ComputeCharClasses(string s, long[] order)
		{
			long[] @class = new long[order.Length];
			@class[order[0]] = 0;

			for (int i = 1; i < s.Length; i++)
			{
				if (s[(int)order[i]] != s[(int)order[i - 1]])
					@class[order[i]] = @class[order[i - 1]] + 1;
				else @class[order[i]] = @class[order[i - 1]];
			}

			return @class;
		}

		public static long[] SortDoubled(string s, long l, long[] order, long[] @class)
		{
			long[] count = new long[s.Length];
			long[] newOrder = new long[s.Length];
			for (int i = 0; i < s.Length; i++)
			{
				count[@class[i]]++;
			}
			for (int i = 1; i < s.Length; i++)
			{
				count[i] += count[i - 1];
			}
			for (int i = s.Length - 1; i >= 0; i--)
			{
				long start = (order[i] - l + s.Length) % s.Length;
				long cl = @class[start];
				count[cl]--;
				newOrder[count[cl]] = start;
			}
			return newOrder;
		}

		public static long[] UpdateClasses(long[] newOrder, long[] @class, long l)
		{
			long n = newOrder.Length;
			long[] newClass = new long[n];
			newClass[newOrder[0]] = 0;

			for (int i = 1; i < n; i++)
			{
				long cur = newOrder[i];
				long prev = newOrder[i - 1];
				long mid = (cur + l) % n;
				long midPrev = (prev + l) % n;

				if (@class[cur] != @class[prev] || @class[mid] != @class[midPrev])
					newClass[cur] = newClass[prev] + 1;
				else newClass[cur] = newClass[prev];
			}
			return newClass;
		}

		public static long[] BuildSuffixArray(string s)
		{
			long[] order = SortCharacters(s);
			long[] @class = ComputeCharClasses(s, order);
			long l = 1;
			while (l < s.Length)
			{
				order = SortDoubled(s, l, order, @class);
				@class = UpdateClasses(order, @class, l);
				l *= 2;
			}
			return order;
		}
	}
}
