using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
    public class Q2BoardGame : Processor
    {
        public Q2BoardGame(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, long[,], string[]>)Solve);

        public string[] Solve(int BoardSize, long[,] Board)
        {
			long[,][] vars = new long[BoardSize, BoardSize][];
			for (int i = 0; i < BoardSize; i++)
			{
				for (int j = 0; j < BoardSize; j++)
				{
					vars[i, j] = VariableNums(i, j, BoardSize);
				}
			}

			List<string> CNF = new List<string>();
			CNF.Add(string.Empty);

			//define 3 bool for each square
			for (int row = 0; row < BoardSize; row++)
			{
				for (int col = 0; col < BoardSize; col++)
				{
					//each square exactly one of(empty, blue, red)
					long[] nums = VariableNums(row, col, BoardSize);
					CNF.Add(string.Join(" ", nums));
					for (int i = 0; i < 2; i++)
					{
						for (int j = i + 1; j < 3; j++)
						{
							CNF.Add($"-{nums[i]} -{nums[j]}");
						}
					}
				}
			}
			
			//در هر سطر حداقل یکی از مهره ها باید بماند
			//اگر سطری از اول خالی بود -> unsatis
			for (int i = 0; i < BoardSize; i++)
			{
				bool emptyRow = true;
				List<long> nums = new List<long>();
				for (int j = 0; j < BoardSize; j++)
				{
					if (Board[i,j] != 1) //not empty
					{
						emptyRow = false;
						nums.Add(vars[i, j][Board[i, j] - 1]);
					}
				}
				CNF.Add(string.Join(" ", nums));
				if (emptyRow)
					return new string[5] { "2 4", "1 2", "-1 -2", "1 -2", "-1 2" };
			}

			//در هر ستون حداقل یکی از مهره ها باید بماند
			for (int j = 0; j < BoardSize; j++)
			{
				bool emptyCol = true;
				List<long> nums = new List<long>();
				List<long> blues = new List<long>();
				List<long> reds = new List<long>();

				for (int i = 0; i < BoardSize; i++)
				{
					if (Board[i, j] != 1) //not empty
					{
						emptyCol = false;
						nums.Add(vars[i, j][Board[i, j] - 1]);

						if (vars[i, j][Board[i, j] - 1] % 3 == 0)
							reds.Add(vars[i, j][Board[i, j] - 1]);
						else blues.Add(vars[i, j][Board[i, j] - 1]);
					}
				}

				if (emptyCol)
					return new string[5] { "2 4", "1 2", "-1 -2", "1 -2", "-1 2" };

				CNF.Add(string.Join(" ", nums));

				//مهره های درون هر ستون همرنگ
				for (int i = 0; i < blues.Count; i++)
				{
					for (int k = 0; k < reds.Count; k++)
					{
						//at most one of
						CNF.Add($"-{blues[i]} -{reds[k]}");
					}
				}
			}

			CNF[0] = $"{BoardSize * BoardSize * 3} {CNF.Count}";
			return CNF.ToArray();
        }
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

		public static long[] VariableNums(int i,int j, int boardSize)
		{
			long[] nums = new long[3];
			nums[0] = i * boardSize * 3 + j * 3 + 1;
			nums[1] = nums[0] + 1;
			nums[2] = nums[1] + 1;

			return nums;
		}
    }
}
