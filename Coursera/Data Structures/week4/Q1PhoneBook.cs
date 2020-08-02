using System;
using System.Linq;
using System.Collections.Generic;

namespace A10
{
	public class Q1PhoneBook
	{
		//public static void Main(string[] args)
		//{
		//	int n = int.Parse(Console.ReadLine());
		//	string[] commands = new string[n];

		//	for (int i = 0; i < n; i++)
		//	{
		//		commands[i] = Console.ReadLine();
		//	}
		//	string[] result = Solve(commands);
		//	foreach(var r in result)
		//		Console.WriteLine(r);
		//}
		static Dictionary<int, string> PhoneBookDic;

		public static string[] Solve(string[] commands)
		{
			PhoneBookDic = new Dictionary<int, string>();
			List<string> result = new List<string>();
			foreach (var cmd in commands)
			{
				var toks = cmd.Split();
				var cmdType = toks[0];
				var args = toks.Skip(1).ToArray();
				int number = int.Parse(args[0]);
				switch (cmdType)
				{
					case "add":
						Add(args[1], number);
						break;
					case "del":
						Delete(number);
						break;
					case "find":
						result.Add(Find(number));
						break;
				}
			}
			return result.ToArray();
		}
		public static void Add(string name, int number)
		{
			if (!PhoneBookDic.ContainsKey(number))
				PhoneBookDic.Add(number, name);

			else PhoneBookDic[number] = name;
		}

		public static string Find(int number)
		{
			if (PhoneBookDic.ContainsKey(number))
				return PhoneBookDic[number];

			return "not found";
		}

		public static void Delete(int number)
		{
			if (PhoneBookDic.ContainsKey(number))
				PhoneBookDic.Remove(number);
		}
	}
}
