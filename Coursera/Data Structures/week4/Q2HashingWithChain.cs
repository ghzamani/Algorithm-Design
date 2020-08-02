using System;
using System.Collections.Generic;
using System.Linq;

namespace A10
{
	public class Q2HashingWithChain
	{
		//public static void Main(string[] args)
		//{
		//	long bucketCount = long.Parse(Console.ReadLine());
		//	int n = int.Parse(Console.ReadLine());
		//	string[] commands = new string[n];
		//	for (int i = 0; i < n; i++)
		//	{
		//		commands[i] = Console.ReadLine();
		//	}
		//	Q2HashingWithChain q2 = new Q2HashingWithChain();
		//	string[] result = q2.Solve(bucketCount, commands);
		//	foreach(var r in result)
		//		Console.WriteLine(r);
		//}

		public List<string>[] Data;
		public string[] Solve(long bucketCount, string[] commands)
		{
			Data = new List<string>[bucketCount];
			for (int i = 0; i < bucketCount; i++)
				Data[i] = new List<string>();

			List<string> result = new List<string>();
			foreach (var cmd in commands)
			{
				var toks = cmd.Split();
				var cmdType = toks[0];
				var arg = toks[1];

				switch (cmdType)
				{
					case "add":
						Add(arg);
						break;
					case "del":
						Delete(arg);
						break;
					case "find":
						result.Add(Find(arg));
						break;
					case "check":
						result.Add(Check(int.Parse(arg)));
						break;
				}
			}
			return result.ToArray();
		}

		public const long BigPrimeNumber = 1000000007;
		public const long ChosenX = 263;

		public static long PolyHash(
			string str, int start, int count,
			long p = BigPrimeNumber, long x = ChosenX)
		{
			long hash = 0;
			for (int i = start + count - 1; i >= start; i--)
			{
				checked
				{
					hash = (hash * x + str[i]) % p;
				}
			}
			return hash;
		}

		public void Add(string str)
		{
			long hash = PolyHash(str, 0, str.Length) % Data.Length;

			if (Data[hash].Count == 0)
				Data[hash].Add(str);

			else
				if(!Data[hash].Contains(str))
					Data[hash].Insert(0, str);
		}

		public string Find(string str)
		{
			long hash = PolyHash(str, 0, str.Length) % Data.Length;

			for (int i = 0; i < Data[hash].Count; i++)
			{
				if (Data[hash][i] == str)
					return "yes";
			}
			return "no";
		}

		public void Delete(string str)
		{
			long hash = PolyHash(str, 0, str.Length) % Data.Length;

			if (Data[hash].Count != 0)
				if (Data[hash].Contains(str))
					Data[hash].Remove(str);
		}

		public string Check(int i)
		{
			if (Data[i].Count != 0)
			{
				string result = string.Join(" ", Data[i]);
				return result;
			}

			return "-";
		}
	}
}
