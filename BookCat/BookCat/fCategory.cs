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
    public partial class fCategory : Form
    {
        public fCategory(int bookcat_id)
        {
            InitializeComponent();
        }

        public fCategory()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            /*
            FileStream fs = new FileStream(textBox2.Text, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            List<byte> b = new List<byte>();
            for (int i = 0; i < fs.Length; i++)
            {
                b.Add(r.ReadByte());
            }
            r.Close();
            fs.Close();

            bo.book.EndEdit();

            bo.bFile_Contents = b.ToArray();
            bo.bFile_fullname = textBox2.Text;
            bo.Update();
        
            /*
            SQLiteConnection con = new SQLiteConnection(Properties.Settings.Default.dbConnectionString);
            con.Open();
            SQLiteCommand sc = new SQLiteCommand("INSERT INTO Book(Name, Year, Author_id, File, File_ext) VALUES (@Name,@Year,@Author_id,@File,@File_ext)", con);
            sc.Parameters.AddWithValue("@Name", textBox1.Text);
            sc.Parameters.AddWithValue("@Year", 1);
            sc.Parameters.AddWithValue("@Author_id", 1);
            sc.Parameters.AddWithValue("@File", b.ToArray());
            sc.Parameters.AddWithValue("@File_ext", "12");
            object ret = sc.ExecuteScalar();

            con.Close();
             */ 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            //MessageBox.Show(fd.FileName);
            //textBox2.Text = fd.FileName;

        }

        private void fAddBook_Load(object sender, EventArgs e)
        {
            //bo = new Book(book_id);

            //textBox2.Text = bo.bFile_fullname;
            //textBox2.DataBindings.Add("Text", bo.book.Table, "File_fullname");
        }
    }
}
