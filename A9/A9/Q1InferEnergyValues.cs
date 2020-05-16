using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

		public static long variablesCount;
        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
			double[] result = new double[MATRIX_SIZE];
			variablesCount = MATRIX_SIZE;

			for (int i = 0; i < MATRIX_SIZE; i++)
			{
				GaussianElimination(ref matrix, i);
			}

			for (int i = 0; i < MATRIX_SIZE; i++)
			{
				if(Math.Abs((int)matrix[i,MATRIX_SIZE] - matrix[i, MATRIX_SIZE]) < 0.25)
				{
					result[i] = (int)matrix[i, MATRIX_SIZE];
				}
				else
				{
					if(Math.Abs((int)matrix[i, MATRIX_SIZE] - matrix[i, MATRIX_SIZE]) >= 0.75)
					{
						result[i] = (int)matrix[i, MATRIX_SIZE];
						if (matrix[i, MATRIX_SIZE] < 0)
							result[i] -= 1;
						else result[i] += 1;
					}

					else
					{
						result[i] = (int)matrix[i, MATRIX_SIZE];

						if (matrix[i, MATRIX_SIZE] < 0)
							result[i] -= 0.5;
						else result[i] += 0.5;
					}
				}
			}
			return result;
        }

		//matrix[i,i] must be 1, matrix[i, other columns] = 0
		public static void GaussianElimination(ref double[,] matrix, int i)
		{
			int j = i;
			while(matrix[j,i] == 0)
			{
				j++;
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

			if(matrix[i,i] != 1)
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
