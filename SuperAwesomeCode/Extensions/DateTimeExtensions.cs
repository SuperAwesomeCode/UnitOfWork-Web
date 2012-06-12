namespace System
{
	/// <summary>
	/// 	Extension methods involving DateTime.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// 	Converts the DateTime to a string of format "yyyy.MM.dd-HH.mm.ss".
		/// </summary>
		/// <param name="value"> DateTime to convert. </param>
		/// <returns> Converted string value. </returns>
		public static string ToFolderPath(this DateTime value)
		{
			return value.ToString("yyyy.MM.dd-HH.mm.ss");
		}

		/// <summary>
		/// 	Converts the DateTime to a string of format "yyyy_MM_dd-HH_mm_ss".
		/// </summary>
		/// <param name="value"> DateTime to convert. </param>
		/// <returns> Converted string value. </returns>
		public static string ToFileStamp(this DateTime value)
		{
			return value.ToString("yyyy_MM_dd-HH_mm_ss");
		}

		/// <summary>
		/// 	Gets the last second of the last day of a year and month.
		/// </summary>
		/// <param name="year"> Year to use when creating the DateTime. </param>
		/// <param name="month"> Month to use when creating the DateTime. </param>
		/// <returns> The last second of the last day of a year and month. </returns>
		public static DateTime GetLastSecondOfMonth(int year, int month)
		{
			return new DateTime(year, month, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
		}

		/// <summary>
		/// Gets the last second of the last day of a year and month.
		/// </summary>
		/// <param name="value">The value to use.</param>
		/// <returns></returns>
		public static DateTime GetLastSecondOfMonth(this DateTime value)
		{
			return GetLastSecondOfMonth(value.Year, value.Month);
		}
	}
}