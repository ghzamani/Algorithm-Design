using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A8
{
    public class Q1CheckBrackets
    {
		//public static void Main (string[] args)
		//{
		//	string str = Console.ReadLine();
		//	long result = Solve(str);

		//	if(result == -1)
		//	{
		//		Console.WriteLine("Success");
		//		return;
		//	}
		//	Console.WriteLine(result);
		//}
        public static long Solve(string str)
        {
			char[] openingChars = new char[] { '[',  '(',  '{' };
			char[] closingChars = new char[] { ']', ')', '}' };

			List<Tuple<char, int>> brackets = new List<Tuple<char, int>>();
			//filing the "brackets" list. it's a list only containing brackets.
			for(int i = 0; i < str.Length; i++)
			{
				if(openingChars.Contains(str[i]) ||
					closingChars.Contains(str[i]))
					brackets.Add(new Tuple<char, int>(str[i], i));				
			}

			Stack<Tuple<char,int>> stack = new Stack<Tuple<char, int>>();			

			for(int i = 0; i < brackets.Count; i++)
			{
				if (openingChars.Contains(brackets[i].Item1))				
					stack.Push(brackets[i]);
				
				else
				{
					if (stack.Count == 0)
						return brackets[i].Item2 + 1;

					else
					{
						char top = stack.Pop().Item1;
						for (int j = 0; j < 3; j++)
							if ((top == openingChars[j]) &&
								(brackets[i].Item1 != closingChars[j]))
								return brackets[i].Item2 + 1;
					}
				}
			}

			if(stack.Count == 1)
				return stack.Pop().Item2 + 1;			

			return -1;
        }
    }
}
