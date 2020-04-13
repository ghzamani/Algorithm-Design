using System;
using System.Collections.Generic;

namespace Q2
{
	public class Program
	{
		static void Main(string[] args)
		{
			string text = Console.ReadLine();
			string pattern = Console.ReadLine();

			Dictionary<char, int> table = MakeArray(pattern);

			int[] result = Matches(text, pattern, table);
			for (int i = 0; i < result.Length; i++)
				Console.Write(result[i] + " ");
		}

		public static Dictionary<char,int> MakeArray(string str)
		{
			Dictionary<char, int> result = new Dictionary<char, int>();
			for (int i = 0; i < str.Length; i++)
			{
				if (!result.ContainsKey(str[i]))
					result.Add(str[i], 0);
				result[str[i]] = Math.Max(1, str.Length - i - 1);
			}
			result.Add('*', str.Length);
			return result;
		}

		public static int[] Matches (string text, string pattern, Dictionary<char, int> table)
		{
			if (pattern.Length > text.Length)
				return new int[1] { -1 };

			List<int> indexes = new List<int>();

			int i = pattern.Length - 1; //iterates over pattern
			int j = pattern.Length - 1; //iterates over text
			int jcopy = j;

			while (j < text.Length)
			{
				if (text[j] == pattern[i])
				{
					if (i == 0) //pattern was found
					{
						indexes.Add(j);												
						j = jcopy + table[text[j]];
						jcopy = j;
						i = pattern.Length - 1;
						continue;
					}
					j--;
					i--;
				}
				else
				{
					if (!table.ContainsKey(text[j]))
					{
						j = j + pattern.Length;
					}
					else j = jcopy + table[text[j]];
					i = pattern.Length - 1;
					jcopy = j;
				}
			}

			return indexes.Count != 0 ? indexes.ToArray() : new int[1] { -1 };
		}
	}
}
