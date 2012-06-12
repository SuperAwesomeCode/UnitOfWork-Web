namespace System
{
	/// <summary>
	/// 	Extension methods for string.
	/// </summary>
	public static class BoolExtensions
	{
		/// <summary>
		/// Nullable bool to Y or N or string.Empty.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string ToYesNoFlag(this bool? value)
		{
			return !value.HasValue ? string.Empty : value.Value.ToYesNoFlag();
		}

		/// <summary>
		/// Bool to Y or N.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string ToYesNoFlag(this bool value)
		{
			return value ? "Y" : "N";
		}
	}
}