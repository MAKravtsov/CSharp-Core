using System;
using System.Collections.Generic;
using System.Linq;

namespace TestCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

			var a = new int[] { 1, 2, 5, 3, 3, 2 };

			var b = MyDistinct(a);

			Console.WriteLine(b[0]);
		}

		/*
		public static int[] MyDistinct(int[] src)
		{
			if (src == null)
				return null;

			var result = new List<int>();

			foreach (var s in src)
			{
				if (result.Contains(s))
					continue;

				result.Add(s);
			}

			return result.ToArray();
		}
		*/

		public static IEnumerable<T> FilterLast<T>(IEnumerable<T> source, int n)
        {
			var cnt = source.Count();
			var counter = 0;
			foreach(var s in source)
            {
				if (counter > cnt - n)
					break;

				yield return s;
				counter++;
            }
        }
	}
}