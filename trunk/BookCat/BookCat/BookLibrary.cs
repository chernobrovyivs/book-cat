using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BookCat
{
	public class BookLibrary
	{
		public static string GetPathAuthorUnknown
		{
			get
			{
				string unkp = Path.Combine(Program.us.txtLocalStore, "Неизвестный автор");

				if (!Directory.Exists(unkp))
				{
					Directory.CreateDirectory(unkp);
				}

				return unkp;
			}
		}

		public static string AddNewBook(string path)
		{
			string to = Path.Combine(GetPathAuthorUnknown, Path.GetFileName(path));
			File.Copy(path, to, true);
			return to;
		}

		public static void DeleteBook(string LocalPath)
		{
			if (File.Exists(LocalPath))
			{
				File.SetAttributes(LocalPath, FileAttributes.Normal);
				File.Delete(LocalPath);

				DirectoryInfo i = new DirectoryInfo(Path.GetDirectoryName(LocalPath));
				if (i.GetFiles().Length == 0) Directory.Delete(Path.GetDirectoryName(LocalPath), false);
			}
		}

		public static void DeleteBook(Book book)
		{
			if (File.Exists(book.LocalPath))
			{
				File.SetAttributes(book.LocalPath, FileAttributes.Normal);
				File.Delete(book.LocalPath);

				DirectoryInfo i = new DirectoryInfo(Path.GetDirectoryName(book.LocalPath));
				if (i.GetFiles().Length == 0) Directory.Delete(Path.GetDirectoryName(book.LocalPath), false);
			}
		}

		internal static string GetPathAuthor(Author a)
		{
			if (a.IsEmpty) return GetPathAuthorUnknown;

			string unkp = Path.Combine(Program.us.txtLocalStore, Authors.GetById(a.Author_id).Fio);

			if (!Directory.Exists(unkp))
			{
				Directory.CreateDirectory(unkp);
			}

			return unkp;
		}

		internal static string MoveBookToNewAuthor(string LocalPath, Author a)
		{
			if (File.Exists(LocalPath))
			{
				string newfilewithpath = Path.Combine(GetPathAuthor(a), Path.GetFileName(LocalPath));

				File.SetAttributes(LocalPath, FileAttributes.Normal);
				File.Move(LocalPath, newfilewithpath);

				DirectoryInfo i = new DirectoryInfo(Path.GetDirectoryName(LocalPath));
				if (i.GetFiles().Length == 0) Directory.Delete(Path.GetDirectoryName(LocalPath), false);

				return newfilewithpath;
			}

			throw new ApplicationException("А файла-то не существует");
		}
	}
}
