using System;
using System.Collections.Generic;
using System.Text;

namespace Samurai.Domain
{
	public class Prime
	{
		public void PrimeOnes(int start, int end)
		{
			bool k = true;
			for (int i = start; i <= end; i++)
			{
				for (int j = start; j < i; j++)
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
