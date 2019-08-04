using System;
using System.Collections.Generic;

namespace Samurai.Domain.ValueObjects
{
	public class DateTimeRange
	{
		public static DateTimeRange Create(DateTime from, DateTime to)
		{
			return new DateTimeRange(from, to);
		}
		public bool Overlaps(DateTimeRange dateTimeRange)
		{
			return this.From < dateTimeRange.To &&
				this.To > dateTimeRange.From;
		}
		public override bool Equals(object obj)
		{
			return obj is DateTimeRange range &&
				   From == range.From &&
				   To == range.To;
		}
		public override int GetHashCode()
		{
			var hashCode = -1781160927;
			hashCode = hashCode * -1521134295 + From.GetHashCode();
			hashCode = hashCode * -1521134295 + To.GetHashCode();
			return hashCode;
		}
		public DateTime From { get; private set; }
		public DateTime To { get; private set; }
		private DateTimeRange() { }
		private DateTimeRange(DateTime from, DateTime to)
		{
			if (to >= from)
			{
				throw new ArgumentOutOfRangeException(nameof(DateTimeRange), "Date Range is Invalid");
			}
			From = from;
			To = to;
		}
		public static bool operator ==(DateTimeRange left, DateTimeRange right)
		{
			return EqualityComparer<DateTimeRange>.Default.Equals(left, right);
		}
		public static bool operator !=(DateTimeRange left, DateTimeRange right)
		{
			return !(left == right);
		}
	}
	public static class DateTimeRangeExtensions
	{
		public static DateTime GetDate() => DateTime.UtcNow;
		public static bool Overlaps(this DateTimeRange dateTimeRange, DateTimeRange oldDateTimeRange)
		{
			return oldDateTimeRange.From < dateTimeRange.To &&
				oldDateTimeRange.To > dateTimeRange.From;
		}
		public static DateTimeRange CreateHoursRange(this DateTime from, double hours)
		{
			return DateTimeRange.Create(from, from.AddHours(hours));
		}
		public static DateTimeRange CreateDaysRange(this DateTime from, double days)
		{
			return DateTimeRange.Create(from, from.AddDays(days));
		}
		public static DateTimeRange CreateWeeksRange(this DateTime from, double weeks)
		{
			return DateTimeRange.Create(from, from.AddDays(weeks * 7));
		}
		public static DateTimeRange CreateMonthRange(this DateTime from, int months)
		{
			return DateTimeRange.Create(from, from.AddMonths(months));
		}
		public static DateTimeRange DaySpan(this DateTime date)
		{
			return DateTimeRange.Create(date.AddHours(0D).AddMinutes(0D).AddSeconds(0D), date.AddHours(23D).AddMinutes(59D).AddSeconds(59D));
		}
		public static DateTimeRange MonthSpan(this DateTime date)
		{
			DateTime Fist_date = Convert.ToDateTime(date.Month + "-" + 1 + "-" + date.Year);
			DateTime Last_date = Convert.ToDateTime(date.Month + "-" + DateTime.DaysInMonth(date.Year, date.Month) + "-" + date.Year).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
			return DateTimeRange.Create(Fist_date, Last_date);
		}
		public static IEnumerable<DateTimeRange> MonthRanges(this DateTime date)
		{
			for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
			{
			  yield return DaySpan(Convert.ToDateTime(date.Month + "-" + i + "-" + date.Year));
			}
		}
		public static int DayDifference(this DateTimeRange dateTimeRange)
		{
			return (dateTimeRange.To - dateTimeRange.From).Days;
		}
		public static int MonthDifference(this DateTimeRange dateTimeRange)
		{
			return dateTimeRange.To.Month - dateTimeRange.From.Month;
		}
		public static int HourDifference(this DateTimeRange dateTimeRange)
		{
			return (dateTimeRange.To - dateTimeRange.From).Hours;
		}
		public static string ToDateRangeString(this DateTimeRange dateTimeRange)
		{
			return $"From: {ToDateString(dateTimeRange.From)} To: {ToDateString(dateTimeRange.To)}"; // " base.ToString();
		}
		public static string ToDateString(this DateTime date)
		{
			return date.ToString("dd-mm-yy");
		}
		public static int Age(this DateTime date)
		{
			if (date >= GetDate()) throw new ArgumentOutOfRangeException(nameof(date));
			return GetDate().Year - date.Year;
		}
	}
}
