using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace BookCat
{
	public class SqlBook
	{
		public static IEnumerable<Book> GetAll()
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT Book.*, Author.Fio AS Author_info FROM Book LEFT JOIN Author ON Author.Author_id=Book.Author_id ORDER BY Name");
			DataTable dt = Db.Fill(sc);

			foreach (DataRow rr in dt.Rows)
			{
				yield return new Book(rr);
			}
		}

		internal static Book GetBook(Book book)
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT  Book.*, Author.Fio AS Author_info FROM Book LEFT JOIN Author ON Author.Author_id=Book.Author_id WHERE Book.Book_id=@Book_id");
			sc.Parameters.AddWithValue("@Book_id", book.Book_id);

			DataTable dt = Db.Fill(sc);

			return new Book(dt.Rows[0]);
		}

		public static IEnumerable<Book> GetByGenre(Genre _Genre)
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT Book.*, Author.Fio AS Author_info FROM Book JOIN BookCatLink ON BookCatLink.Book_id=Book.Book_id LEFT JOIN Author ON Author.Author_id=Book.Author_id WHERE BookCatLink.BookCat_id=@BookCat_id");
			sc.Parameters.AddWithValue("@BookCat_id", _Genre.BookCat_id);

			DataTable dt = Db.Fill(sc);

			foreach (DataRow rr in dt.Rows)
			{
				yield return new Book(rr);
			}
		}

		public static IEnumerable<Book> GetByAuthor(Author _Author)
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT Book.*, Author.Fio AS Author_info FROM Book LEFT JOIN Author ON Author.Author_id=Book.Book_id WHERE Book.Author_id=@Author_id");
			sc.Parameters.AddWithValue("@Author_id", _Author.Author_id);

			DataTable dt = Db.Fill(sc);

			foreach (DataRow rr in dt.Rows)
			{
				yield return new Book(rr);
			}
		}

		internal static void UpdateBook(Book b)
		{
			SQLiteCommand sc = new SQLiteCommand(@"UPDATE Book SET 
                                                    Name=@Name, 
                                                    Year=@Year, 
                                                    Author_id=@Author_id, 
                                                    CoAuthor=@CoAuthor,
                                                    File_path=@File_path,
                                                    File_name=@File_name,
                                                    Dt_added=@Dt_added,
                                                    ISBN=@ISBN,
                                                    Announce=@Announce,
                                                    Comments=@Comments,
                                                    Cover=@Cover,
                                                    Cover_filename=@Cover_filename,
                                                    Publisher=@Publisher,
                                                    PageCount=@PageCount,
                                                    Tiraj=@Tiraj,
                                                    LocalPath=@LocalPath
                                            WHERE Book_id=@Book_id
                ");
			sc.Parameters.AddWithValue("@Name", b.Name);
			sc.Parameters.AddWithValue("@Year", b.Year);
			sc.Parameters.AddWithValue("@Author_id", b.Author_id);
			sc.Parameters.AddWithValue("@CoAuthor", b.CoAuthor);
			sc.Parameters.AddWithValue("@File_path", b.File_path);
			sc.Parameters.AddWithValue("@File_name", b.File_name);
			sc.Parameters.AddWithValue("@Dt_added", b.Dt_added);
			sc.Parameters.AddWithValue("@ISBN", b.ISBN);
			sc.Parameters.AddWithValue("@Announce", b.Announce);
			sc.Parameters.AddWithValue("@Comments", b.Comments);
			sc.Parameters.AddWithValue("@Cover", b.Cover);
			sc.Parameters.AddWithValue("@Cover_filename", b.Cover_filename);
			sc.Parameters.AddWithValue("@Publisher", b.Publisher);
			sc.Parameters.AddWithValue("@PageCount", b.PageCount);
			sc.Parameters.AddWithValue("@Tiraj", b.Tiraj);
			sc.Parameters.AddWithValue("@LocalPath", b.LocalPath);

			sc.Parameters.AddWithValue("@Book_id", b.Book_id);

			Db.ExecuteNonQuery(sc);
		}

	}
}
