using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Wintellect.PowerCollections;

namespace BookCat
{
	public class Books
	{
		public static void AddGroups(ListView _listViewBooks)
		{
			foreach (ListViewItem li in _listViewBooks.Items)
			{
				Book b = ((Book)li.Tag);
				if (b.Name != "")
				{
					bool wasfond = false;
					foreach (ListViewGroup lg in _listViewBooks.Groups)
					{
						if (lg.Header == b.Name.Substring(0, 1))
						{
							wasfond = true;
							break;
						}
					}

					if (!wasfond)
					{
						ListViewGroup lvg = new ListViewGroup();
						lvg.Header = b.Name.Substring(0, 1);

						_listViewBooks.Groups.Add(lvg);
					}
				}
			}

			foreach (ListViewItem li in _listViewBooks.Items)
			{
				foreach (ListViewGroup lg in _listViewBooks.Groups)
				{
					if (((Book)li.Tag).Name != "")
					{
						if (((Book)li.Tag).Name.Substring(0, 1) == lg.Header) li.Group = lg;
					}
				}
			}

			// а теперь добавим группу по умолчанию
			ListViewGroup lgd = new ListViewGroup("Неизвестно");
			foreach (ListViewItem li in _listViewBooks.Items)
			{
				if (li.Group ==null)
				{
					_listViewBooks.Groups.Add(lgd);
					li.Group = lgd;
				}
			}
		}


		/// <summary>
		/// Добавляет новую книгу, подставляя новые Id, LocalPath
		/// </summary>
		private static void AddNewBookToDb(Book b, string path)
		{
			// копируем
			b.LocalPath = BookLibrary.AddNewBook(path);

			// и в базу
			SQLiteCommand sc = new SQLiteCommand(@"INSERT INTO Book(Name, Year, Author_id, CoAuthor, File_path, File_name, Dt_added, ISBN, Announce, Comments, Cover, Cover_filename, Publisher, PageCount, Tiraj, LocalPath) 
                                                       VALUES (@Name, @Year, @Author_id, @CoAuthor, @File_path, @File_name, @Dt_added, @ISBN, @Announce, @Comments, @Cover, @Cover_filename, @Publisher, @PageCount, @Tiraj, @LocalPath)");
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

			b.Book_id = Db.ExecuteNonQueryInsert(sc);
		}

		public static ListViewItem GetNewListViewItem(Book _Book)
		{
			ListViewItem li = new ListViewItem();
			li.Text = _Book.Name;
			li.ImageIndex = 1;
			li.Tag = _Book;

			return li;
		}

		public static void FillListViewAll(ListView _listViewBooks, object FillBy)
		{
			_listViewBooks.Items.Clear();
			_listViewBooks.Groups.Clear();

			if (FillBy == null)
			{
				foreach (Book b in SqlBook.GetAll())
				{
					_listViewBooks.Items.Add(GetNewListViewItem(b));
				}
			}
			if (FillBy is Genre)
			{
				foreach (Book b in SqlBook.GetByGenre((Genre)FillBy))
				{
					_listViewBooks.Items.Add(GetNewListViewItem(b));
				}
			}
			if (FillBy is Author)
			{
				foreach (Book b in SqlBook.GetByAuthor((Author)FillBy))
				{
					_listViewBooks.Items.Add(GetNewListViewItem(b));
				}
			}

			AddGroups(_listViewBooks);
		}

		public static void ReadBook(Book book)
		{
			if (book.LocalPath.Length == 0)
			{
				MessageBox.Show("У книги не задан путь к локальному файлу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			Process.Start(book.LocalPath);
		}


		public static Book GetSelectedBook(ListView listViewBooks)
		{
			if (listViewBooks.SelectedItems.Count == 0) return Book.Empty;
			return (Book)listViewBooks.SelectedItems[0].Tag;
		}

		public static IEnumerable<Book> GetSelectedBooks(ListView listViewBooks)
		{
			if (listViewBooks.SelectedItems.Count == 0) yield break;

			foreach (ListViewItem li in listViewBooks.SelectedItems)
				yield return (Book)li.Tag;
		}

		public static bool IsBookExists(string Name)
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT count(*) FROM Book WHERE Name=@Name");
			sc.Parameters.AddWithValue("@Name", Name);

			if ((long)Db.ExecuteScalar(sc)>0) return true;

			return false;
		}

		private static bool DeleteBook(Book book)
		{
            try
            {
                SQLiteCommand sc = new SQLiteCommand("DELETE FROM Book WHERE Book_id=@id");
                sc.Parameters.AddWithValue("@id", book.Book_id);
                Db.ExecuteNonQuery(sc);

                sc = new SQLiteCommand("DELETE FROM BookCatLink WHERE Book_id=@id");
                sc.Parameters.AddWithValue("@id", book.Book_id);
                Db.ExecuteNonQuery(sc);

            	BookLibrary.DeleteBook(book);
            }
            catch (Exception ex)
            {
                enr.dialogError("Ошибка при удалении книги. (" + ex.Message + ")");
                return false;
            }

            return true;
		}

		/// <summary>
		/// Для множественного удаления (использует транзакции)
		/// </summary>
		/// <param name="books"></param>
		public static void DeleteBooks(List<Book> books, ctlStatusProgress csp)
		{
			Db.BeginTransaction();

			int k = 0;
			foreach (Book b in books)
			{
				k++;
				DeleteBook(b);

				ReportStatus(new ctlStatusProgress.ReportParams(b.Name, ctlStatusProgress.Actions.Удаление, Convert.ToInt32(k * 100 / books.Count)), csp);
			}
			Db.CommitTransaction();
		}

		public delegate void ReportStatusItem(ctlStatusProgress.ReportParams rp);
		public static void ReportStatus(ctlStatusProgress.ReportParams rp, ctlStatusProgress csp)
		{
			ReportStatusItem myDelegate = csp.DoReport;
			csp.Invoke(myDelegate, new object[] { rp });
		}

		public static void AddNewBooksToDb(List<Pair<Book, string>> bp, ctlStatusProgress csp)
		{
			Db.BeginTransaction();

			int k = 0;
			foreach (var b in bp)
			{
				k++;
				AddNewBookToDb(b.First, b.Second);

				ReportStatus(new ctlStatusProgress.ReportParams(b.Second, ctlStatusProgress.Actions.Добавление, Convert.ToInt32(k * 100 / bp.Count)), csp);
			}
			Db.CommitTransaction();
		}

		//todo: для множественного выбора дописать транзакции для ускорения
		public static void RefreshListView(ListView books)
		{
			foreach (ListViewItem li in books.SelectedItems)
			{
				Book b = (Book)li.Tag;

				li.Tag = SqlBook.GetBook(b);

				b = (Book) li.Tag;

				li.Text = b.Name;

				bool wasfond = false;
				foreach (ListViewGroup lg in books.Groups)
				{
					if (lg.Header == b.getGroup )
					{
						wasfond = true;
						li.Group = lg;
						break;
					}
				}
				if (!wasfond)
				{
					ListViewGroup lvg = new ListViewGroup();
					lvg.Header = b.getGroup;
					books.Groups.Add(lvg);
					li.Group = lvg;
				}
			}

			
			// можно не ставить но тогда группа уезжает в самый низ как вновьдобавленная
			//books.Groups.Clear();
			//AddGroups(books);
		}


		public static void UpdateBooks(IEnumerable<Book> books)
		{
			Db.BeginTransaction();
			foreach (var b in books)
			{
				SqlBook.UpdateBook(b);
			}
			Db.CommitTransaction();
		}
	}
}
