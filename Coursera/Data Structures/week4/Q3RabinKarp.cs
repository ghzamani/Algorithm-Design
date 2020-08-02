using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A10
{
	public class Q3RabinKarp 
	{
		//public static void Main(string[] args)
		//{
		//	string pattern = Console.ReadLine();
		//	string text = Console.ReadLine();
		//	long[] occurences = Solve(pattern, text);
		//	for (int i = 0; i < occurences.Length; i++)
		//	{
		//		Console.Write(occurences[i] + " ");
		//	}
		//}

		public const long BigPrimeNumber = 1000000007;
		public static long[] Solve(string pattern, string text)
		{
			long p = BigPrimeNumber;
			long x = 263;//new Random().Next(1, (int)p - 1);
			long patternHash = PolyHash(pattern, 0, pattern.Length, p, x);

			List<long> positions = new List<long>();
			long[] H = PreComputeHashes(text, pattern.Length, p, x);
			for (int i = 0; i <= text.Length - pattern.Length; i++)
			{
				if (patternHash != H[i])
					continue;

				StringBuilder sb = new StringBuilder(pattern.Length);
				for (int j = i; j <= i + pattern.Length - 1; j++)
					sb.Append(text[j]);
				if (sb.ToString() == pattern)
					positions.Add(i);
			}
			return positions.ToArray();
		}

		public static long[] PreComputeHashes(
			string T,
			int P,
			long p,
			long x)
		{
			long[] H = new long[T.Length - P + 1];
			H[H.Length - 1] = PolyHash(T, T.Length - P, P, p, x);

			long y = 1;
			for (int i = 1; i <= P; i++)
				checked
				{
					y = y * x % p;
				}
			for (int i = T.Length - P - 1; i >= 0; i--)
				checked
				{
					H[i] = x * H[i + 1] + T[i] - (y * T[i + P]);
					//does not calculate the negative numbers mod sth correctly
					//so first we make it positive
					while (H[i] < 0)
						H[i] += p;

					H[i] %= p;
				}

			return H;
		}

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
	}
}
