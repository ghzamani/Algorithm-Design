using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Q3.Tests
{
	[TestClass()]
	public class ProgramTests
	{
		[TestMethod()]
		public void OccurrencesTest()
		{
			string text = "aabxaabxcaabxaabxay";
			string patern = "aabx";
			long[] results = { 0, 4, 9, 13 };

			List<long> res = Program.Search(text, patern).ToList();
			CollectionAssert.AreEqual(results, res);
		}
	}
}