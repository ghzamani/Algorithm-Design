using System;
using System.Collections.Generic;

namespace Q3
{
	public class Program
	{
		static void Main(string[] args)
		{
			string text = Console.ReadLine();
			string pattern = Console.ReadLine();

			long[] result = Search(text, pattern);
			foreach (var r in result)
				Console.Write(r + " ");
		}

		public static long[] Z_Function(string str)
		{
			long[] z = new long[str.Length];
			z[0] = 0;

			int left = 0;
			int right = 0;
			for (int i = 1; i < str.Length; i++)
			{
				if (i > right)
				{
					left = i;
					right = i;					
				}

				else
				{
					int idx = i - left;
					if (z[idx] < right - i + 1)
					{
						z[i] = z[idx];
						continue; // only this part doesn't reach to while
					}

					else left = i;											
				}

				while (right < str.Length)
				{
					if (str[right - left] != str[right])
						break;
					right++;
				}
				z[i] = right - left;
				right--;
			}

			return z;
		}

		public static long[] Search(string text, string pattern)
		{
			string s = pattern + "$" + text;
			long[] zArray = Z_Function(s);

			List<long> indexes = new List<long>();

			for (int i = pattern.Length + 1; i < s.Length; i++)
			{
				if (zArray[i] == pattern.Length)
					indexes.Add(i - pattern.Length - 1);
			}

			return indexes.Count != 0 ? indexes.ToArray() : new long[1] { -1 };
		}
	}
}
