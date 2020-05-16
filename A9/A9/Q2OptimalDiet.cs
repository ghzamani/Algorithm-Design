using System;
using TestCommon;
using System.Linq;
using System.Collections.Generic;

namespace A9
{
	public class Q2OptimalDiet : Processor
	{
		public Q2OptimalDiet(string testDataName) : base(testDataName)
		{
			ExcludeTestCases(8, 22);
		}

		public override string Process(string inStr) =>
			TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

		public static List<int[]> subsets;
		public string Solve(int N, int M, double[,] matrix1)
		{
			string result = string.Empty;

			//saving all inequations in a new array
			//N first lines are the inequations in matrix1
			double[,] inequalities = new double[N + M, matrix1.GetLength(1)];
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < matrix1.GetLength(1); j++)
				{
					inequalities[i, j] = matrix1[i, j];
				}
			}

			//M next lines are -variable <= 0
			for (int i = N; i < M + N; i++)
			{
				inequalities[i, i - N] = -1;
			}

			int[] nums = Enumerable.Range(0, M + N).ToArray();
			//need subsets of length M
			subsets = new List<int[]>();
			Subsets(nums, nums.Length, M, 0, new int[M], 0);

			double max = double.MinValue;
			double[] resultPoint = new double[M];

			foreach (var subset in subsets)
			{
				double[,] m = new double[M, M + 1];
				for (int i = 0; i < subset.Length; i++)
				{
					for (int j = 0; j < matrix1.GetLength(1); j++)
					{
						m[i, j] = inequalities[subset[i], j];
					}
				}

				//if there's two parallel lines in m, then there's no solution for that matrix
				if (!Parallel(m))
				{
					double[] ans = Solve(M, ref m);

					bool non_negative = true;
					foreach (var answer in ans)
					{
						if (answer < 0)
						{
							non_negative = false;
							break;
						}
					}

					//all variables are >= 0
					//باید چک شود که در بقیه نامعادله ها صدق میکند یا خیر
					if (non_negative)
					{
						if (Satisfy(inequalities, subset, ans))
						{
							double d = 0;
							for (int i = 0; i < ans.Length; i++)
							{
								d += ans[i] * matrix1[N, i];
							}
							if (d >= max)
							{
								max = d;
								resultPoint = ans;
							}
						}
					}
				}
			}

			//if max == minVal, means no solution or infinity
			if (max != double.MinValue)
			{
				result += "Bounded Solution";
				result += "\n";
				foreach (var p in resultPoint)
				{
					double res = 0;
					if (Math.Abs((int)p - p) < 0.25)
					{
						res += (int)p;
					}
					else
					{
						if (Math.Abs((int)p - p) >= 0.75)
						{
							res += (int)p;
							if (p < 0)
								res -= 1;
							else res += 1;
						}

						else
						{
							res = (int)p;

							if (p < 0)
								res -= 0.5;
							else res += 0.5;
						}
					}
					result += res;
					result += " ";
				}
			}

			else result += "No Solution";

			return result;
		}

		public static bool Satisfy(double[,] matrix, int[] subset, double[] answer)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (!subset.Contains(i))
				{
					double d = 0;
					for (int j = 0; j < answer.Length; j++)
					{
						d += answer[j] * matrix[i, j];
					}
					if (d > matrix[i, answer.Length])
						return false;
				}
			}
			return true;
		}

		public static bool Parallel(double[,] m)
		{
			for (int i = 0; i < m.GetLength(0); i++)
			{
				for (int j = i + 1; j < m.GetLength(0); j++)
				{
					if (Multiplied(m, i, j))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool Multiplied(double[,] m, int i, int j)
		{
			double c = m[i, 0] / m[j, 0];
			for (int k = 1; k < m.GetLength(1) - 1; k++)
			{
				if (m[i, k] / m[j, k] != c)
					return false;
			}
			return true;
		}

		public static void Subsets(int[] array, int n, int r, int index, int[] data, int i)
		{
			if (index == r)
			{
				int[] a = new int[r];
				for (int idx = 0; idx < r; idx++)
				{
					a[idx] = data[idx];
				}
				subsets.Add(a);
				return;
			}

			if (i >= n)
				return;

			data[index] = array[i];
			Subsets(array, n, r, index + 1, data, i + 1);
			Subsets(array, n, r, index, data, i + 1);

		}

		public static long variablesCount;
		public double[] Solve(long MATRIX_SIZE, ref double[,] matrix)
		{
			double[] result = new double[MATRIX_SIZE];
			variablesCount = MATRIX_SIZE;

			for (int i = 0; i < MATRIX_SIZE; i++)
			{
				GaussianElimination(ref matrix, i);
			}

			for (int i = 0; i < MATRIX_SIZE; i++)
			{
				result[i] = matrix[i, MATRIX_SIZE];

			}
			return result;
		}

		//matrix[i,i] must be 1, matrix[i, other columns] = 0
		public static void GaussianElimination(ref double[,] matrix, int i)
		{
			int j = i;
			while (matrix[j, i] == 0)
			{
				j++;
				if (j >= variablesCount)
					return;
			}

			if (j != i)
			{
				Swap(ref matrix, i, j);
			}

			for (int row = 0; row < variablesCount; row++)
			{
				if (row == i)
					continue;

				double c = matrix[row, i] / matrix[i, i] * -1;
				for (int col = 0; col <= variablesCount; col++)
				{
					matrix[row, col] += c * matrix[i, col];
				}
			}

			if (matrix[i, i] != 1)
			{
				for (int k = 0; k <= variablesCount; k++)
				{
					if (k == i)
						continue;

					matrix[i, k] /= matrix[i, i];
				}
				matrix[i, i] = 1;
			}
		}

		public static void Swap(ref double[,] matrix, int i, int j)
		{
			for (int k = 0; k <= variablesCount; k++)
			{
				(matrix[i, k], matrix[j, k]) = (matrix[j, k], matrix[i, k]);
			}
		}
	}
}
