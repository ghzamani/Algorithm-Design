using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q3AdBudgetAllocation : Processor
    {
        public Q3AdBudgetAllocation(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
			List<string> CNF = new List<string>();
			CNF.Add(string.Empty);

			for (int i = 0; i < eqCount; i++)
			{
				List<int> nonZeroIndexes = new List<int>();
				for (int j = 0; j < varCount; j++)
				{
					if (A[i][j] != 0)
						nonZeroIndexes.Add(j);
				}

				if (nonZeroIndexes.Count == 0)
					continue;

				string str = $"{nonZeroIndexes[0] + 1}";
				//when variable is 0 -> +variable should be added to cnf
				//when variable is 1 -> -variable should be added to cnf
				switch (nonZeroIndexes.Count)
				{
					case 1:
						for (int x = 0; x < 2; x++)
						{
							if(A[i][nonZeroIndexes[0]] * x > b[i])
							{
								CNF.Add(str);
							}
							str = $"-{nonZeroIndexes[0] + 1}";
						}
						break;

					case 2:						
						for (int x = 0; x < 2; x++)
						{
							string str2 = $" {nonZeroIndexes[1] + 1}";
							for (int y = 0; y < 2; y++)
							{
								if(A[i][nonZeroIndexes[0]] * x + A[i][nonZeroIndexes[1]] * y > b[i])
								{
									CNF.Add(str + str2);
								}
								str2 = $" -{nonZeroIndexes[1] + 1}";
							}
							str = $"-{nonZeroIndexes[0] + 1}";
						}
						break;

					case 3:
						for (int x = 0; x < 2; x++)
						{
							string str2 = $" {nonZeroIndexes[1] + 1}";
							for (int y = 0; y < 2; y++)
							{
								string str3 = $" {nonZeroIndexes[2] + 1}";
								for (int z = 0; z < 2; z++)
								{
									if (A[i][nonZeroIndexes[0]] * x + A[i][nonZeroIndexes[1]] * y + A[i][nonZeroIndexes[2]] * z > b[i])
									{
										CNF.Add(str + str2 + str3);
									}
									str3 = $" -{nonZeroIndexes[2] + 1}";
								}
								str2 = $" -{nonZeroIndexes[1] + 1}";
							}
							str = $"-{nonZeroIndexes[0] + 1}";
						}
						break;
				}
			}

			CNF[0] = $"{varCount} {CNF.Count + 2}";
			return CNF.ToArray();
        }
    }
}
