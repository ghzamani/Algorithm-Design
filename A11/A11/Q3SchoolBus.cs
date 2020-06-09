using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11
{
    public class Q3SchoolBus : Processor
    {
        public Q3SchoolBus(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)=>
        TestTools.Process(inStr, (Func<long, long[][], Tuple<long, long[]>>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.TSPVerifier;

		public virtual Tuple<long, long[]> Solve(long nodeCount, long[][] edges)
        {
			if (nodeCount == 2)
				return new Tuple<long, long[]>(edges[0][2] * 2, new long[2] { 1, 2 });
					   			 
			long[,] graph = new long[nodeCount, nodeCount];
			for (int i = 0; i < edges.Length; i++)
			{
				long u = edges[i][0] - 1;
				long v = edges[i][1] - 1;
				long w = edges[i][2];

				graph[u, v] = w;
				graph[v, u] = w;
			}
					   			 
			Subset[] subsets = new Subset[(int)Math.Pow(2, nodeCount) / 2];

			for (int i = 0; i < subsets.Length; i++)
			{
				//node 0 must be in all of the subsets in TSP
				//so skip the subsets that their decimal number is even

				int[] binaryDigits = Convert.ToString(2 * i + 1, 2).Select(d => int.Parse(d.ToString())).ToArray();

				List<long> members = new List<long>();
				for (int j = binaryDigits.Length - 1; j >= 0; j--)
					if(binaryDigits[j] == 1)
						members.Add(binaryDigits.Length - j - 1);
				subsets[i] = new Subset(members, 2 * i + 1);
			}
			
			var groupedSubsets = subsets.GroupBy(x => x.members.Count()).ToList();

			//groupSubsets[0].key = 1 -> subset = {0}
			subsets[0].C[0] = 0;
			
			//for subsets with more than 1 member
			for (int k = 1; k < groupedSubsets.Count; k++)
			{
				foreach(var subsetsSizeS in groupedSubsets[k])
				{
					foreach(var i in subsetsSizeS.members)
					{
						if (i == 0)
							continue;
						foreach(var j in subsetsSizeS.members)
						{
							if (j == i || graph[i,j] == 0)
								continue;

							long index = (subsetsSizeS.decimalNum - (long)Math.Pow(2, i)) / 2;		
							if(subsets[index].C[j] + graph[i, j] < subsetsSizeS.C[i])
							{
								subsetsSizeS.C[i] = subsets[index].C[j] + graph[i, j];
								subsetsSizeS.previousVisited[i] = j;
							}
						}
					}
				}
			}

			long dist = int.MaxValue;
			long node = 0;
			foreach(var dists in subsets.Last().C)
			{
				if (graph[dists.Key, 0] == 0)
					continue;

				if(dists.Value + graph[dists.Key,0] < dist)
				{
					dist = dists.Value + graph[dists.Key, 0];
					node = dists.Key;
				}
			}

			if (dist == int.MaxValue)
				return new Tuple<long, long[]>(-1, new long[0]);

			long[] path = new long[nodeCount];
			path[nodeCount - 1] = node + 1;

			long idx = nodeCount - 2;
			var lastSub = subsets.Last();
			while (idx >= 0)
			{
				var temp = lastSub.previousVisited[node];
				path[idx] = temp + 1;

				lastSub = subsets[(lastSub.decimalNum - (long)Math.Pow(2, node)) / 2];
				node = temp;
				idx--;
			}
			return new Tuple<long, long[]>(dist, path);
		}
	}

	public class Subset
	{
		public long decimalNum;
		public List<long> members;

		//key is end node
		//value is C
		public Dictionary<long, long> C;
		public Dictionary<long, long> previousVisited;
		public Subset(List<long> mems, long deciNum)
		{
			members = mems;
			decimalNum = deciNum;
			C = new Dictionary<long, long>();
			previousVisited = new Dictionary<long, long>();

			for (int i = 0; i < members.Count; i++)
			{
				C.Add(members[i], int.MaxValue);
				previousVisited.Add(members[i], -1);
			}
		}
	}
}
