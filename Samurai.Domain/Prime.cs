using System;
using System.Collections.Generic;
using System.Text;

namespace Samurai.Domain
{
	public class Prime
	{
		public void PrimeOnes(int start, int end)
		{
			int n = 101; bool k = true;
			for (int i = 2; i <= n; i++)
			{
				for (int j = 2; j < i; j++)
				{
					if (i % j == 0)
					{
						k = false;
						break;
					}
				}
				if (k)
				{
					Console.WriteLine($"{i} is Prime");
				}
				k = true;
			}
		}
	}
}
