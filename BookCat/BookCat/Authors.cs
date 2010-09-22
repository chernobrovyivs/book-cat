using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BookCat
{
	public class Authors
	{
		public static ListViewItem GetListViewItem(Author _Author)
		{
			if (_Author.IsEmpty)
			{
				ListViewItem lie = new ListViewItem();
				lie.Text = "[ Не указано ]";
				lie.ImageIndex = 0;
				lie.Tag = Author.Empty;
			}

			ListViewItem li = new ListViewItem();
			li.Text = _Author.Fio;
			li.ImageIndex = 0;
			li.Tag = _Author;

			return li;
		}

		public static DialogResult DeleteAuthor(Author a)
		{
			if (a.IsEmpty) return DialogResult.None;

			if (enr.dialogRealyDelete != DialogResult.Yes)
				return DialogResult.Cancel;

			IEnumerable<Book> books = SqlBook.GetByAuthor(a);

			DialogResult dr = SqlAuthor.DeleteAuthor(a);

			// Перемещаем книжки
			if (dr==DialogResult.OK)
			{
				foreach (Book b in books)
				{
					b.LocalPath = BookLibrary.MoveBookToNewAuthor(b.LocalPath, Author.Empty);
				}
				Books.UpdateBooks(books);
			}
			return dr;
		}

		public static Author GetSelectedAuthor(ListView listViewAuthors)
		{
			if (listViewAuthors.SelectedItems.Count == 0) return Author.Empty;
			return (Author)listViewAuthors.SelectedItems[0].Tag;
		}

		public static void FillListViewAll(ListView listViewAuthors)
		{
			listViewAuthors.Items.Clear();

			foreach (Author a in GetAll())
			{
				listViewAuthors.Items.Add(GetListViewItem(a));
			}

			listViewAuthors.Items.Add(GetListViewItem(Author.Empty));
		}

		public static IEnumerable<Author> GetAll()
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Author ORDER BY Fio");
			DataTable dt = Db.Fill(sc);

			foreach (DataRow rr in dt.Rows)
			{
				Author a = new Author
				{
					Author_id = (long)rr["Author_id"],
					Fio = (string)rr["Fio"],
					About = (string)rr["About"],
					Dates = (string)rr["Dates"],
					Cover_filename = (string)rr["Cover_filename"]
				};

				yield return a;
			}

		}

		public static Author GetById(long id)
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Author WHERE Author_id=@Author_id");
			sc.Parameters.AddWithValue("@Author_id", id);
			DataTable dt = Db.Fill(sc);

			foreach (DataRow rr in dt.Rows)
			{
				Author a = new Author
				{
					Author_id = (long)rr["Author_id"],
					Fio = (string)rr["Fio"],
					About = (string)rr["About"],
					Dates = (string)rr["Dates"],
					Cover_filename = (string)rr["Cover_filename"]
				};

				return a;
			}

			return Author.Empty;
		}
	}
}
