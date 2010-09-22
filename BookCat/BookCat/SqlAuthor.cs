using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
	public class SqlAuthor
	{
		public static DialogResult DeleteAuthor(Author a)
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT Count(*) FROM Book WHERE Author_id=@Author_id");
			sc.Parameters.AddWithValue("@Author_id", a.Author_id);
			long c = (long)Db.ExecuteScalar(sc);

			if (c > 0)
			{
				if (DialogResult.Yes != MessageBox.Show(
					"Существуют книги связанные с данным автором! Действительно продолжить удаление и обнулить у связанных книг автора?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					)
				{
					return DialogResult.Cancel;
				}
			}

			try
			{
				sc = new SQLiteCommand("DELETE FROM Author WHERE Author_id=@Author_id");
				sc.Parameters.AddWithValue("@Author_id", a.Author_id);
				Db.ExecuteNonQuery(sc);

				sc = new SQLiteCommand("UPDATE Book SET Author_id=0 WHERE Book_id=@Author_id");
				sc.Parameters.AddWithValue("@Author_id", a.Author_id);
				Db.ExecuteNonQuery(sc);
			}
			catch (Exception ex)
			{
				enr.dialogError("Ошибка при удалении книги. (" + ex.Message + ")");
				return DialogResult.Cancel;
			}

			return DialogResult.OK;
			
		}
	}
}
