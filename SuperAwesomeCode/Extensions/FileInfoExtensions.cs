using Microsoft.Win32;

namespace System.IO
{
	/// <summary>Extensions for FileInfo.</summary>
	public static class FileInfoExtensions
	{
		/// <summary>Gets the type of the content.</summary>
		/// <param name="fileInfo">The file info.</param>
		/// <param name="defaultContentType">Default content type.</param>
		/// <returns>ContentType of the File, or the defaultContentType.</returns>
		public static string GetContentType(this FileInfo fileInfo, string defaultContentType = null)
		{
			if (fileInfo == null)
			{
				return defaultContentType;
			}

			try
			{
				using (var regkey = Registry.ClassesRoot)
				{
					var fileExtensionKey = regkey.OpenSubKey(fileInfo.Extension);
					return fileExtensionKey.GetValue("Content Type", defaultContentType).ToString();
				}
			}
			catch
			{
				return defaultContentType;
			}
		}
	}
}