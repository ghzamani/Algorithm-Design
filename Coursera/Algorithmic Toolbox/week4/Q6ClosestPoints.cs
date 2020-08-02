using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace A5
{
    public class Q6ClosestPoints
    {
		//static void Main(string[] args)
		//{
		//}

		//List<long> xWithLessThandDist = new List<long>();
		//List<long> yWithLessThandDist = new List<long>();
		public virtual double Solve(long n, long[] xPoints, long[] yPoints)
		{
			throw new NotImplementedException();
		

		//	QuickSort(xPoints, yPoints, 0, xPoints.Length - 1);


		//	return Dist(xPoints, yPoints, 0, n - 1);
		//}

		//public double Dist(long[] x, long[] y, long l, long r)
		//{
			
		//	if (r - l == 1)
		//	{
		//		return CalculateDist(x[0], y[0], x[1], y[1]);
		//	}

		//	long mid = (l + r) / 2;
		//	double dLeft = Dist(x,y, l, mid);
		//	double dRight = Dist(x, y , mid, r);
		//	double minInParts = Math.Min(dLeft, dRight);

		//	for (long i = l; i < r; i++)
		//	{
		//		if (Math.Abs(x[mid] - x[i]) < mid)
		//		{
		//			xWithLessThandDist.Add(x[i]);
		//			yWithLessThandDist.Add(y[i]);
		//		}
		//	}

		//	QuickSort(yWithLessThandDist.ToArray(), xWithLessThandDist.ToArray(), 0, yWithLessThandDist.Count - 1);
		//	FindMin(xWithLessThandDist.ToArray(), yWithLessThandDist.ToArray(), ref minInParts);
		//	return minInParts;
		}

		////distance with 4 number precision
		//public double CalculateDist(long x1,long y1, long x2, long y2)
		//{
		//	return Math.Truncate(Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)) * 10_000) / 10_000;
		//}



	}
}