using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7
{
    public class Q2CunstructSuffixArray
    {
		//static void Main(string[] args)
		//{
		//	string text = Console.ReadLine();
		//	long[] suffixArray = Solve(text);
		//	foreach (var s in suffixArray)
		//		Console.Write(s + " ");
		//}
        protected static long[] Solve(string text)
        {
			return BuildSuffixArray(text);
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

		public static long[] SortDoubled (string s, long l, long[] order, long[] @class)
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
