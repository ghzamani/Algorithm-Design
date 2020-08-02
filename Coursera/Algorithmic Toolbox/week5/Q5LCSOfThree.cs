using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A6
{
    public class Q5LCSOfThree
    {
		static void Main(string[] args)
		{
			Console.ReadLine();
			var seq1 = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
			Console.ReadLine();
			var seq2 = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
			Console.ReadLine();
			var seq3 = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
			Console.WriteLine(Solve(seq1,seq2,seq3));
		}

		public static long Solve(long[] seq1, long[] seq2, long[] seq3)
        {
			long[, ,] alignmentScore = new long[seq1.Length +1 , seq2.Length + 1,seq3.Length +1];
			for(int i = 0; i <= seq1.Length; i++)
			{
				alignmentScore[i, 0, 0] = i;
			}
			for(int i = 0; i <= seq2.Length; i++)
			{
				alignmentScore[0, i, 0] = i;
			}
			for(int i = 0; i <= seq3.Length; i++)
			{
				alignmentScore[0, 0, i] = i;
			}
			
			for(int i = 1; i <= seq1.Length; i++)
			{
				for(int j = 1; j <= seq2.Length; j++)
				{
					for(int k = 1; k <= seq3.Length; k++)
					{
						long insertion = alignmentScore[i, j - 1, k];
						long deletion = alignmentScore[i, j, k - 1];
						long match = alignmentScore[i - 1, j - 1, k - 1] + 1;
						long misMatch = alignmentScore[i - 1, j, k];

						if ((seq1[i - 1] == seq2[j - 1]) &&
							(seq1[i - 1] == seq3[k - 1]))
							alignmentScore[i, j, k] = match;
						else
							alignmentScore[i, j, k] = Math.Max(Math.Max(insertion, deletion), misMatch);
					}
				}
			}
			return alignmentScore[seq1.Length, seq2.Length, seq3.Length];
        }
    }
}
