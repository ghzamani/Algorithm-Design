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
			this.ExcludeTestCaseRangeInclusive(40, 50);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, long n, string[] patterns)
        {
			List<long> result = new List<long>();

			//char[] test = { 'A', 'A', 'C', 'D' };
			//int f = first(test, 0, test.Length-1, 'B', test.Length - 1);
			//int l = last(test, 0, test.Length - 1, 'B', test.Length - 1);

			long[] suffixArray = Q2CunstructSuffixArray.BuildSuffixArray(text + "$");
			foreach(var pattern in patterns)
			{
				//Tuple<long, long> t = SuffixArrayPatternMatchin(text, pattern, suffixArray);
				//long start = t.Item1;
				//long end = t.Item2;

				//while (start <= end) //if start > end -> pattern was not found
				//{
				//	if (!result.Contains(suffixArray[start]))
				//		result.Add(suffixArray[start]);
				//	start++;
				//}
				//text += "$";
				long start = BinarySearch(text, pattern, suffixArray);
				while (start < suffixArray.Length && string.Compare(pattern, text.Substring((int)suffixArray[start])) != 1)
				{
					if(text.Length - suffixArray[start] >= pattern.Length)
					{
						if (pattern == text.Substring((int)suffixArray[start], pattern.Length))
							if (!result.Contains(suffixArray[start]))
								result.Add(suffixArray[start]);
					}
					start++;					
				}
			}

			return result.Count != 0 ? result.ToArray() : new long[1] { -1 };
        }

		/* if x is present in arr[] then 
		returns the index of FIRST  
		occurrence of x in arr[0..n-1], 
		otherwise returns -1 */
		public static int first(char[] arr, int low,
							int high, char x, int n)
		{
			if (high >= low)
			{
				int mid = low + (high - low) / 2;

				if ((mid == 0 || x > arr[mid - 1])
								 && arr[mid] == x)
					return mid;
				else if (x > arr[mid])
					return first(arr, (mid + 1),
									 high, x, n);
				else
					return first(arr, low,
								 (mid - 1), x, n);
			}

			return -1;
		}

		/* if x is present in arr[] then returns 
		the index of LAST occurrence of x in  
		arr[0..n-1], otherwise returns -1 */
		public static int last(char[] arr, int low,
							int high, char x, int n)
		{
			if (high >= low)
			{
				int mid = low + (high - low) / 2;

				if ((mid == n - 1 || x < arr[mid + 1])
								  && arr[mid] == x)
					return mid;
				else if (x < arr[mid])
					return last(arr, low, (mid - 1),
											 x, n);
				else
					return last(arr, (mid + 1),
									   high, x, n);
			}

			return -1;
		}


		public static long BinarySearch(string text, string pattern, long[] suffixArray)
		{
			long min = 0;
			long max = text.Length;
			long mid;
			//pattern += "$";

			while (min < max)
			{
				mid = (min + max) / 2;
				if (string.Compare(pattern, text.Substring((int)suffixArray[mid])) == 1) //(pattern > text.Substring((int)suffixArray[mid]))
					min = mid + 1;
				else max = mid;
			}
			return min; //returns start point that pattern might be found
		}


		public static Tuple<long,long> SuffixArrayPatternMatchin(string text, string pattern, long[] suffixArray)
		{
			long min = 0;
			long max = text.Length;
			long mid;
			//pattern += "$";

			while (min < max)
			{
				mid = (min + max) / 2;
				if (string.Compare(pattern, text.Substring((int)suffixArray[mid])) == 1) //(pattern > text.Substring((int)suffixArray[mid]))
					min = mid + 1;
				else max = mid;
			}
			long start = min;

			pattern = "Z" + pattern;
			min = 0;
			max = text.Length;
			while (min < max)
			{
				mid = (min + max) / 2;
				if (string.Compare(pattern, text.Substring((int)suffixArray[mid])) == -1) //(pattern < text.Substring((int)suffixArray[mid]))
					max = mid;
				else   min = mid + 1;
			}
			long end = max;
			return new Tuple<long, long>(start, end);
		}
		public static long LCPOfSuffixes(string s, long i, long j, long equal)
		{
			long lcp = equal > 0 ? equal : 0;
			while (i + lcp < s.Length && j + lcp < s.Length)
			{
				if (s[(int)(i + lcp)] != s[(int)(j + lcp)]) //a bit different from slide
					break;
				lcp++;
			}
			return lcp;
		}

		public static long[] InvertSuffixArray (long[] order)
		{
			long[] pos = new long[order.Length];
			for (int i = 0; i < pos.Length; i++)
			{
				pos[order[i]] = i;
			}
			return pos;
		}

		public static long[] ComputeLCPArray(string s, long[] order)
		{
			long[] lcpArray = new long[s.Length - 1];
			long lcp = 0;
			long[] posInOrder = InvertSuffixArray(order);
			long suffix = order[0];
			for (int i = 0; i < s.Length; i++)
			{
				long orderIndex = posInOrder[suffix];
				if (orderIndex == s.Length - 1)
				{
					lcp = 0;
					suffix = (++suffix) % s.Length;
					continue;
				}
				long nextSuffix = order[orderIndex + 1];
				lcp = LCPOfSuffixes(s, suffix, nextSuffix, lcp - 1);
				lcpArray[orderIndex] = lcp;
				suffix = (++suffix) % s.Length;
			}
			return lcpArray;
		}
	}
}
