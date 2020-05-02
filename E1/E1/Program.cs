using System;
using System.Drawing;
using System.Collections.Generic;

namespace Exam1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string path = args[0];
			int verticalSeamsCount = int.Parse(args[1]);
			int horizontalSeamsCount = int.Parse(args[2]);

			string[] result = Solve(new string[3] { path, verticalSeamsCount.ToString(), horizontalSeamsCount.ToString() });
		}

		public static string[] Solve(string[] data)
		{
			string imagePath = data[0];
			var img = Utilities.LoadImage(imagePath);
			var bmp = Utilities.ConvertImageToColorArray(img);

			//vertical removes
			int dimReduction = int.Parse(data[1]);
			char direction = 'V';
			var res = Solve(bmp, dimReduction, direction);

			//horizontal removes
			dimReduction = int.Parse(data[2]);
			direction = 'H';
			res = Solve(res, dimReduction, direction);

			Utilities.SavePhoto(res, imagePath, "../../../../asd", direction);
			return Utilities.ConvertColorArrayToRGBMatrix(res);
		}

		public static Color[,] Solve(Color[,] input, int reduction, char direction)
		{
			ColorsAndEnergies result = new ColorsAndEnergies();
			if (direction == 'H')
			{
				double[,] energies = FindEnergies(input);
				int[] horizontalSeam = HorizontalSeam(energies);
				result = RemoveSeam(input, energies, horizontalSeam, 'H');

				for (int i = 1; i < reduction; i++)
				{
					energies = FindEnergies(result.Colors);
					horizontalSeam = HorizontalSeam(energies);
					result = RemoveSeam(result.Colors, result.Energies, horizontalSeam, 'H');
				}
			}

			else //vertical direction
			{
				double[,] energies = FindEnergies(input);
				int[] verticalSeam = VerticalSeam(energies);
				result = RemoveSeam(input, energies, verticalSeam, 'V');

				for (int i = 1; i < reduction; i++)
				{
					energies = FindEnergies(result.Colors);
					verticalSeam = VerticalSeam(energies);
					result = RemoveSeam(result.Colors, result.Energies, verticalSeam, 'V');
				}
			}

			return result.Colors;
		}

		#region energies
		public static double[,] FindEnergies(Color[,] data)
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
		#endregion

		#region seams
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
		public static int MinIdxVertical(double[,] data) //returns the index of minimum of second row
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

		public static int MinIdxHorizontal(double[,] data) //returns index of minimum of second column
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
		#endregion

		#region removeSeams
		public static ColorsAndEnergies RemoveSeam(Color[,] colors, double[,] data, int[] removes, char direction)
		{
			long h = data.GetLength(0);
			long w = data.GetLength(1);
			double[,] result;
			Color[,] colorResult;

			if (direction == 'V')
			{
				result = new double[h, w - 1];
				colorResult = new Color[h, w - 1];
				for (int i = 0; i < h; i++)
				{
					int column = 0;
					for (int j = 0; j < w; j++)
					{
						if (j != removes[i])
						{
							result[i, column] = data[i, j];
							colorResult[i, column] = colors[i, j];
							column++;
						}
					}
				}
			}

			else
			{
				result = new double[h - 1, w];
				colorResult = new Color[h - 1, w];
				for (int j = 0; j < w; j++)
				{
					int row = 0;
					for (int i = 0; i < h; i++)
					{
						if (i != removes[j])
						{
							result[row, j] = data[i, j];
							colorResult[row, j] = colors[i, j];
							row++;
						}
					}
				}
			}

			return new ColorsAndEnergies(result, colorResult);
		}

		public class ColorsAndEnergies
		{
			public double[,] Energies;
			public Color[,] Colors;

			public ColorsAndEnergies() { }
			public ColorsAndEnergies(double[,] e, Color[,] c)
			{
				Energies = e;
				Colors = c;
			}
		}
		#endregion
	}
}

