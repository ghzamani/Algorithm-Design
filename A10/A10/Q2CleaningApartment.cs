using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public String[] Solve(int V, int E, long[,] matrix)
        {
			int[,] adjMatrix = new int[V, V];
			for (int i = 0; i < E; i++)
			{
				long u = matrix[i, 0] - 1;
				long v = matrix[i, 1] - 1;

				adjMatrix[u, v] = adjMatrix[v, u] = 1;
			}

			List<string> CNF = new List<string>();
			CNF.Add(string.Empty);

			int[][] variableNums = new int[V][];
			for (int i = 0; i < V; i++)
			{
				variableNums[i] = Enumerable.Range(i * V + 1, V).ToArray();
			}

			//each node has V positions in hamiltionian path (V variables for each node)
			//for each node, exactly one of the variables must be true
			for (int i = 0; i < V; i++)
			{
				int[] nums = variableNums[i];
				CNF.Add(string.Join(" ", nums));

				for (int j = 0; j < V - 1; j++)
				{
					for (int k = j + 1; k < V; k++)
					{
						CNF.Add($"-{nums[j]} -{nums[k]}");
					}
				}
			}

			//exactly one of i, i+v, i+2v, ...
			for (int i = 0; i < V; i++)
			{
				int[] nums = new int[V];
				for (int j = 0; j < V; j++)
				{
					nums[j] = variableNums[j][i];
				}
				CNF.Add(string.Join(" ", nums));

				for (int j = 0; j < V - 1; j++)
				{
					for (int k = j + 1; k < V; k++)
					{
						CNF.Add($"-{nums[j]} -{nums[k]}");
					}
				}
			}

			//چک کردن قطر اصلی به بالای ماتریس مجاورت
			for (int i = 0; i < V; i++)
			{
				for (int j = i; j < V; j++)
				{
					//Nonadjacent nodes i and j cannot be adjacent in the path
					if (adjMatrix[i,j] == 0)
					{
						int[] nums1 = variableNums[i];
						int[] nums2 = variableNums[j];

						//x^ik , x^j(k+1) can't be true at the same time
						for (int k = 0; k < V - 1; k++)
						{
							CNF.Add($"-{nums1[k]} -{nums2[k + 1]}");
							CNF.Add($"-{nums2[k]} -{nums1[k + 1]}");
						}
					}
				}
			}

			CNF[0] = $"{V} {CNF.Count}";
			return CNF.ToArray();
        }

    }
}
