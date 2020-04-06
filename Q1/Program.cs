using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
	class Program
	{
		static void Main(string[] args)
		{
			string a = Console.ReadLine();
			string b = Console.ReadLine();
			Console.WriteLine(HammingDist(a,b));
		}

		public static string HammingDist (string A, string B)
		{
			string result = string.Empty;
			int dist = 0;

			for (int i = 0; i < A.Length; i++)
			{
				if (A[i] != B[i])
				{
					if (dist % 2 != 0)
					{
						result += A[i];
					}
					else
					{
						result += B[i];						
					}
					dist++;

				}
				else result += A[i];
			}

			return dist % 2 == 0 ? result : "Not Possible";
		}
	}
}
