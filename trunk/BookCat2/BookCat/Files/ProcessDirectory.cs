using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BookCat.Properties;

namespace BookCat
{
	public class Files
	{
		public static void ProcessDirectory(string targetDirectory, List<string> bp)
		{
			string[] fileEntries = Directory.GetFiles(targetDirectory);
			foreach (string fileName in fileEntries)
			{
				ProcessFile(fileName, bp);
			}

			string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
			foreach (string subdirectory in subdirectoryEntries)
			{
				ProcessDirectory(subdirectory, bp);
			}
		}


		public static void ProcessFile(string path, List<string> bp)
		{
			if (Settings.Default.bookFileExtensions.Contains(Path.GetExtension(path).TrimStart('.')))
			{
				bp.Add(path);
			}
		}
	}
}
