using System;

namespace SuperAwesomeCode
{
	/// <summary>Static Class used to as a helper agains invalid method parameters.</summary>
	public static class Guard
	{
		/// <summary>Throws an exception if the parameter is null.</summary>
		/// <param name="parameter">Parameter to check null against.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">Exception message to display if the parameter is null.</param>
		public static void AgainstNull(object parameter, string parameterName = null, string message = null)
		{
			if (parameter == null)
			{
				Guard.ThrowArgumentNullException(parameterName, message);
			}
		}

		/// <summary>Throws an exception if the parameter is null or empty.</summary>
		/// <param name="parameter">String parameter to check null or empty against.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">Exception message to display if the parameter is null or empty.</param>
		public static void AgainstNullOrEmpty(string parameter, string parameterName = null, string message = null)
		{
			if (string.IsNullOrEmpty(parameter))
			{
				Guard.ThrowArgumentNullException(parameterName, message);
			}
		}

		/// <summary>Throws an ArgumentNullException.</summary>
		/// <param name="parameterName">ParameterName to throw the exception with.</param>
		/// <param name="message">Message to throw the exception with.</param>
		public static void ThrowArgumentNullException(string parameterName = null, string message = null)
		{
			if (!string.IsNullOrEmpty(parameterName))
			{
				if (!string.IsNullOrEmpty(message))
				{
					throw new ArgumentNullException(parameterName, message);
				}

				throw new ArgumentNullException(parameterName);
			}
			else
			{
				if (!string.IsNullOrEmpty(message))
				{
					throw new ArgumentNullException(message, (Exception)null);
				}
			}

			//Must always throw an exception
			throw new ArgumentNullException();
		}
	}
}