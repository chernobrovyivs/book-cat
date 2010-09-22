using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
    public partial class fAuthor : Form
    {
        DataRow author;
        private long Author_id;
        private byte[] CoverImage = new byte[]{};

        public static DialogResult DeleteById(long _Author_id)
        {
            if (enr.dialogRealyDelete != DialogResult.Yes)
                return DialogResult.Cancel;

            SQLiteCommand sc = new SQLiteCommand("SELECT Count(*) FROM Book WHERE Author_id=@id");
            sc.Parameters.AddWithValue("@id", _Author_id);
            long c = (long)Db.ExecuteScalar(sc);

            if (c>0)
            {
                if (DialogResult.Yes != MessageBox.Show(
                    "Существуют книги связанные с данным автором! Действительно продолжить удаление и обнулить у связанных книг автора?", "Внимание",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                    )
                {
                    return DialogResult.Cancel;
                }
            }

            try
            {
                sc = new SQLiteCommand("DELETE FROM Author WHERE Author_id=@id");
                sc.Parameters.AddWithValue("@id", _Author_id);
                Db.ExecuteNonQuery(sc);

                sc = new SQLiteCommand("UPDATE Book SET Author_id=0 WHERE Book_id=@id");
                sc.Parameters.AddWithValue("@id", _Author_id);
                Db.ExecuteNonQuery(sc);
            }
            catch (Exception ex)
            {
                enr.dialogError("Ошибка при удалении книги. (" + ex.Message + ")");
                return DialogResult.Cancel;
            }

            return DialogResult.OK;
        }


        public fAuthor(long _author_id)
        {
            InitializeComponent();

            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Author WHERE Author_id=@id");
            sc.Parameters.AddWithValue("@id", _author_id);

            DataTable dt1 = Db.Fill(sc);

            if (dt1.Rows.Count != 1) throw new ApplicationException("Неверный @id");

            Author_id = _author_id;
            author = dt1.Rows[0];
        }

        public fAuthor()
        {
            InitializeComponent();

            SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Author WHERE Author_id = 0");
            DataTable dt1 = Db.Fill(sc).Clone();

            author = dt1.NewRow();

            Author_id  = 0;

            author["Fio"] = "";
            author["Dates"] = "";

            author["About"] = "";

            author["Dt_added"] = DateTime.Now;

            author["Cover"] = DBNull.Value;
            author["Cover_filename"] = "";

            author.EndEdit();

            dt1.Rows.Add(author);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Validate();
            author.EndEdit();

            if (CoverImage.Length != 0)
            {
                author["Cover"] = CoverImage;
            }

            if (Author_id == 0)
            {
                SQLiteCommand sc = new SQLiteCommand(@"INSERT INTO Author(Fio, Dates, About, Dt_added, Cover, Cover_filename) 
                                                       VALUES (@Fio, @Dates, @About, @Dt_added, @Cover, @Cover_filename)");
                sc.Parameters.AddWithValue("@Fio", author["Fio"]);
                sc.Parameters.AddWithValue("@Dates", author["Dates"]);
                sc.Parameters.AddWithValue("@About", author["About"]);
                sc.Parameters.AddWithValue("@Dt_added", author["Dt_added"]);
                sc.Parameters.AddWithValue("@Cover", author["Cover"]);
                sc.Parameters.AddWithValue("@Cover_filename", author["Cover_filename"]);

                Author_id = Db.ExecuteNonQueryInsert(sc);
            }
            else
            {
                SQLiteCommand sc = new SQLiteCommand(@"UPDATE Author SET 
                                                    Fio=@Fio, 
                                                    Dates=@Dates, 
                                                    About=@About, 
                                                    Dt_added=@Dt_added,
                                                    Cover=@Cover,
                                                    Cover_filename=@Cover_filename
                                            WHERE Author_id=@Author_id
                ");
                sc.Parameters.AddWithValue("@Fio", author["Fio"]);
                sc.Parameters.AddWithValue("@Dates", author["Dates"]);
                sc.Parameters.AddWithValue("@About", author["About"]);
                sc.Parameters.AddWithValue("@Dt_added", author["Dt_added"]);
                sc.Parameters.AddWithValue("@Cover", author["Cover"]);
                sc.Parameters.AddWithValue("@Cover_filename", author["Cover_filename"]);

                sc.Parameters.AddWithValue("@Author_id", Author_id);

                Db.ExecuteNonQuery(sc);
            }

            this.Close();
        }

        private void fAddBook_Load(object sender, EventArgs e)
        {
            textBox1.DataBindings.Add("Text", author.Table, "Fio");
            textBox3.DataBindings.Add("Text", author.Table, "Dates");
            textBox4.DataBindings.Add("Text", author.Table, "About");

            textBox5.DataBindings.Add("Text", author.Table, "Cover_filename");

            if (author["Cover"] != DBNull.Value)
            {
                CoverImage = (byte[])author["Cover"];
                RefreshCover();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();

            author["Cover_filename"] = fd.FileName;

            try
            {
                CoverImage = enr.getBytesFromFile(fd.FileName);
                RefreshCover();
            }
            catch { }
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
    }
}
