using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Xml.Serialization;
using BaseControl.TRHtmlGridBox;
using Wintellect.PowerCollections;

namespace BookCat
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			InitSettings();

			Db.Vacuum();

			BindStartData();

			toolStrip1.Renderer = new RedTextRenderer();

			//toolStrip1.Renderer = new ToolStripProfessionalRenderer();
		}

        private void BindStartData()
        {
        	ctlNavigator1.Init(tabControl1, listViewBooks, treeViewGenres, listViewAuthors);
        }

        void InitSettings()
        {
            Program.connString = @"data source=" + Application.StartupPath + @"\db";
			Program.xmlFullPath = Application.StartupPath + @"\BookCat.xml";

			try
			{
				XmlSerializer mySerializer = new XmlSerializer(typeof(UserSettings));
				using (FileStream myFileStream = new FileStream(Program.xmlFullPath, FileMode.Open))
				{
					Program.us = (UserSettings)mySerializer.Deserialize(myFileStream);
				}
			}
			catch (Exception)
			{
				// создаем по умолчанию
				Program.us = new UserSettings();
				Program.us.txtLocalStore = Application.StartupPath + @"\Collection\";
				Program.us.serializeToXml(Program.xmlFullPath);
			}

            splitContainer2.SplitterDistance = splitContainer2.Height - 55;

            Text = "Книжный Котик 0.7";
        }


        public enum curmode
        {
            Автор, 
            Книга,
            Категория
        }



       
        private void button4_Click(object sender, EventArgs e)
        {
            //aAddAuthor();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //aAddBook();
        }

        private void button5_Click(object sender, EventArgs e)
        {
			fCategory fa = new fCategory(0);
			fa.ShowDialog();
		}


        private void button6_Click(object sender, EventArgs e)
        {
            string n = Environment.NewLine;

            MessageBox.Show(Text + n + n + "(c) Андрей Ковалев, 2009","О программе",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }


        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (splitContainer2.Height<100) return;
            splitContainer2.SplitterDistance = splitContainer2.Height - 55;
        }

		private void listViewAuthors_DoubleClick(object sender, EventArgs e)
		{
			if (listViewAuthors.SelectedItems.Count == 0) return;

			Author a = Authors.GetSelectedAuthor(listViewAuthors);

			Books.FillListViewAll(listViewBooks, a);
			ctlNavigator1.Navigate(ctlNavigator.DisplayMode.BookFromAuthor);
			ctlNavigator1.SetTitle(a.GetHeader);
		}


		private void listViewBooks_DoubleClick(object sender, EventArgs e)
		{
			if (File.Exists(Books.GetSelectedBook(listViewBooks).LocalPath))
			{
				aReadBook();
			}
			else
			{
				aBookSettings();
			}
		}

		public static void ProcessDirectory(string targetDirectory, ProcessArgs pa, List<Pair<Book, string>> bp)
		{
			string[] fileEntries = Directory.GetFiles(targetDirectory);
			foreach (string fileName in fileEntries)
			{
				if (!pa.cancel)
					ProcessFile(fileName, pa, bp);
			}

			string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
			foreach (string subdirectory in subdirectoryEntries)
			{
				if (!pa.cancel) ProcessDirectory(subdirectory, pa, bp);
			}
		}

		public class ProcessArgs
		{
			public bool cancel = false;
			public int skipped;
			public int succeed;
			public bool silent = false;

			public bool NotEmpty
			{
				get
				{
					if (skipped!=0 || succeed !=0) return true;

					return false;
				}
			}
/*
			public ProcessArgs(bool _cancel, int _skiped, int _succeed, bool _silent)
			{
				cancel = _cancel;
				skipped = _skiped;
				succeed = _succeed;
				silent = _silent;
			}
*/ 
		}

		public static void ProcessFile(string path, ProcessArgs pa, List<Pair<Book, string>> bp)
		{
			if (pa.cancel) return;
			if (Program.us.Exts.Contains(Path.GetExtension(path).TrimStart('.')))
			{
				if (Books.IsBookExists(Path.GetFileNameWithoutExtension(path)))
				{
					pa.skipped++;
					return;
				}

				DialogResult dr = DialogResult.Yes;
				if (!pa.silent)
				{
					dr = new fDialogParse(String.Format("Найден файл: '{0}'. Добавлять в библиотеку?", path)).ShowDialog();
				}
				if (dr == DialogResult.Cancel)
				{
					pa.cancel = true;
					return;
				}
				if (dr == DialogResult.Ignore)
				{
					pa.silent = true;
					return;
				}
				if (dr == DialogResult.Yes)
				{
					Book b = new Book {
					         			Name = Path.GetFileNameWithoutExtension(path)
					         		  };

					bp.Add(new Pair<Book, string>(b, path));
					pa.succeed++;
				}

			}
		}

		private void treeViewGenres_DoubleClick(object sender, EventArgs e)
		{
			if (treeViewGenres.SelectedNode == null) return;

			Genre g = Genres.GetSelectedGenre(treeViewGenres);

			Books.FillListViewAll(listViewBooks, g);
			ctlNavigator1.Navigate(ctlNavigator.DisplayMode.BookFromGenre);
			ctlNavigator1.SetTitle(g.GetHeader);
		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			FLocalStorage fs = new FLocalStorage();
			fs.ShowDialog();
		}

		private void добавитьКнигиИзКаталогаToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (backgroundWorkerAddBooks.IsBusy) return;

			FolderBrowserDialog fd = new FolderBrowserDialog {ShowNewFolderButton = false};

			ProcessArgs pa = new ProcessArgs();

			List<Pair<Book, string>> bp = new List<Pair<Book, string>>();

			#if DEBUG
			fd.SelectedPath = @"D:\Work\Prg\BookCat\BookCat\bin\_libtoimport";
			#endif

			if (DialogResult.OK == fd.ShowDialog())
			{
				// получаем список книг
				ProcessDirectory(fd.SelectedPath, pa, bp);

				// добавляем
				backgroundWorkerAddBooks.RunWorkerAsync(bp);
			}
			
			/*
			if (pa.NotEmpty)
			{
				string mstr = "Библиотека книг успешно обновлена";

				mstr += "\r\nКниг успешно добавлено: " + pa.succeed;
				if (pa.skipped != 0) mstr += "\r\nКниг из-за совпадения имен с локальной базой пропущено: " + pa.skipped;

				MessageBox.Show(mstr, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Books.FillListViewAll(listViewBooks, null);
			}
			*/ 
		}

		private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
		{
			toolStripSplitButton1.ShowDropDown();
		}

		private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			aDelBook();
		}

		private void читатьToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			aReadBook();
		}

		private void свойстваToolStripMenuItem_Click(object sender, EventArgs e)
		{
			aBookSettings();
		}


		[Browsable(true)]
		public void aReadBook()
		{
			if (File.Exists(Books.GetSelectedBook(listViewBooks).LocalPath))
			{
				Books.ReadBook(Books.GetSelectedBook(listViewBooks));
			}
			else
			{
				enr.dialogError("Указанного файла не существует. Измените путь к книге в окне свойств.");
				aBookSettings();
			}
		}

		public void aDelBook()
		{
			if (backgroundWorkerDelBooks.IsBusy) return;

			if (enr.dialogRealyDelete == DialogResult.Yes)
			{
				List<Book> books = new List<Book>(Books.GetSelectedBooks(listViewBooks));
				//MessageBox.Show(books.Count.ToString());
				backgroundWorkerDelBooks.RunWorkerAsync(books);
			}
		}

		[Browsable(true)]
		public void aBookSettings()
		{
			List<Book> bl = new List<Book>();
			foreach (Book bs in Books.GetSelectedBooks(listViewBooks))
			{
				bl.Add(bs);
			}

			if (bl.Count == 1)
			{
				fBook f = new fBook(bl[0]);
				f.ShowDialog();
			}
			else if (bl.Count > 1)
			{
				fBooks f = new fBooks(bl);
				f.ShowDialog();
			}

			Books.RefreshListView(listViewBooks);
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			foreach (ListViewItem li in listViewBooks.Items)
			{
				li.Selected = checkBox1.Checked;
			}
		}

		private void listViewBooks_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				foreach (ListViewItem li in listViewBooks.Items)
				{
					li.Selected = true;
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Delete)
			{
				e.Handled = true;
				aDelBook();
			}
		}

		private void backgroundWorkerAddBooks_DoWork(object sender, DoWorkEventArgs e)
		{
			List<Pair<Book, string>> bp = (List<Pair<Book, string>>)e.Argument;
			Books.AddNewBooksToDb(bp, ctlStatusProgress1);

		}

		private void backgroundWorkerDelBooks_DoWork(object sender, DoWorkEventArgs e)
		{
			List<Book> books = new List<Book>((IEnumerable<Book>)e.Argument);
			Books.DeleteBooks(books, ctlStatusProgress1);
		}


		public class RedTextRenderer : ToolStripProfessionalRenderer
		{
			protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
			{
				Graphics g = e.Graphics;
				Rectangle r = e.AffectedBounds;
				if (r.X == 0 && r.Y == 0 && r.Width == 0 && r.Height == 0) return;

				g.FillRectangle(new LinearGradientBrush(r, Color.FromArgb(195, 195, 195), Color.FromArgb(150, 150, 150), LinearGradientMode.Vertical), r);

				base.OnRenderToolStripBackground(e);
			}

			protected override void Initialize(ToolStrip toolStrip)
			{
				base.Initialize(toolStrip);
				RoundedEdges = false;
			}
	
			protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
			{

				Graphics g = e.Graphics;
				Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
				using (Pen pen = new Pen(Color.Black))
				{
					g.DrawLine(pen, rectangle.Left, rectangle.Height - 1, rectangle.Right, rectangle.Height - 1);
				}
				//base.OnRenderToolStripBorder(e);
			}

			protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
			{
				bool vertical = e.Vertical;
				ToolStripSeparator item = (ToolStripSeparator)e.Item;
				Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
				Graphics g = e.Graphics;


				Color separatorDark = this.ColorTable.SeparatorDark;
				Color separatorLight = Color.Gray;// this.ColorTable.SeparatorLight;
				Pen pen = new Pen(separatorDark);
				Pen pen2 = new Pen(separatorLight);
				bool flag = true;
				bool flag2 = true;
				bool flag3 = item is ToolStripSeparator;
				bool flag4 = false;
				if (flag3)
				{
					if (vertical)
					{
						if (!item.IsOnDropDown)
						{
							bounds.Y += 3;
							bounds.Height = Math.Max(0, bounds.Height - 6);
						}
					}
					else
					{
						ToolStripDropDownMenu currentParent = item.GetCurrentParent() as ToolStripDropDownMenu;
						if (currentParent != null)
						{
							if (currentParent.RightToLeft == RightToLeft.No)
							{
								bounds.X += currentParent.Padding.Left - 2;
								bounds.Width = currentParent.Width - bounds.X;
							}
							else
							{
								bounds.X += 2;
								bounds.Width = (currentParent.Width - bounds.X) - currentParent.Padding.Right;
							}
						}
						else
						{
							flag4 = true;
						}
					}
				}
				try
				{
					if (vertical)
					{
						if (bounds.Height >= 4)
						{
							bounds.Inflate(0, -2);
						}
						bool flag5 = item.RightToLeft == RightToLeft.Yes;
						Pen pen3 = flag5 ? pen2 : pen;
						Pen pen4 = flag5 ? pen : pen2;
						int num = bounds.Width / 2;
						g.DrawLine(pen3, num, bounds.Top, num, bounds.Bottom - 1);
						num++;
						g.DrawLine(pen4, num, bounds.Top + 1, num, bounds.Bottom);
					}
					else
					{
						if (flag4 && (bounds.Width >= 4))
						{
							bounds.Inflate(-2, 0);
						}
						int num2 = bounds.Height / 2;
						g.DrawLine(pen, bounds.Left, num2, bounds.Right - 1, num2);
						if (!flag3 || flag4)
						{
							num2++;
							g.DrawLine(pen2, bounds.Left + 1, num2, bounds.Right - 1, num2);
						}
					}
				}
				finally
				{
					if (flag && (pen != null))
					{
						pen.Dispose();
					}
					if (flag2 && (pen2 != null))
					{
						pen2.Dispose();
					}
				}

			}
		}

		private void backgroundWorkerDelBooks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Books.FillListViewAll(listViewBooks, null);
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			using (fAbout f = new fAbout())
			{
				f.ShowDialog();
			}
		}

		private void backgroundWorkerAddBooks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Books.FillListViewAll(listViewBooks, null);
		}

		public static void DrawSelectedBorder(Rectangle rect, Graphics g)
		{
			// линия посредине градиента
			using (Pen aPen = new Pen(Color.FromArgb(205, 219, 238)))
				g.DrawLine(aPen, rect.Left, rect.Top + (int)rect.Height / 2, rect.Right, rect.Top + (int)rect.Height / 2);

			Rectangle topPart = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, (int)(rect.Height / 2) - 1);
			Rectangle lowPart = new Rectangle(rect.Left + 1, rect.Top + (int)(rect.Height / 2) + 1, rect.Width - 1, (int)(rect.Height / 2) - 1);

			using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(228, 236, 246), Color.FromArgb(214, 226, 241), LinearGradientMode.Vertical))
				g.FillRectangle(aGB, topPart);

			using (LinearGradientBrush aGB = new LinearGradientBrush(lowPart, Color.FromArgb(194, 212, 235), Color.FromArgb(208, 222, 239), LinearGradientMode.Vertical))
				g.FillRectangle(aGB, lowPart);

			// кант 
			using (Pen aPen = new Pen(Color.FromArgb(141, 174, 217)))
				g.DrawRectangle(aPen, rect.X, rect.Y + 1, rect.Width - 1, rect.Height - 2);
		}


		private void listViewBooks_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			Book b = (Book)e.Item.Tag;

			Image image = imageList1.Images[1]; // а надо по key
			Rectangle rec = e.Bounds;
			Graphics g = e.Graphics;

			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit; // кул шрифт

			if (listViewBooks.SelectedItems.Contains(e.Item))
			{
				DrawSelectedBorder(new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), g);
			}

			int vert = TextRenderer.MeasureText("Какой-то шрифт для определения высоты", listViewBooks.Font).Height;

			StringFormat drawFormat = new StringFormat();
			drawFormat.Trimming = StringTrimming.EllipsisCharacter;

			Rectangle NameRec = new Rectangle(rec.X + 50, rec.Y + 10, rec.Width - 50, vert);
			Rectangle FioRec = new Rectangle(rec.X + 50, rec.Y + vert + 10 + 2, rec.Width - 50, vert);

			g.DrawImage(image, rec.X, rec.Y + (rec.Height - image.Height) / 2, image.Height, image.Width);
			g.DrawString(b.Name, listViewBooks.Font, Brushes.Blue, NameRec, drawFormat);
			g.DrawString(b.Author_info, listViewBooks.Font, Brushes.Gray, FioRec);


		}

		private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Author a = Authors.GetSelectedAuthor(listViewAuthors);
			if (a != Author.Empty)
			{
				using (fAuthor fa = new fAuthor(a.Author_id))
				{
					fa.ShowDialog();
				}
				Authors.FillListViewAll(listViewAuthors);
			}
		}

		private void добавToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (fAuthor fa = new fAuthor()) 
			{
				fa.ShowDialog(); 
			}
			Authors.FillListViewAll(listViewAuthors);
		}

		private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Authors.DeleteAuthor(Authors.GetSelectedAuthor(listViewAuthors));
			Authors.FillListViewAll(listViewAuthors);
		}

		private void добавитьКнигуВручнуюToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (fBook f = new fBook())
			{
				f.ShowDialog();
			}
		}

		private void управлениеКатегориямиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (fCategory fa = new fCategory(0))
			{
				fa.ShowDialog();
			}
		}

		private void добавитьАвтораToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (fAuthor fa = new fAuthor(0))
			{
				fa.ShowDialog();
			}
		}
    }
}