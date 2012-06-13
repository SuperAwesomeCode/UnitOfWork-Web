using SuperAwesomeCode;

namespace System.IO
{
	/// <summary>Extension and static method involving directories.</summary>
	public static class DirectoryExtensions
	{
		/// <summary>Creates a directory if it does not already exists.</summary>
		/// <param name="directory">Path of the directory to check.</param>
		public static void CreateDirectoryIfDoesntExists(string directory)
		{
			Guard.AgainstNullOrEmpty(directory);
			if (Directory.Exists(directory))
			{
				return;
			}

			Directory.CreateDirectory(directory);
		}
	}
}