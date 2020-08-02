using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    public class Q4SuffixTree 
    {
		//static void Main (string[] args)
		//{
		//	string text = Console.ReadLine();
		//	string[] result = Solve(text);

		//	foreach(var r in result)
		//		Console.WriteLine(r);
		//}
		public static List<string> result;
		public static string[] Solve(string text)
		{
			result = new List<string>();
			SuffixTree tree = new SuffixTree(text);

			for (int i = 1; i < tree.Nodes.Count; i++)
				result.Add(tree.Nodes[i].Label);
			
			return result.ToArray();
		}
		public class Node
		{
			public string Label;                     
			public List<int> Children = new List<int>();

			public Node() { }

			public Node(string subString, List<int> children)
			{
				this.Label = subString;
				Children.AddRange(children);
			}
		}

		public class SuffixTree
		{
			public List<Node> Nodes = new List<Node>();
			static int NodeCount;
			public SuffixTree(string str)
			{
				NodeCount = 1; //wherever a node was added to Nodes, this should be incremented
				Nodes.Add(new Node()); //root
				for (int i = 0; i < str.Length; i++)
					AddString(str.Substring(i));				
			}

			public void AddString(string str, int current = 0)
			{
				int? idx = CheckCurrentChildren(current, str); //index in Nodes list

				if (!idx.HasValue) //اشتراک دار پیدا نشده پس اد میکنیم
				{
					Node n = new Node(str,new List<int>());
					Nodes.Add(n);
					Nodes[current].Children.Add(NodeCount);
					NodeCount++;
					return;
				}

				else //استرینگ با یکی از بچه ها اشتراک دارد
				//(nodes[idx]) با
				{
					//should check they are common in how many letters
					current = idx.Value;
					string currentLabel = Nodes[current].Label;
					int lettersInCommon = CommonCharactersCount(idx.Value, str);

					if (lettersInCommon < currentLabel.Length)
					//در این حالت باید کارنت لیبل تکه شود
					{
						string currentPart2 = currentLabel.Substring(lettersInCommon); //قسمتی از کارنت لیبل که باید جدا شود
						//و به عنوان نود جدید به لیست اضافه شود
						Node node = new Node(currentPart2, Nodes[current].Children); //بچه های کارنت باید بچه های نود جدید شوند
						Nodes.Add(node);

						Nodes[current].Children = new List<int>(); //اکنون بچه های نود کارنت فقط شامل نود جدید میشود
						Nodes[current].Children.Add(NodeCount);
						NodeCount++;

						Nodes[current].Label = currentLabel.Substring(0, lettersInCommon); //currentPart2 باید از لیبل جدا شود											
					}

					AddString(str.Substring(lettersInCommon), current);
				}
				
			}

			int? CheckCurrentChildren(int key, string str) 
			//چک اینکه استرینگ با بچه های کارنت نود اشتراک دارد یا نه
			//اگر دارد با کدام نود
			//اگر ندارد نال ریترن میشود
			{
				foreach (var child in Nodes[key].Children)
					if (Nodes[child].Label[0] == str[0])
						return child; //return the child index in Nodes list

				return null; //string has nothing in common with children
			}

			int CommonCharactersCount(int key, string str)
			{
				string label = Nodes[key].Label;
				int i;
				for (i = 1; (i < str.Length) && (i<label.Length); i++)
				{
					if (label[i] != str[i])
						return i;
				}
				return i; //i is letters in common
			}
		}
	}    
}
