using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
			List<string> CNF = new List<string>();
			CNF.Add(string.Empty);

			//boolean variable number of each node
			long[][] variableNums = new long[V][];
			//for (int i = 0; i < V; i++)
			//{
			//}

			//define 3 bool for each node
			//then for each node -> exactly one color
			for (int i = 0; i < V; i++)
			{
				long tmp = 3 * i;
				variableNums[i] = new long[3] { tmp + 1, tmp + 2, tmp + 3 };

				long[] nums = variableNums[i];

				//at least one color
				CNF.Add(string.Join(" ", nums));

				//at most one color
				for (int j = 0; j < nums.Length - 1; j++)
				{
					for (int k = j + 1; k < nums.Length; k++)
					{
						CNF.Add($"-{nums[j]} -{nums[k]}");
					}
				}
			}

			//nodes that have edge between them, must have different colors
			for (int i = 0; i < E; i++)
			{
				long u = matrix[i, 0] - 1;
				long v = matrix[i, 1] - 1;

				long[] nums1 = variableNums[u];
				long[] nums2 = variableNums[v];

				//can have at most one of each color
				for (int j = 0; j < nums1.Length; j++)
				{
					CNF.Add($"-{nums1[j]} -{nums2[j]}");
				}
			}

			CNF[0] = $"{V} {CNF.Count}";
			return CNF.ToArray();
		}

		public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }
}
