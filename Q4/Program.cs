using System;
using System.Collections.Generic;

namespace Q4
{
	public class Program
	{
		static void Main(string[] args)
		{
			Random rnd = new Random(10);
			int subgraph1 = rnd.Next(3, 8);
			int subgraph2 = rnd.Next(3, 8);
			int subgraph3 = rnd.Next(3, 8);
			//returns list of edges
			//graph is undirected
			List<Tuple<int, int>> graph = new List<Tuple<int, int>>();

			Func<int, int, List<Tuple<int, int>>> f1 = FirstModel;
			Func<int, int, List<Tuple<int, int>>> f2 = SecondModel;
			Func<int, int, List<Tuple<int, int>>> f3 = ThirdModel;
			List<Func<int,int, List<Tuple<int, int>>>> functions = new List<Func<int,int, List<Tuple<int, int>>>>
			{
				f1,f2,f3
			};

			int start = 0;
			int end = subgraph1 - 1;
			graph.AddRange(functions[rnd.Next() % 3](start, end));
			Console.WriteLine("first subgraph edges:");
			foreach(var edge in graph)
			{
				Console.WriteLine(edge.Item1 + " " + edge.Item2);
			}

			start = end + 1;
			end += subgraph2;
			int idx = graph.Count;
			graph.AddRange(functions[rnd.Next() % 3](start, end));
			Console.WriteLine("second subgraph edges:");
			for (int i = idx; i < graph.Count; i++)
			{
				Console.WriteLine(graph[i].Item1 + " " + graph[i].Item2);
			}

			start = end + 1;
			end += subgraph3;
			idx = graph.Count;
			graph.AddRange(functions[rnd.Next() % 3](start, end));
			Console.WriteLine("third subgraph edges:");
			for (int i = idx; i < graph.Count; i++)
			{
				Console.WriteLine(graph[i].Item1 + " " + graph[i].Item2);
			}

		}

		public static List<Tuple<int,int>> FirstModel (int start, int end)
		{
			List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

			for (int i = start; i < end; i++)
			{
				for (int j = i + 1; j <= end; j++)
				{
					edges.Add(new Tuple<int, int>(i, j));
				}
			}

			return edges;
		}

		public static List<Tuple<int, int>> SecondModel(int start, int end)
		{
			List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

			for (int i = start; i < end; i++)
			{
				for (int j = i + 2; j <= end; j += 2)
				{
					edges.Add(new Tuple<int, int>(i, j));
				}
			}
			return edges;
		}

		public static List<Tuple<int, int>> ThirdModel(int start, int end)
		{
			List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

			for (int i = start; i < end; i++)
			{
				for (int j = i + 1; j <= end; j += 2)
				{
					edges.Add(new Tuple<int, int>(i, j));
				}
			}

			return edges;
		}

	}
}
