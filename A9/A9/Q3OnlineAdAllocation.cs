using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {

        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

		public static string[] leftSidedCol;
		public string Solve(int c, int v, double[,] matrix1) //c =3 , v =2 tedad vars
        {
			string result = string.Empty;

			double[,] table = MakeTable(c, v, matrix1);

			int? entering = EnteringRow(table);
			int leaving;
			while (entering.HasValue)
			{
				double min = double.MaxValue;
				for (int i = 0; i < c; i++)
				{
					if (table[i, entering.Value] > 0)
					{
						if(table[i,table.GetLength(1) - 1] / table[i,entering.Value] < min)
						{
							min = table[i, table.GetLength(1) - 1] / table[i, entering.Value];
							leaving = i;
						}
					}
				}
			}
			return result;
		}

		public static double[,] MakeTable(int c, int v, double[,] matrix1)
		{
			int artificialsCount = 0;
			for (int i = 0; i < c; i++)
			{
				if (matrix1[i, v] < 0)
				{
					artificialsCount++;
				}
			}

			double[,] table = new double[c + 1, c + v + artificialsCount + 1];
			leftSidedCol = new string[c]; //maybe needed to be c+1

			int artifindex = 0;
			for (int i = 0; i < c; i++)
			{
				if (matrix1[i, v] < 0)
				{
					for (int j = 0; j < v; j++)
					{
						table[i, j] = -1 * matrix1[i, j];
					}

					//s(i) must be -1
					table[i, i + v] = -1;

					//new artificial must be added
					table[i, v + c + artifindex] = 1;

					//table[i,lastColumn] = -constant
					table[i, table.GetLength(1) - 1] = -1 * matrix1[i, v];

					//fill left column with "a"
					leftSidedCol[i] = $"a{artifindex}";

					artifindex++;
				}

				else
				{
					for (int j = 0; j < v; j++)
					{
						table[i, j] = matrix1[i, j];
					}

					//s(i) must be 1
					table[i, i + v] = 1;

					//table[i,lastColumn] = constant
					table[i, table.GetLength(1) - 1] = matrix1[i, v];

					//fill left column with "a"
					leftSidedCol[i] = $"s{i}";
				}
			}

			//last row of the table must be the objective function
			for (int i = 0; i < v; i++)
			{
				table[c, i] = -1 * matrix1[c, i];
			}
			return table;
		}

		//if the minimum valud of last row isn't negative -> return null		
		public static int? EnteringRow(double[,] matrix) //ممکنه تغییر نیاز داشته باشه
		{
			int lastRowidx = matrix.GetLength(0) - 1;
			double min = matrix[lastRowidx, 0];
			int idx = 0;

			for (int i = 1; i < matrix.GetLength(1) - 1; i++) //momkene -1 ezafe bashe
			{
				if (matrix[lastRowidx, i] < min) //ممکنه منفی ترین عدد اگه توی کانست ها باشه نشه انتخاب بشه
				{
					min = matrix[lastRowidx, i];
					idx = i;
				}
			}
			if (min < 0)
				return idx;
			return null;
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
		//returns false if the whole last column is 0
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
