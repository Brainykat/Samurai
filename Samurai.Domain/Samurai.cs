﻿using Samurai.Domain.ValueObjects;
using System.Collections.Generic;

namespace Samurai.Domain
{
	public class Samurai
	{
		public Samurai()
		{
			Quotes = new List<Quote>();
			SecretIdentity = new SecretIdentity();
			SamuraiBattles = new List<SamuraiBattle>();
		}
		public int Id { get; set; }
		public Name Name { get; set; }
		public Money Salary { get; set; }
		public List<Quote> Quotes { get; set; }
		public List<SamuraiBattle> SamuraiBattles { get; set; }
		public SecretIdentity SecretIdentity { get; set; }
	}
}
