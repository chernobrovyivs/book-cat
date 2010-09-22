using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
	public class Genres
	{
		/*
		public static ListViewItem GetNewListViewItem(Book _Book)
		{
			ListViewItem li = new ListViewItem();
			li.Text = _Book.Name;
			li.ImageIndex = 1;
			li.Tag = _Book;

			return li;
		}

		public static IEnumerable<Book> GetAll()
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Book ORDER BY Name");
			DataTable dt = Db.Fill(sc);

			foreach (DataRow rr in dt.Rows)
			{
				yield return new Book
				{
					Book_id = (long)rr["Book_id"],
					Name = (string)rr["Name"],
				};
			}
		}

		public static void FillListViewByAuthor(ListView _listViewBooks, Author _Author)
		{
			_listViewBooks.Items.Clear();

			foreach (Book b in GetByAuthor(_Author))
			{
				_listViewBooks.Items.Add(GetNewListViewItem(b));
			}
		}

		public static IEnumerable<Book> GetByAuthor(Author _Author)
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Book WHERE Author_id=@Author_id");
			sc.Parameters.AddWithValue("@Author_id", _Author.Author_id);

			DataTable dt = Db.Fill(sc);

			foreach (DataRow rr in dt.Rows)
			{
				yield return new Book
				{
					Book_id = (long)rr["Book_id"],
					Name = (string)rr["Name"]
				};
			}
		}
		*/
		public static void FillTreeViewAll(TreeView treeViewGenres)
		{
			treeViewGenres.Nodes.Clear();

			SQLiteCommand sc = new SQLiteCommand("SELECT BookCat.*, count(BookCatLink.BookCat_id) AS CC FROM BookCat LEFT JOIN BookCatLink ON BookCatLink.BookCat_id=BookCat.BookCat_id GROUP BY BookCat.BookCat_id");
			DataTable dat = Db.Fill(sc);

			rec(dat, 0, null, treeViewGenres);

			treeViewGenres.ExpandAll();
		}

		private static void rec(DataTable dat, long top_id, TreeNode tr, TreeView treeViewGenres)
		{
			foreach (DataRow rr in dat.Select("Top_id=" + top_id))
			{
				TreeNode t = new TreeNode((string)rr["Name"] + " (" + rr["CC"] + ")")

				{
					Tag =
						new Genre
						{
							BookCat_id = (long)rr["BookCat_id"],
							Name = (string)rr["Name"],
							Top_id = (long)rr["Top_id"]
						}
				};

				if (tr == null)
					treeViewGenres.Nodes.Add(t);
				else
					tr.Nodes.Add(t);

				rec(dat, (long)rr["BookCat_id"], t, treeViewGenres);
			}
		}

		public static Genre GetSelectedGenre(TreeView treeViewGenres)
		{
			if (treeViewGenres.SelectedNode == null) return Genre.Empty;
			return (Genre)treeViewGenres.SelectedNode.Tag;
		}
	
		/*
		public static Book GetSelectedBook(ListView listViewBooks)
		{
			if (listViewBooks.SelectedItems.Count == 0) return Book.Empty;
			return (Book)listViewBooks.SelectedItems[0].Tag;
		}
		*/ 
	}
}
