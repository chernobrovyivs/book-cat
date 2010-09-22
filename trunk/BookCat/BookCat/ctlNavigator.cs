using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
	public partial class ctlNavigator : UserControl
	{
		private TabControl tc;
		private ListView listViewBooks;
		private ListView listViewAuthors;
		private TreeView treeViewGenres;

		public ctlNavigator()
		{
			InitializeComponent();

			toolStrip1.Renderer = new StackRenderer();
			toolStrip2.Renderer = new StackRenderer();
		}

		internal class StackRenderer : ToolStripProfessionalRenderer
		{
			public StackRenderer()
			{
				RoundedEdges = false;
			}

			protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
			{
			}

			protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
			{
			}

			protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
			{
			}

			// This method handles the RenderButtonBackground event.
			protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
			{
				Graphics g = e.Graphics;
				Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
				//Rectangle bounds = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height);

				Color gradientBegin = Color.FromArgb(203, 225, 252);
				Color gradientEnd = Color.FromArgb(125, 165, 224);

				ToolStripButton button = e.Item as ToolStripButton;

				if (button.Pressed || button.Checked)
				{
					gradientBegin = Color.FromArgb(254, 128, 62);
					gradientEnd = Color.FromArgb(255, 223, 154);
				}
				else if (button.Selected)
				{
					gradientBegin = Color.FromArgb(255, 255, 222);
					gradientEnd = Color.FromArgb(255, 203, 136);
				}

				using (Brush b = new LinearGradientBrush(
					bounds,
					gradientBegin,
					gradientEnd,
					LinearGradientMode.Vertical))
				{
					//g.FillRectangle(b, bounds);
					Rectangle r = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height / 2);
					Rectangle r2 = new Rectangle(bounds.X, bounds.Y + bounds.Height / 2, bounds.Width, bounds.Height / 2);

					if (button.ForeColor == SelectedColor)
					{
						g.FillRectangle(
							new LinearGradientBrush(r, Color.FromArgb(70, 70, 70), Color.FromArgb(20, 20, 20), LinearGradientMode.Vertical),
							r);
						g.FillRectangle(
							new LinearGradientBrush(r2, Color.FromArgb(0, 0, 0), Color.FromArgb(10, 10, 10), LinearGradientMode.Vertical),
							r2);
					}
					else if (button.Pressed || button.Checked)
					{
						g.FillRectangle(
							new LinearGradientBrush(r, Color.FromArgb(110, 110, 110), Color.FromArgb(60, 60, 60), LinearGradientMode.Vertical),
							r);
						g.FillRectangle(
							new LinearGradientBrush(r2, Color.FromArgb(40, 40, 40), Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical),
							r2);
					}
					else if (button.Selected)
					{
						g.FillRectangle(
							new LinearGradientBrush(r, Color.FromArgb(130, 130, 130), Color.FromArgb(80, 80, 80), LinearGradientMode.Vertical),
							r);
						g.FillRectangle(
							new LinearGradientBrush(r2, Color.FromArgb(60, 60, 60), Color.FromArgb(70, 70, 70), LinearGradientMode.Vertical),
							r2);
					}
				}

				e.Graphics.DrawRectangle(
					SystemPens.ControlDarkDark,
					bounds);

				g.DrawLine(
					SystemPens.ControlDarkDark,
					bounds.X,
					bounds.Y,
					bounds.Width - 1,
					bounds.Y);

				g.DrawLine(
					SystemPens.ControlDarkDark,
					bounds.X,
					bounds.Y,
					bounds.X,
					bounds.Height - 1);

				ToolStrip toolStrip = button.Owner;
				ToolStripButton nextItem = button.Owner.GetItemAt(
					button.Bounds.X,
					button.Bounds.Bottom + 1) as ToolStripButton;

				if (nextItem == null)
				{
					
					g.DrawLine(
						SystemPens.ControlDarkDark,
						bounds.X,
						bounds.Height - 1,
						bounds.X + bounds.Width - 1,
						bounds.Height - 1);
					
					g.DrawLine(
						SystemPens.ControlDarkDark,
						bounds.Width - 1,
						bounds.Y,
						bounds.Width - 1,
						bounds.Height);
				}
			}
		}


		private void btnNavAuthor_Click(object sender, EventArgs e)
		{
			GoToAuthors();
		}

		private void btnNavGenre_Click(object sender, EventArgs e)
		{
			GoToGenres();
		}

		private void btnNavBook_Click(object sender, EventArgs e)
		{
			Books.FillListViewAll(listViewBooks, null);
			Navigate(DisplayMode.AllBooks);
		}

		public enum DisplayMode
		{
			Author, Genre, BookFromAuthor, BookFromGenre, AllBooks
		}

		public void Init(TabControl _tc, ListView _listViewBooks, TreeView _treeViewGenres, ListView _listViewAuthors)
		{
			tc = _tc;

			listViewBooks = _listViewBooks;
			treeViewGenres = _treeViewGenres;
			listViewAuthors = _listViewAuthors;

			tc.SizeMode = TabSizeMode.Fixed;
			tc.Appearance = TabAppearance.FlatButtons;
			tc.DrawMode = TabDrawMode.OwnerDrawFixed;
			tc.ItemSize = new Size(0, 1);

			GoToAuthors();
		}

		private DisplayMode cur;

		public void SetTitle(string _Text)
		{
			lblTitle.Visible = true;
			lblTitle.Text = _Text;
			Invalidate();
		}

		public new void Refresh()
		{
			base.Refresh();

			Navigate(cur);
		}

		static Color SelectedColor = Color.LemonChiffon;

		public void Navigate(DisplayMode dm)
		{
			cur = dm;

			lblTitle.Visible = false;

			tsbtnNavAuthor.ForeColor = Color.White;
			tsbtnNavGenre.ForeColor = Color.White;
			tsbtnNavBook.ForeColor = Color.White;

			toolStrip1.Visible = true;
			tsbtnNavBack.Visible = false;
			switch (dm)
			{
				case DisplayMode.Author:
					tc.SelectedIndex = 0;
					tsbtnNavAuthor.ForeColor = SelectedColor;
					break;
				case DisplayMode.Genre:
					tc.SelectedIndex = 1;
					tsbtnNavGenre.ForeColor = SelectedColor;
					break;
				case DisplayMode.AllBooks:
					tc.SelectedIndex = 2;
					tsbtnNavBook.ForeColor = SelectedColor;
					break;
				case DisplayMode.BookFromAuthor:
					tsbtnNavBack.Visible = true;
					toolStrip1.Visible = false;
					tsbtnNavBack.Text = "Все авторы";
					tc.SelectedIndex = 2;
					break;
				case DisplayMode.BookFromGenre:
					tsbtnNavBack.Visible = true;
					toolStrip1.Visible = false;
					tsbtnNavBack.Text = "Все жанры";
					tc.SelectedIndex = 2;
					break;
			}
			Invalidate();
		}

		private void btnNavBack_Click(object sender, EventArgs e)
		{
			switch (cur)
			{
				case DisplayMode.BookFromAuthor:
					GoToAuthors();
					break;
				case DisplayMode.BookFromGenre:
					GoToGenres();
					break;
			}
		}

		void SelectTreeNodeRecursive(TreeNodeCollection t, Genre g)
		{
			foreach(TreeNode trn in t)
			{
				SelectTreeNodeRecursive(trn.Nodes, g);

				if (g.Equals(trn.Tag))
				{ 
					treeViewGenres.SelectedNode = trn; 
				}
			}
		}

		void GoToGenres()
		{
			Genre g = Genres.GetSelectedGenre(treeViewGenres);

			Genres.FillTreeViewAll(treeViewGenres);

			SelectTreeNodeRecursive(treeViewGenres.Nodes, g);

			Navigate(DisplayMode.Genre);
		}

		void GoToAuthors()
		{
			Author a = Authors.GetSelectedAuthor(listViewAuthors);

			Authors.FillListViewAll(listViewAuthors);

			foreach (ListViewItem li in listViewAuthors.Items)
				if (((Author)li.Tag).Equals(a)) li.Selected = true;

			Navigate(DisplayMode.Author);
		}

		public new event PaintEventHandler Paint;

		protected override void OnPaint(PaintEventArgs e)
		{
			if (Paint != null)
			{
				Paint(this, e);
			}
			base.OnPaint(e);
		}
		
		private void ctlNavigator_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Rectangle r = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height / 2);
			Rectangle r2 = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y + e.ClipRectangle.Height / 2, e.ClipRectangle.Width, e.ClipRectangle.Height / 2);

			if (r.X == 0 && r.Y == 0 && r.Width == 0 && r.Height == 0) return;

			g.FillRectangle(new LinearGradientBrush(r, Color.FromArgb(110, 110, 110), Color.FromArgb(60, 60, 60), LinearGradientMode.Vertical), r);
			g.FillRectangle(new LinearGradientBrush(r2, Color.FromArgb(40, 40, 40), Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical), r2);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			GoToAuthors();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			GoToGenres();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			Books.FillListViewAll(listViewBooks, null);
			Navigate(DisplayMode.AllBooks);
		}

		private void tsbtnNavBack_Click(object sender, EventArgs e)
		{
			switch (cur)
			{
				case DisplayMode.BookFromAuthor:
					GoToAuthors();
					break;
				case DisplayMode.BookFromGenre:
					GoToGenres();
					break;
			}
		}

	}
}
