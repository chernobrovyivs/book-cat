using System;
using System.Data;

namespace BookCat
{
	public class Book
	{
		public Book()
		{}

		public Book(DataRow rr)
		{
			Book_id = (long) rr["Book_id"];
			LocalPath = (string) rr["LocalPath"];
			Name = (string)rr["Name"];

			Author_id = (long)rr["Author_id"];

			if (rr["Author_info"] is DBNull)
			{
				Author_info = Author.Empty.Fio;
			}
			else
			{
				Author_info = (string)rr["Author_info"];
			}
		}

		public string getGroup
		{
			get { return Name.Substring(0, 1); }
		}

		public long Book_id;
		public long Author_id;

		public string Author_info = ""; // ФИО автора чтобы не париться доп.запросами к бд

		public string Name="";

		public int Year;
		public string CoAuthor = "";
		public string File_path = "";
		public string File_name = "";

		public DateTime Dt_added=DateTime.Now;

		public string ISBN = "";
		public string Announce = "";
		public string Comments = "";
		public string Cover_filename = "";
		public string Publisher = "";
		public string PageCount = "";
		public string Tiraj = "";
		public string LocalPath = "";

		public byte[] Cover;

		public static readonly Book Empty = new Book();

		public bool IsEmpty
		{
			get
			{
				if (Book_id == 0) return true;
				return false;
			}
		}

	}
}
