﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Samurai.Domain
{
	public class Battle
	{
		public Battle()
		{
			SamuraiBattles = new List<SamuraiBattle>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<SamuraiBattle> SamuraiBattles { get; set; }
	}
}
