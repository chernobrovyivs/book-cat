using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
	public partial class fBooks : Form
	{
		private List<Book> books;

		public fBooks(List<Book> _books)
		{
			books = _books;

			InitializeComponent();

			RefreshAuthors();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			/*
			int[] scores = new int[] { 97, 92, 81, 60 };

			var scoreQuery =
				from book in books
				where book.Year > 80
				select book;
			*/

			bool NeedUpdate = false;

			if (chkAuthor.Checked && dataGridView1.SelectedRows.Count > 0)
			{
				long Author_id = (long) ((DataRowView) dataGridView1.SelectedRows[0].DataBoundItem).Row["Author_id"];

				foreach (Book b in books)
				{
					b.Author_id = Author_id;
				}
				NeedUpdate = true;
			}

			if (NeedUpdate)
			{
				Books.UpdateBooks(books);
			}

			DialogResult = DialogResult.OK;
		}

		private void RefreshAuthors()
		{
			SQLiteCommand sc = new SQLiteCommand("SELECT * FROM Author");
			DataTable dtAuthor = Db.Fill(sc);

			dataGridView1.AutoGenerateColumns = false;
			dataGridView1.DataSource = dtAuthor;

			DataGridViewColumn cmn = new DataGridViewTextBoxColumn();
			cmn.Name = "Fio";
			cmn.HeaderText = "Ф.И.О.";
			cmn.DataPropertyName = "Fio";
			cmn.Visible = true;
			dataGridView1.Columns.Add(cmn);


			//DataGridViewColumn dc = new DataGridViewColumn();
			
			//dataGridView1.Columns.Add("Fio", "Fio");

	
			//comboBox1.DisplayMember = "Fio";
			//comboBox1.ValueMember = "Author_id";
			//comboBox1.DataBindings.Clear();
			//comboBox1.DataBindings.Add("SelectedValue", book.Table, "Author_id");

			//comboBox1.SelectedIndex = wassel;

		}

		private void button3_Click(object sender, EventArgs e)
		{
			fAuthor f = new fAuthor();
			f.ShowDialog();

			RefreshAuthors();
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			chkAuthor.Checked = true;
		}
	}
}
