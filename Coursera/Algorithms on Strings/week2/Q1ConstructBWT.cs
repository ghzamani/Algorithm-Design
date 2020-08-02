using System;
using System.Collections.Generic;
using System.Linq;

namespace A6
{
    public class Q1ConstructBWT
    {
		//static void Main(string[] args)
		//{
		//	string text = Console.ReadLine();
		//	Console.WriteLine(Solve(text));
		//}
        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public static string Solve(string text)
        {
			string[] matrix = new string[text.Length];
			//a matrix of char array, char arary has joined and made a string

			for (int i = 0; i < text.Length; i++)
			{
				string str = string.Empty;

				str += text.Substring(i);
				str += text.Substring(0, text.Length - str.Length);
				matrix[i] = str;
			}
			Array.Sort(matrix);

			string result = string.Empty;
			for(int i = 0; i < text.Length; i++)
				result += matrix[i][text.Length - 1];			

			return result;
        }
    }
}
