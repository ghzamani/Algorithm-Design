using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q1GeneticMutation : Processor
    {
        public Q1GeneticMutation(string testDataName) : base(testDataName) { }
        public override string Process(string inStr)
            => TestTools.Process(inStr, (Func<string, string, string>)Solve);


        static int no_of_chars = 256;

        public string Solve(string firstDNA, string secondDNA)
        {
			for (int i = 0; i < firstDNA.Length; i++)
			{
				string str = string.Empty;
				str += firstDNA.Substring(i);
				str += firstDNA.Substring(0, firstDNA.Length - str.Length);

				if (str == secondDNA)
					return "1";
			}
			return "-1";
        }
    }
}
