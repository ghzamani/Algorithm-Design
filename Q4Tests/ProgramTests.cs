using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q4.Tests
{
	[TestClass()]
	public class ProgramTests
	{
		[TestMethod()]
		public void FirstModelTest()
		{
			int start = 0;
			int end = 5;
			List<Tuple<int, int>> edges = new List<Tuple<int, int>>
			{
				new Tuple<int, int>(0,1),
				new Tuple<int, int>(0,2),
				new Tuple<int, int>(0,3),
				new Tuple<int, int>(0,4),
				new Tuple<int, int>(0,5),
				new Tuple<int, int>(1,2),
				new Tuple<int, int>(1,3),
				new Tuple<int, int>(1,4),
				new Tuple<int, int>(1,5),
				new Tuple<int, int>(2,3),
				new Tuple<int, int>(2,4),
				new Tuple<int, int>(2,5),
				new Tuple<int, int>(3,4),
				new Tuple<int, int>(3,5),
				new Tuple<int, int>(4,5)
			};

			CollectionAssert.AreEqual(Program.FirstModel(start, end), edges);
		}

		[TestMethod()]
		public void SecondModelTest()
		{
			int start = 0;
			int end = 5;
			List<Tuple<int, int>> edges = new List<Tuple<int, int>>
			{
				new Tuple<int, int>(0,2),
				new Tuple<int, int>(0,4),				
				new Tuple<int, int>(1,3),
				new Tuple<int, int>(1,5),
				new Tuple<int, int>(2,4),
				new Tuple<int, int>(3,5)
			};

			int start2 = 4;
			int end2 = 8;
			List<Tuple<int, int>> edges2 = new List<Tuple<int, int>>
			{
				new Tuple<int, int>(4,6),
				new Tuple<int, int>(4,8),
				new Tuple<int, int>(5,7),
				new Tuple<int, int>(6,8)
			};

			CollectionAssert.AreEqual(Program.SecondModel(start, end), edges);
			CollectionAssert.AreEqual(Program.SecondModel(start2, end2), edges2);
		}

		[TestMethod()]
		public void ThirdModelTest()
		{
			int start = 0;
			int end = 5;
			List<Tuple<int, int>> edges = new List<Tuple<int, int>>
			{
				new Tuple<int, int>(0,1),
				new Tuple<int, int>(0,3),
				new Tuple<int, int>(0,5),
				new Tuple<int, int>(1,2),
				new Tuple<int, int>(1,4),
				new Tuple<int, int>(2,3),
				new Tuple<int, int>(2,5),
				new Tuple<int, int>(3,4),
				new Tuple<int, int>(4,5)
			};

			int start2 = 4;
			int end2 = 8;
			List<Tuple<int, int>> edges2 = new List<Tuple<int, int>>
			{
				new Tuple<int, int>(4,5),
				new Tuple<int, int>(4,7),
				new Tuple<int, int>(5,6),
				new Tuple<int, int>(5,8),
				new Tuple<int, int>(6,7),
				new Tuple<int, int>(7,8)
			};

			CollectionAssert.AreEqual(Program.ThirdModel(start, end), edges);
			CollectionAssert.AreEqual(Program.ThirdModel(start2, end2), edges2);
		}
	}
}