namespace BookCat
{
	partial class ctlNavigator
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlNavigator));
			this.lblTitle = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbtnNavAuthor = new System.Windows.Forms.ToolStripButton();
			this.tsbtnNavGenre = new System.Windows.Forms.ToolStripButton();
			this.tsbtnNavBook = new System.Windows.Forms.ToolStripButton();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.tsbtnNavBack = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblTitle.ForeColor = System.Drawing.Color.White;
			this.lblTitle.Location = new System.Drawing.Point(361, 1);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Padding = new System.Windows.Forms.Padding(3);
			this.lblTitle.Size = new System.Drawing.Size(58, 36);
			this.lblTitle.TabIndex = 24;
			this.lblTitle.Text = "label1";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNavAuthor,
            this.tsbtnNavGenre,
            this.tsbtnNavBook});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip1.Location = new System.Drawing.Point(422, 1);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(301, 28);
			this.toolStrip1.TabIndex = 29;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbtnNavAuthor
			// 
			this.tsbtnNavAuthor.AutoSize = false;
			this.tsbtnNavAuthor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnNavAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.tsbtnNavAuthor.ForeColor = System.Drawing.Color.White;
			this.tsbtnNavAuthor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNavAuthor.Image")));
			this.tsbtnNavAuthor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnNavAuthor.Name = "tsbtnNavAuthor";
			this.tsbtnNavAuthor.Size = new System.Drawing.Size(100, 25);
			this.tsbtnNavAuthor.Text = "Авторы";
			this.tsbtnNavAuthor.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// tsbtnNavGenre
			// 
			this.tsbtnNavGenre.AutoSize = false;
			this.tsbtnNavGenre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnNavGenre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.tsbtnNavGenre.ForeColor = System.Drawing.Color.White;
			this.tsbtnNavGenre.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNavGenre.Image")));
			this.tsbtnNavGenre.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnNavGenre.Name = "tsbtnNavGenre";
			this.tsbtnNavGenre.Size = new System.Drawing.Size(100, 25);
			this.tsbtnNavGenre.Text = "Жанры";
			this.tsbtnNavGenre.Click += new System.EventHandler(this.toolStripButton2_Click);
			// 
			// tsbtnNavBook
			// 
			this.tsbtnNavBook.AutoSize = false;
			this.tsbtnNavBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnNavBook.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.tsbtnNavBook.ForeColor = System.Drawing.Color.White;
			this.tsbtnNavBook.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNavBook.Image")));
			this.tsbtnNavBook.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnNavBook.Name = "tsbtnNavBook";
			this.tsbtnNavBook.Size = new System.Drawing.Size(100, 25);
			this.tsbtnNavBook.Text = "Книги";
			this.tsbtnNavBook.Click += new System.EventHandler(this.toolStripButton3_Click);
			// 
			// toolStrip2
			// 
			this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNavBack});
			this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip2.Location = new System.Drawing.Point(0, 1);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Padding = new System.Windows.Forms.Padding(4);
			this.toolStrip2.Size = new System.Drawing.Size(108, 36);
			this.toolStrip2.TabIndex = 30;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// tsbtnNavBack
			// 
			this.tsbtnNavBack.AutoSize = false;
			this.tsbtnNavBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnNavBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.tsbtnNavBack.ForeColor = System.Drawing.Color.White;
			this.tsbtnNavBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNavBack.Image")));
			this.tsbtnNavBack.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnNavBack.Name = "tsbtnNavBack";
			this.tsbtnNavBack.Size = new System.Drawing.Size(100, 25);
			this.tsbtnNavBack.Text = "Назад";
			this.tsbtnNavBack.Click += new System.EventHandler(this.tsbtnNavBack_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblTitle, 2, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(973, 39);
			this.tableLayoutPanel1.TabIndex = 31;
			// 
			// ctlNavigator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ctlNavigator";
			this.Size = new System.Drawing.Size(973, 39);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ctlNavigator_Paint);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbtnNavAuthor;
		private System.Windows.Forms.ToolStripButton tsbtnNavGenre;
		private System.Windows.Forms.ToolStripButton tsbtnNavBook;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton tsbtnNavBack;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
