using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A6
{
    public class Q2ReconstructStringFromBWT
    {
		//static void Main(string[] args)
		//{
		//	string bwt = Console.ReadLine();
		//	Console.WriteLine(Solve(bwt));
		//}
        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public static string Solve(string bwt)
        {
			StringBuilder builder = new StringBuilder(bwt.Length);

			Tuple<char, int>[] lastColumn = new Tuple<char, int>[bwt.Length];			
			for (int i = 0; i < bwt.Length; i++)
				lastColumn[i] = new Tuple<char, int>(bwt[i], i);			
			
			//keep the indexes of sorted lastColumn
			int[] firstColumn = lastColumn.OrderBy(x => x.Item1).Select(x => x.Item2).ToArray();

			int length = 0;
			int idx = firstColumn[firstColumn[0]];
			while(length != bwt.Length)
			{
				builder.Append(lastColumn[idx].Item1);
				idx = firstColumn[idx];
				length++;
			}

			return builder.ToString();
        }
    }
}
