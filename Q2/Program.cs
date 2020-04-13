using System;
using System.Collections.Generic;

namespace Q2
{
	class Program
	{
		static void Main(string[] args)
		{
			string text = Console.ReadLine();
			string pattern = Console.ReadLine();

			List<int> result = new List<int>();
			Dictionary<char, int> table = MakeArray(pattern);

			//int i = pattern.Length - 1;
			//int j = pattern.Length - 1;
			//while (true)
			//{
			//	if(text[j] == pattern[i])
			//	{

			//	}
			//}
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
	}
}
