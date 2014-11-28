using System;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime GetBeginWeekDate(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
		{
			int diff = date.DayOfWeek - startOfWeek;
			if (diff < 0)
			{
				diff += 7;
			}

			return date.AddDays(-1 * diff).Date;
		}

		public static DateTime GetEndWeekDate(this DateTime date, DayOfWeek endOfWeek = DayOfWeek.Sunday)
		{
			int diff = endOfWeek - date.DayOfWeek;
			if (diff < 0)
			{
				diff += 7;
			}

			return date.AddDays(diff).Date;
		}

		public static DateTime GetBeginMonthDate(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime GetEndMonthDate(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
		}

		public static DateTime GetBeginQuarterDate(this DateTime date)
		{
			var quarterNum = (date.Month - 1) / 3;
			var beginMonth = 1 + (quarterNum * 3);

			return new DateTime(date.Year, beginMonth, 1);
		}

		public static DateTime GetEndQuarterDate(this DateTime date)
		{
			var quarterNum = (date.Month - 1) / 3;
			var endMonth = 1 + (quarterNum * 3) + 2;

			return new DateTime(date.Year, endMonth, DateTime.DaysInMonth(date.Year, endMonth));
		}

		public static DateTime GetBeginYearDate(this DateTime date)
		{
			return new DateTime(date.Year, 1, 1);
		}

		public static DateTime GetEndYearDate(this DateTime date)
		{
			return new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12));
		}
	}
}
