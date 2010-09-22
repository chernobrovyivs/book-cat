using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
    public partial class fBook : Form
    {
        long Book_id;
        DataRow book;
        private byte[] CoverImage = new byte[] { };

        public static DialogResult DeleteById(long _Book_id)
        {
            if (enr.dialogRealyDelete != DialogResult.Yes)
                return DialogResult.Cancel;

            try
            {
                SQLiteCommand sc = new SQLiteCommand("DELETE FROM Book WHERE Book_id=@id");
                sc.Parameters.AddWithValue("@id", _Book_id);
                Db.ExecuteNonQuery(sc);

                sc = new SQLiteCommand("DELETE FROM BookCatLink WHERE Book_id=@id");
                sc.Parameters.AddWithValue("@id", _Book_id);
                Db.ExecuteNonQuery(sc);
            }
            catch (Exception ex)
            {
                enr.dialogError("Ошибка при удалении книги. (" + ex.Message + ")");
                return DialogResult.Cancel;
            }

            return DialogResult.OK;
        }

    	private Book Kniga;

		public fBook(Book _Book)
		{
			InitializeComponent();


			SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Book WHERE Book_id=@id");
			sc.Parameters.AddWithValue("@id", _Book.Book_id);

			DataTable dt1 = Db.Fill(sc);

			if (dt1.Rows.Count != 1) throw new ApplicationException("Неверный @id");


			Kniga = _Book;
			Book_id = _Book.Book_id;
			book = dt1.Rows[0];
		}

		public fBook(List<Book> _BookList)
		{
			InitializeComponent();

			MessageBox.Show("omg");

			//SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Book WHERE Book_id=@id");
			//sc.Parameters.AddWithValue("@id", _Book.Book_id);

			//DataTable dt1 = Db.Fill(sc);

			//if (dt1.Rows.Count != 1) throw new ApplicationException("Неверный @id");


			//Kniga = _Book;
			//Book_id = _Book.Book_id;
			//book = dt1.Rows[0];
		}

		public fBook()
        {
            InitializeComponent();

            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Book WHERE Book_id = 0");
            DataTable dt1 = Db.Fill(sc).Clone();

            book = dt1.NewRow();

            Book_id = 0;

            book["Name"] = "";
            book["Year"] = 0;

            book["Author_id"] = 0;
            book["CoAuthor"] = "";

            book["File_path"] = "";
            book["File_name"] = "";

            book["Dt_added"] = DateTime.Now;

            book["ISBN"] = "";
            book["Announce"] = "";
            book["Comments"] = "";

            book["Cover"] = DBNull.Value;
            book["Cover_filename"] = "";

            book["Publisher"] = "";
            book["PageCount"] = 0;
            book["Tiraj"] = 0;

            book["LocalPath"] = "";

            book.EndEdit();

            dt1.Rows.Add(book);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Validate();
            book.EndEdit();

			string lFile = (string)book["LocalPath"];

			if (lFile != "" && !File.Exists(lFile))
			{
				enr.dialogError("Указанный в пути файл не существует");
				return;
			}

			// если все в порядке, передвигаем файл
			if (lFile != "" & Book_id != 0)
			{
				// если путь отличается
				if ((string)book["LocalPath", DataRowVersion.Original] != (string)book["LocalPath"])
				{
					// всегда удаляем по старому пути
					BookLibrary.DeleteBook((string)book["LocalPath", DataRowVersion.Original]);

					// если не пустой новый путь то сохраняем по-новому
					if ((string)book["LocalPath"] != "")
					{
						book["LocalPath"] = BookLibrary.AddNewBook((string) book["LocalPath"]);
					}
				}
				// изменился только автор
				else if ((long)book["Author_id", DataRowVersion.Original] != (long)book["Author_id"])
				{
					book["LocalPath"] = BookLibrary.MoveBookToNewAuthor((string)book["LocalPath"], Authors.GetById((long)book["Author_id"]));
				}
				
			}

            if (CoverImage.Length!=0 )
            {
                book["Cover"] = CoverImage;
            }

            if (Book_id == 0)
            {
                SQLiteCommand sc = new SQLiteCommand(@"INSERT INTO Book(Name, Year, Author_id, CoAuthor, File_path, File_name, Dt_added, ISBN, Announce, Comments, Cover, Cover_filename, Publisher, PageCount, Tiraj, LocalPath) 
                                                       VALUES (@Name, @Year, @Author_id, @CoAuthor, @File_path, @File_name, @Dt_added, @ISBN, @Announce, @Comments, @Cover, @Cover_filename, @Publisher, @PageCount, @Tiraj, @LocalPath)");
                sc.Parameters.AddWithValue("@Name", book["Name"]);
                sc.Parameters.AddWithValue("@Year", book["Year"]);
                sc.Parameters.AddWithValue("@Author_id", book["Author_id"]);
                sc.Parameters.AddWithValue("@CoAuthor", book["CoAuthor"]);
                sc.Parameters.AddWithValue("@File_path", book["File_path"]);
                sc.Parameters.AddWithValue("@File_name", book["File_name"]);
                sc.Parameters.AddWithValue("@Dt_added", book["Dt_added"]);
                sc.Parameters.AddWithValue("@ISBN", book["ISBN"]);
                sc.Parameters.AddWithValue("@Announce", book["Announce"]);
                sc.Parameters.AddWithValue("@Comments", book["Comments"]);
                sc.Parameters.AddWithValue("@Cover", book["Cover"]);
                sc.Parameters.AddWithValue("@Cover_filename", book["Cover_filename"]);
                sc.Parameters.AddWithValue("@Publisher", book["Publisher"]);
                sc.Parameters.AddWithValue("@PageCount", book["PageCount"]);
                sc.Parameters.AddWithValue("@Tiraj", book["Tiraj"]);
                sc.Parameters.AddWithValue("@LocalPath", book["LocalPath"]);

                Book_id = Db.ExecuteNonQueryInsert(sc);
            }
            else
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
                sc.Parameters.AddWithValue("@Name", book["Name"]);
                sc.Parameters.AddWithValue("@Year", book["Year"]);
                sc.Parameters.AddWithValue("@Author_id", book["Author_id"]);
                sc.Parameters.AddWithValue("@CoAuthor", book["CoAuthor"]);
                sc.Parameters.AddWithValue("@File_path", book["File_path"]);
                sc.Parameters.AddWithValue("@File_name", book["File_name"]);
                sc.Parameters.AddWithValue("@Dt_added", book["Dt_added"]);
                sc.Parameters.AddWithValue("@ISBN", book["ISBN"]);
                sc.Parameters.AddWithValue("@Announce", book["Announce"]);
                sc.Parameters.AddWithValue("@Comments", book["Comments"]);
                sc.Parameters.AddWithValue("@Cover", book["Cover"]);
                sc.Parameters.AddWithValue("@Cover_filename", book["Cover_filename"]);
                sc.Parameters.AddWithValue("@Publisher", book["Publisher"]);
                sc.Parameters.AddWithValue("@PageCount", book["PageCount"]);
                sc.Parameters.AddWithValue("@Tiraj", book["Tiraj"]);
                sc.Parameters.AddWithValue("@LocalPath", book["LocalPath"]);

                sc.Parameters.AddWithValue("@Book_id", Book_id);

                Db.ExecuteNonQuery(sc);
            }

			// устанавливает выбранные связи для конкретной книги
			bookCat1.Save(Book_id);

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();

            book["Cover_filename"] = fd.FileName;

            try
            {
                CoverImage = enr.getBytesFromFile(fd.FileName);
                RefreshCover();
            } 
            catch {}
        }

        void RefreshCover()
        {
            if (CoverImage.Length != 0)
            {
                try
                {
                    Image i = Image.FromStream(new MemoryStream(CoverImage));
                    pictureBox1.Image = i;
                }
                catch
                {
                    CoverImage = new byte[]{};
                }
            }
        }

        private void fAddBook_Load(object sender, EventArgs e)
        {
            textBox1.DataBindings.Add("Text", book.Table, "Name");
            textBox2.DataBindings.Add("Text", book.Table, "Cover_filename");
            textBox3.DataBindings.Add("Text", book.Table, "Year");
            textBox4.DataBindings.Add("Text", book.Table, "Announce");
            textBox5.DataBindings.Add("Text", book.Table, "CoAuthor");
            textBox6.DataBindings.Add("Text", book.Table, "Publisher");
            textBox7.DataBindings.Add("Text", book.Table, "PageCount");
            textBox8.DataBindings.Add("Text", book.Table, "Tiraj");
            textBox9.DataBindings.Add("Text", book.Table, "ISBN");
            textBox10.DataBindings.Add("Text", book.Table, "LocalPath");


        	RefreshAuthors();

            if (book["Cover"] != DBNull.Value)
            {
                CoverImage = (byte[])book["Cover"];
                RefreshCover();
            }

            bookCat1.LoadChekByBookId(Book_id);
        }

		private void RefreshAuthors()
		{
			int wassel = comboBox1.SelectedIndex;

			SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Author");
			DataTable dtAuthor = Db.Fill(sc);
			comboBox1.DataSource = dtAuthor;
			comboBox1.DisplayMember = "Fio";
			comboBox1.ValueMember = "Author_id";
			comboBox1.DataBindings.Clear();
			comboBox1.DataBindings.Add("SelectedValue", book.Table, "Author_id");

			comboBox1.SelectedIndex = wassel;
	
		}

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();

            book["LocalPath"] = fd.FileName;
        }

		private void btnNewAuthor_Click(object sender, EventArgs e)
		{
			fAuthor f = new fAuthor();
			f.ShowDialog();

			RefreshAuthors();
		}
    }
}
