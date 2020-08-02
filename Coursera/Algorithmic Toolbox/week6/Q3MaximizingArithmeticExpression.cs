using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace A7
{
	public class Q3MaximizingArithmeticExpression 
	{
		//static void Main(string[] args)
		//{
		//	string input = Console.ReadLine();
		//	Console.WriteLine(Solve(input));
		//}

		public static long Solve (string expression)
		{
			int[] nums = new int[expression.Length / 2 + 1];
			long arraysLength = nums.Length;

			char[] operations = new char[expression.Length - arraysLength];
			for (int i = 0; i < expression.Length; i++)
			{
				if (i % 2 == 0)
					nums[i / 2] = int.Parse(expression[i].ToString());
				else operations[i / 2] = expression[i];
			}
			long[,] Max = new long[arraysLength, arraysLength];
			long[,] Min = new long[arraysLength, arraysLength];
			for (int i = 0; i < arraysLength; i++)
			{
				Max[i, i] = nums[i];
				Min[i, i] = nums[i];
			}
			for (int j = 1; j < arraysLength; j++)
			{
				for (int i = 0; i < arraysLength - j; i++)
					FindMinAndMax(Min, Max, nums, operations, i, i + j);				
			}
			return Max[0, arraysLength - 1];
		}		

		static void FindMinAndMax(long[,] Min, long[,] Max, int[] numbers, char[] operations, int i, int j)
		{
			long min = int.MaxValue;
			long max = int.MinValue;
			for (int k = i; k < j; k++)
			{
				long a = Operate(Min[i, k], Min[k + 1, j], operations[k]);
				long b = Operate(Min[i, k], Max[k + 1, j], operations[k]);
				long c = Operate(Max[i, k], Min[k + 1, j], operations[k]);
				long d = Operate(Max[i, k], Max[k + 1, j], operations[k]);
				min = SmallestNum(a, b, c, d, min);
				max = BiggestNum(a, b, c, d, max);
			}
			Max[i, j] = max;
			Min[i, j] = min;
		}

		public static long Operate(long a, long b, char o)
		{
			switch (o)
			{
				case '+':
					return a + b;
				case '-':
					return a - b;
				case '*':
					return a * b;
			}
			return 0;
		}
		public static long SmallestNum(params long[] values) => Enumerable.Min(values);
		public static long BiggestNum(params long[] values) => Enumerable.Max(values);
	}
}
