using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Solvers;
using TestCommon;

namespace A11
{
    public class Q4RescheduleExam : Processor
    {
        public Q4RescheduleExam(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, char[], long[][], char[]>) Solve);

        public override Action<string, string> Verifier =>
            TestTools.GraphColorVerifier;


		public List<long[]> cnf;
		public virtual char[] Solve(long nodeCount, char[] colors, long[][] edges)
        {
			//x1 red , x2 green, x3 blue
			//x1 x2 x3 node 1
			cnf = new List<long[]>();

			for (int i = 0; i < nodeCount; i++)
			{
				long tmp = 3 * i + 1;
				switch (colors[i])
				{
					case 'R': //should be exactly one of green or blue
						ExactlyOneOf(tmp + 1, tmp + 2);
						break;

					case 'G': //should be exactly one of red or blue
						ExactlyOneOf(tmp, tmp + 2);
						break;

					case 'B': //should be exactly one of green or red
						ExactlyOneOf(tmp, tmp + 1);
						break;
				}
			}

			for (int i = 0; i < edges.Length; i++)
			{
				long u = edges[i][0] * 3;
				long v = edges[i][1] * 3;

				AtMostOneOf(u, v);
				AtMostOneOf(u - 1, v - 1);
				AtMostOneOf(u - 2, v - 2);
			}

			Tuple<bool, long[]> assigned = new Q1CircuitDesign(" ").Solve(3 * nodeCount, cnf.Count, cnf.ToArray());

			if (!assigned.Item1)
				return "Impossible".ToCharArray();

			char[] assignedColors = new char[nodeCount];
			for (int i = 0; i < assigned.Item2.Length; i++)
			{
				if(assigned.Item2[i] > 0)
				{
					switch(assigned.Item2[i] % 3)
					{
						case 0:
							assignedColors[i / 3] = 'R';
							break;

						case 1:
							assignedColors[i / 3] = 'G';
							break;

						case 2:
							assignedColors[i / 3] = 'B';
							break;
					}
				}
			}
			return assignedColors;
        }

		public virtual void ExactlyOneOf(long x, long y)
		{
			cnf.Add(new long[2] { x, y });
			cnf.Add(new long[2] { -1 * x, -1 * y });
		}

		public virtual void AtMostOneOf(long x, long y)
		{
			cnf.Add(new long[2] { -1 * x, -1 * y });
		}
    }
}
