using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TestCommon;

namespace Exam1
{
	public class Q3SeamCarving1 : Processor // Calculate Energy
	{
		public Q3SeamCarving1(string testDataName) : base(testDataName) { }

		public override string Process(string inStr)
		{
			// Parse input file
			string[] rows = inStr.Split('\n');
			int x = rows.Length;
			int y = rows[0].Split('|').Length;
			Color[,] data = new Color[x, y];

			for (int i = 0; i < x; i++)
			{
				var column = rows[i].Split('|');
				for (int j = 0; j < y; j++)
				{
					var colors = column[j].Split(',');
					data[i, j] = Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
				}
			}
			var solved = Solve(data);
			string result = string.Empty;
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y - 1; j++)
				{
					result += solved[i, j] + ",";
				}
				result += solved[i, y - 1] + "\n";
			}
			// convert solved into output string
			return result;
		}

		public double[,] Solve(Color[,] data)
		{
			long h = data.GetLength(0);
			long w = data.GetLength(1);
			double[,] energies = new double[h, w];
			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					if (j == 0 || j == w - 1 || i == 0 || i == h - 1)
					{
						energies[i, j] = 1000;
						continue;
					}
					double deltaX2 = Delta2(data, i, j, w, h, "x");
					double deltaY2 = Delta2(data, i, j, w, h, "y");
					energies[i, j] = Math.Round(Math.Sqrt(deltaX2 + deltaY2), 3);
				}
			}

			return energies;
		}
		static double Delta2(Color[,] data, int x, int y, long w, long h, string direction)
		{
			double r;
			double g;
			double b;

			if (direction == "x")
			{
				r = R(data, x, y, w, h, "x");
				g = G(data, x, y, w, h, "x");
				b = B(data, x, y, w, h, "x");
				return r * r + g * g + b * b;
			}

			r = R(data, x, y, w, h, "y");
			g = G(data, x, y, w, h, "y");
			b = B(data, x, y, w, h, "y");
			return r * r + g * g + b * b;
		}
		static double R(Color[,] data, int x, int y, long w, long h, string direction)
		{
			if (direction == "y")
				return data[x + 1, y].R - data[x - 1, y].R;

			return data[x, y + 1].R - data[x, y - 1].R;
		}

		static double G(Color[,] data, int x, int y, long w, long h, string direction)
		{
			if (direction == "y")
				return data[x + 1, y].G - data[x - 1, y].G;

			return data[x, y + 1].G - data[x, y - 1].G;
		}

		static double B(Color[,] data, int x, int y, long w, long h, string direction)
		{
			if (direction == "y")
				return data[x + 1, y].B - data[x - 1, y].B;

			return data[x, y + 1].B - data[x, y - 1].B;
		}
	}

	public class Q3SeamCarving2 : Processor // Find Seam
	{
		public Q3SeamCarving2(string testDataName) : base(testDataName) { }

		public override string Process(string inStr)
		{
			// Parse input file
			string[] rows = inStr.Split('\n');
			int x = rows.Length;
			int y = rows[0].Split(',').Length;
			double[,] data = new double[x, y];

			for (int k = 0; k < x; k++)
			{
				var column = rows[k].Split(',');
				for (int j = 0; j < y; j++)
				{
					data[k, j] = double.Parse(column[j].Trim());
				}
			}

			var solved = Solve(data);
			string result = string.Empty;
			int i;
			for (i = 0; i < x - 1; i++)
			{
				result += solved[i] + ",";
			}
			result += solved[x - 1] + "\n";
			i++;
			for (; i < x + y - 1; i++)
			{
				result += solved[i] + ",";
			}
			result += solved[x + y - 1];
			// convert solved into output string
			return result;
		}


		public int[] Solve(double[,] data)
		{
			int[] vertical = VerticalSeam(data);
			int[] horizontal = HorizontalSeam(data);
			int[] seams = new int[vertical.Length + horizontal.Length];
			int k = 0;
			for (int i = 0; i < vertical.Length; i++)
			{
				seams[k] = vertical[i];
				k++;
			}
			for (int i = 0; i < horizontal.Length; i++)
			{
				seams[k] = horizontal[i];
				k++;
			}
			return seams;
		}
		public static int[] VerticalSeam(double[,] data)
		{
			long h = data.GetLength(0);
			long w = data.GetLength(1);
			int[] result = new int[h];

			int idx = MinIdxVertical(data);
			result[0] = result[1] = idx;
			int i = 2;
			while (i < h - 1)
			{
				idx = MinNeighborIdxVertical(data, i - 1, idx);
				result[i] = idx;
				i++;
			}
			result[h - 1] = idx;
			return result;
		}
		public static int MinIdxVertical(double[,] data) //minimum of second row
		{
			long w = data.GetLength(1);
			int index = 0;

			double min = data[1, 0];
			for (int i = 1; i < w; i++)
			{
				if (data[1, i] < min)
				{
					min = data[1, i];
					index = i;
				}
			}
			return index;
		}
		public static int MinNeighborIdxVertical(double[,] data, int i, int j)
		{
			double a = data[i + 1, j - 1];
			double b = data[i + 1, j];
			double c = data[i + 1, j + 1];

			int index = j - 1;
			if (b < a)
				index = j;
			else b = a;
			if (c < b)
				index = j + 1;
			return index;
		}

		public static int[] HorizontalSeam(double[,] data)
		{
			long h = data.GetLength(0);
			long w = data.GetLength(1);
			int[] result = new int[w];

			int idx = MinIdxHorizontal(data);
			result[0] = result[1] = idx;
			int j = 2;
			while (j < w - 1)
			{
				idx = MinNeighborIdxHorizontal(data, idx, j - 1);
				result[j] = idx;
				j++;
			}
			result[w - 1] = idx;
			return result;
		}

		public static int MinIdxHorizontal(double[,] data) //minimum of second column
		{
			long h = data.GetLength(0);
			int index = 0;

			double min = data[0, 1];
			for (int i = 1; i < h; i++)
			{
				if (data[i, 1] < min)
				{
					min = data[i, 1];
					index = i;
				}
			}
			return index;
		}
		public static int MinNeighborIdxHorizontal(double[,] data, int i, int j)
		{
			double a = data[i - 1, j + 1];
			double b = data[i, j + 1];
			double c = data[i + 1, j + 1];

			int index = i - 1;
			if (b < a)
				index = i;
			else b = a;
			if (c < b)
				index = i + 1;
			return index;
		}
	}

	public class Q3SeamCarving3 : Processor // Remove Seam
	{
		public Q3SeamCarving3(string testDataName) : base(testDataName) { }

		public override string Process(string inStr)
		{
			// Parse input file
			var input = inStr.Split('\n');
			int h = int.Parse(input[0]);
			int w = input[1].Split(',').Length;

			double[,] data = new double[h, w];
			int i = 0;
			while (i < h)
			{
				var row = input[i + 1].Split(',');
				for (int j = 0; j < row.Length; j++)
				{
					data[i, j] = double.Parse(row[j].Trim());
				}
				i++;
			}

			int n = int.Parse(input[++i].Trim());

			i++;
			var removesLine = input[i].Split(':', ',');
			int[] toRemoveIndexes = new int[removesLine.Length - 1];
			direction d = removesLine[0] == "h" ? direction.horizontal : direction.vertical;
			for (i = 1; i < removesLine.Length; i++)
			{
				toRemoveIndexes[i - 1] = int.Parse(removesLine[i].Trim());
			}

			var solved = Solve(data, toRemoveIndexes, d);

			h = solved.GetLength(0);
			w = solved.GetLength(1);

			string result = string.Empty;
			for (i = 0; i < h; i++)
			{
				for (int j = 0; j < w - 1; j++)
				{
					result += String.Format("{0:0.00}", solved[i, j]) + ",";
				}
				result += String.Format("{0:0.00}", solved[i, w - 1]) + "\n";
			}
			// convert solved into output string
			return result;
		}
		public enum direction
		{
			vertical,
			horizontal
		}

		public double[,] Solve(double[,] data, int[] removes, direction d)
		{
			long h = data.GetLength(0);
			long w = data.GetLength(1);
			double[,] result;


			if (d == direction.vertical)
			{
				result = new double[h, w - 1];
				for (int i = 0; i < h; i++)
				{
					int column = 0;
					for (int j = 0; j < w; j++)
					{
						if (j != removes[i])
						{
							result[i, column] = data[i, j];
							column++;
						}
					}
				}
			}

			else
			{
				result = new double[h - 1, w];
				for (int j = 0; j < w; j++)
				{
					int row = 0;
					for (int i = 0; i < h; i++)
					{
						if (i != removes[j])
						{
							result[row, j] = data[i, j];
							row++;
						}
					}
				}
			}

			return result;
		}
	}
}
