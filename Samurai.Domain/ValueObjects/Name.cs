namespace Samurai.Domain.ValueObjects
{
	public class Name
	{
		//Name Should not really be a value object as per my thoughts coz its not necessarily immutable
		public Name() { }
		public static Name Create(string sur, string first, string middle = null)
		{
			return new Name(sur, first, middle);
		}
		public static Name Empty()
		{
			return new Name(" ", " ");
		}
		private Name(string sur, string first, string middle = null)
		{
			Sur = sur; First = first; Middle = middle;
		}
		public string Sur { get; set; } 
		public string First { get; set; } 
		public string Middle { get; set; }
		public string FullName => $"{Sur} {First} {Middle}";
		public string FullNameReverse => $"{First} {Middle} {Sur}";
	}
}
