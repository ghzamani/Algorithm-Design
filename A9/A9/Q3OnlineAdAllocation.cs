using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {

        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {  }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

		public static string[] leftSidedCol;
		public static int artificialsCount;
		public string Solve(int c, int v, double[,] matrix1)
        {
			string result = string.Empty;

			double[,] table = MakeTable(c, v, matrix1);

			int? entering = EnteringRow(table);
			int leaving = 0;
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

				//if a negative item was found in last row but no leaving was found -> infinitely solutions
				if(min == double.MaxValue) 
				{
					return "Infinity";
				}

				leftSidedCol[leaving] = AddToLeftCol(v, c, artificialsCount, entering.Value);
				Elimination(ref table, leaving, entering.Value);

				entering = EnteringRow(table);
			}

			double[] answer = new double[v];
			for (int i = 0; i < leftSidedCol.Length; i++)
			{
				//if value of an "a" was positive -> no solution
				if(leftSidedCol[i][0] == 'a')
				{
					if (table[i, table.GetLength(1) - 1] > 0)
						return "No Solution";
				}

				if(leftSidedCol[i][0] == 'x')
				{
					int index = int.Parse(leftSidedCol[i].Substring(1));
					double val = table[i, table.GetLength(1) - 1];

					if (Math.Abs((int)val - val) < 0.25)
					{
						answer[index] = (int)val;
					}
					else
					{
						if (Math.Abs((int)val - val) >= 0.75)
						{
							answer[index] = (int)val;
							if (val < 0)
								answer[index] -= 1;
							else answer[index] += 1;
						}

						else
						{
							answer[index] = (int)val;

							if (val < 0)
								answer[index] -= 0.5;
							else answer[index] += 0.5;
						}
					}
				}
			}

			result += "Bounded Solution" + "\n";

			for (int i = 0; i < answer.Length; i++)
			{
				result += answer[i] + " ";
			}

			return result;
		}

		public static double[,] MakeTable(int c, int v, double[,] matrix1)
		{
			artificialsCount = 0;
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

		//if the minimum value of last row isn't negative -> return null		
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

		public static string AddToLeftCol (int vars, int sCount, int aCount, int i)
		{
			if (i < vars)
			{
				return $"x{i}";
			}

			else
			{
				if (i < vars + sCount)
				{
					return $"s{i - vars}";
				}

				else
				{
					return $"a{i - vars + sCount}";
				}
			}
		}


		//matrix[leaving, entering] must be 1, matrix[i, entering] = 0
		public static void Elimination(ref double[,] matrix, int row, int col)
		{	
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (row == i)
					continue;

				double c = matrix[i,col] / matrix[row,col] * -1;
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					matrix[i, j] += c * matrix[row, j];
				}
			}

			if (matrix[row, col] != 1)
			{
				for (int k = 0; k < matrix.GetLength(1); k++)
				{
					if (k == col)
						continue;

					matrix[row, k] /= matrix[row, col];
				}
				matrix[row, col] = 1;
			}
		}
	}
}
