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
			int y = rows.Length;
			int x = rows[0].Split('|').Length;
			Color[,] data = new Color[x,y];

			for (int i = 0; i < y; i++)
			{
				var column = rows[i].Split('|');
				for(int j = 0; j < x; j++)
				{
					var colors = column[j].Split(',');
					data[j, i] = Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
				}
			}
            var solved = Solve(data);
			string result = string.Empty;
			for (int i = 0; i < rows.Length; i++)
			{
				for (int j = 0; j < rows[0].Split('|').Length; j++)
				{
					result += solved[i, j];
				}
				result += "\n";
			}
            // convert solved into output string

            return result;
        }
            

        public double[,] Solve(Color[,] data)
        {
			long w = data.GetLength(0);
			long h = data.GetLength(1);
			double[,] energies = new double[w, h];
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					if (j == 0 || j == h - 1 || i == 0 || i == w - 1)
					{
						energies[i, j] = 1000;
						continue;
					}
					double deltaX2 = Math.Pow(Delta(data, i, j, w, h, "x"), 2);
					double deltaY2 = Math.Pow(Delta(data, i, j, w, h, "y"), 2);
					energies[i, j] = Math.Sqrt(deltaX2 + deltaY2);
				}
			}

			return energies;
        }
		static double Delta(Color[,] data, int x, int y, long w, long h, string direction)
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
		static double R (Color[,] data, int x, int y, long w, long h, string direction)
		{
			//if (x == 0 || x == w -1 || y == 0 || y == h -1)
			//	return 1000;

			if(direction == "x")
				return data[x + 1, y].R - data[x - 1, y].R;

			return data[x, y + 1].R - data[x, y - 1].R;
		}

		static double G(Color[,] data, int x, int y, long w, long h, string direction)
		{
			//if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
			//	return 1000;

			if (direction == "x")
				return data[x + 1, y].G - data[x - 1, y].G;

			return data[x, y + 1].G - data[x, y - 1].G;
		}

		static double B(Color[,] data, int x, int y, long w, long h, string direction)
		{
			//if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
			//	return 1000;

			if (direction == "x")
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
            double[,] data = new double[0, 0];
            var solved = Solve(data);
            // convert solved into output string
            return string.Empty;
        }


        public int[] Solve(double[,] data)
        {
            throw new NotImplementedException();
        }
    }

    public class Q3SeamCarving3 : Processor // Remove Seam
    {
        public Q3SeamCarving3(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            // Parse input file
            double[,] data = new double[0, 0];
            var solved = Solve(data);
            // convert solved into output string
            return string.Empty;
        }


        public double[,] Solve(double[,] data)
        {
            throw new NotImplementedException();
        }
    }
}
