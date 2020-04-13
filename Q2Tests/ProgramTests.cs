using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.Tests
{
	[TestClass()]
	public class ProgramTests
	{
		[TestMethod()]
		public void MatchesTest()
		{
			string input1 = "hi my name is alis and i live is alisis";
			string pattern1 = "alis";
			int[] arr1 = new int[2] { 14, 33 };
			CollectionAssert.AreEqual(arr1, Program.Matches(input1, pattern1, Program.MakeArray(pattern1)));
			
			string input2 = "aaaaaa";
			string pattern2 = "aaa";
			int[] arr2 = new int[4] { 0, 1, 2, 3 };
			CollectionAssert.AreEqual(arr2, Program.Matches(input2, pattern2, Program.MakeArray(pattern2)));

			string input3 = "abbdbababa";
			string pattern3 = "df";
			int[] arr3 = new int[1] {-1};
			CollectionAssert.AreEqual(arr3, Program.Matches(input3, pattern3, Program.MakeArray(pattern3)));
		}
	}
}