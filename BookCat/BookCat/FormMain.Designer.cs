namespace BookCat
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("111", 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("222", 0);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("111", 1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("222", 1);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.добавитьКнигиИзКаталогаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.добавитьКнигуВручнуюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnSettings = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnAbout = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabAuthor = new System.Windows.Forms.TabPage();
			this.listViewAuthors = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.tabGenre = new System.Windows.Forms.TabPage();
			this.treeViewGenres = new System.Windows.Forms.TreeView();
			this.tabBook = new System.Windows.Forms.TabPage();
			this.listViewBooks = new System.Windows.Forms.ListView();
			this.contextMenuBooks = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.читатьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.свойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.contextMenuGenre = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.читатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.contextMenuAuthor = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.открытьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.добавToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorkerAddBooks = new System.ComponentModel.BackgroundWorker();
			this.backgroundWorkerDelBooks = new System.ComponentModel.BackgroundWorker();
			this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.добавитьАвтораToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.управлениеКатегориямиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ctlStatusProgress1 = new BookCat.ctlStatusProgress();
			this.ctlNavigator1 = new BookCat.ctlNavigator();
			this.toolStrip1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabAuthor.SuspendLayout();
			this.tabGenre.SuspendLayout();
			this.tabBook.SuspendLayout();
			this.contextMenuBooks.SuspendLayout();
			this.contextMenuGenre.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.contextMenuAuthor.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.btnSettings,
            this.toolStripSeparator1,
            this.btnAbout});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1016, 70);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSplitButton1
			// 
			this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьКнигиИзКаталогаToolStripMenuItem,
            this.добавитьКнигуВручнуюToolStripMenuItem,
            this.добавитьАвтораToolStripMenuItem,
            this.управлениеКатегориямиToolStripMenuItem});
			this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
			this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(79, 67);
			this.toolStripSplitButton1.Text = "Книготека";
			this.toolStripSplitButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolStripSplitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
			// 
			// добавитьКнигиИзКаталогаToolStripMenuItem
			// 
			this.добавитьКнигиИзКаталогаToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.добавитьКнигиИзКаталогаToolStripMenuItem.Name = "добавитьКнигиИзКаталогаToolStripMenuItem";
			this.добавитьКнигиИзКаталогаToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
			this.добавитьКнигиИзКаталогаToolStripMenuItem.Text = "Добавить книги из каталога...";
			this.добавитьКнигиИзКаталогаToolStripMenuItem.Click += new System.EventHandler(this.добавитьКнигиИзКаталогаToolStripMenuItem_Click);
			// 
			// добавитьКнигуВручнуюToolStripMenuItem
			// 
			this.добавитьКнигуВручнуюToolStripMenuItem.Name = "добавитьКнигуВручнуюToolStripMenuItem";
			this.добавитьКнигуВручнуюToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
			this.добавитьКнигуВручнуюToolStripMenuItem.Text = "Добавить книгу вручную";
			this.добавитьКнигуВручнуюToolStripMenuItem.Click += new System.EventHandler(this.добавитьКнигуВручнуюToolStripMenuItem_Click);
			// 
			// btnSettings
			// 
			this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
			this.btnSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(71, 67);
			this.btnSettings.Text = "Настройки";
			this.btnSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 70);
			// 
			// btnAbout
			// 
			this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
			this.btnAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(86, 67);
			this.btnAbout.Text = "О программе";
			this.btnAbout.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
			this.splitContainer1.Panel1Collapsed = true;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Panel2.Controls.Add(this.ctlNavigator1);
			this.splitContainer1.Size = new System.Drawing.Size(1016, 617);
			this.splitContainer1.SplitterDistance = 244;
			this.splitContainer1.SplitterWidth = 2;
			this.splitContainer1.TabIndex = 18;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabAuthor);
			this.tabControl1.Controls.Add(this.tabGenre);
			this.tabControl1.Controls.Add(this.tabBook);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 50);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1016, 567);
			this.tabControl1.TabIndex = 26;
			// 
			// tabAuthor
			// 
			this.tabAuthor.Controls.Add(this.listViewAuthors);
			this.tabAuthor.Location = new System.Drawing.Point(4, 22);
			this.tabAuthor.Name = "tabAuthor";
			this.tabAuthor.Size = new System.Drawing.Size(1008, 541);
			this.tabAuthor.TabIndex = 0;
			this.tabAuthor.Text = "Автор";
			this.tabAuthor.UseVisualStyleBackColor = true;
			// 
			// listViewAuthors
			// 
			this.listViewAuthors.ContextMenuStrip = this.contextMenuAuthor;
			this.listViewAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewAuthors.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listViewAuthors.LargeImageList = this.imageList1;
			this.listViewAuthors.Location = new System.Drawing.Point(0, 0);
			this.listViewAuthors.Name = "listViewAuthors";
			this.listViewAuthors.Size = new System.Drawing.Size(1008, 541);
			this.listViewAuthors.TabIndex = 22;
			this.listViewAuthors.UseCompatibleStateImageBehavior = false;
			this.listViewAuthors.DoubleClick += new System.EventHandler(this.listViewAuthors_DoubleClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "author.png");
			this.imageList1.Images.SetKeyName(1, "Empty.png");
			// 
			// tabGenre
			// 
			this.tabGenre.Controls.Add(this.treeViewGenres);
			this.tabGenre.Location = new System.Drawing.Point(4, 22);
			this.tabGenre.Name = "tabGenre";
			this.tabGenre.Size = new System.Drawing.Size(1008, 541);
			this.tabGenre.TabIndex = 2;
			this.tabGenre.Text = "Жанр";
			this.tabGenre.UseVisualStyleBackColor = true;
			// 
			// treeViewGenres
			// 
			this.treeViewGenres.ContextMenuStrip = this.contextMenuGenre;
			this.treeViewGenres.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewGenres.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeViewGenres.Location = new System.Drawing.Point(0, 0);
			this.treeViewGenres.Name = "treeViewGenres";
			this.treeViewGenres.Size = new System.Drawing.Size(1008, 541);
			this.treeViewGenres.TabIndex = 0;
			this.treeViewGenres.DoubleClick += new System.EventHandler(this.treeViewGenres_DoubleClick);
			// 
			// tabBook
			// 
			this.tabBook.Controls.Add(this.listViewBooks);
			this.tabBook.Controls.Add(this.checkBox1);
			this.tabBook.Location = new System.Drawing.Point(4, 22);
			this.tabBook.Name = "tabBook";
			this.tabBook.Size = new System.Drawing.Size(1008, 541);
			this.tabBook.TabIndex = 1;
			this.tabBook.Text = "Книга";
			this.tabBook.UseVisualStyleBackColor = true;
			// 
			// listViewBooks
			// 
			this.listViewBooks.ContextMenuStrip = this.contextMenuBooks;
			this.listViewBooks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewBooks.HideSelection = false;
			this.listViewBooks.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
			this.listViewBooks.LargeImageList = this.imageList1;
			this.listViewBooks.Location = new System.Drawing.Point(0, 17);
			this.listViewBooks.Name = "listViewBooks";
			this.listViewBooks.OwnerDraw = true;
			this.listViewBooks.Size = new System.Drawing.Size(1008, 524);
			this.listViewBooks.TabIndex = 24;
			this.listViewBooks.UseCompatibleStateImageBehavior = false;
			this.listViewBooks.View = System.Windows.Forms.View.Tile;
			this.listViewBooks.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listViewBooks_DrawItem);
			this.listViewBooks.DoubleClick += new System.EventHandler(this.listViewBooks_DoubleClick);
			this.listViewBooks.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewBooks_KeyUp);
			// 
			// contextMenuBooks
			// 
			this.contextMenuBooks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.читатьToolStripMenuItem1,
            this.свойстваToolStripMenuItem,
            this.удалитьToolStripMenuItem2});
			this.contextMenuBooks.Name = "contextMenuBooks";
			this.contextMenuBooks.Size = new System.Drawing.Size(126, 70);
			// 
			// читатьToolStripMenuItem1
			// 
			this.читатьToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.читатьToolStripMenuItem1.Name = "читатьToolStripMenuItem1";
			this.читатьToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
			this.читатьToolStripMenuItem1.Text = "Читать";
			this.читатьToolStripMenuItem1.Click += new System.EventHandler(this.читатьToolStripMenuItem1_Click);
			// 
			// свойстваToolStripMenuItem
			// 
			this.свойстваToolStripMenuItem.Name = "свойстваToolStripMenuItem";
			this.свойстваToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.свойстваToolStripMenuItem.Text = "Свойства";
			this.свойстваToolStripMenuItem.Click += new System.EventHandler(this.свойстваToolStripMenuItem_Click);
			// 
			// удалитьToolStripMenuItem2
			// 
			this.удалитьToolStripMenuItem2.Name = "удалитьToolStripMenuItem2";
			this.удалитьToolStripMenuItem2.Size = new System.Drawing.Size(125, 22);
			this.удалитьToolStripMenuItem2.Text = "Удалить";
			this.удалитьToolStripMenuItem2.Click += new System.EventHandler(this.удалитьToolStripMenuItem2_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.checkBox1.Location = new System.Drawing.Point(0, 0);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(1008, 17);
			this.checkBox1.TabIndex = 25;
			this.checkBox1.Text = "Выделить все";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// contextMenuGenre
			// 
			this.contextMenuGenre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.читатьToolStripMenuItem});
			this.contextMenuGenre.Name = "contextMenuStrip1";
			this.contextMenuGenre.Size = new System.Drawing.Size(208, 48);
			// 
			// открытьToolStripMenuItem
			// 
			this.открытьToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.открытьToolStripMenuItem.Text = "Показать книги жанра";
			// 
			// читатьToolStripMenuItem
			// 
			this.читатьToolStripMenuItem.Name = "читатьToolStripMenuItem";
			this.читатьToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.читатьToolStripMenuItem.Text = "Управление жанрами";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1016, 25);
			this.panel1.TabIndex = 19;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BackColor = System.Drawing.Color.Black;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 70);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.panel1);
			this.splitContainer2.Size = new System.Drawing.Size(1016, 646);
			this.splitContainer2.SplitterDistance = 617;
			this.splitContainer2.TabIndex = 20;
			// 
			// contextMenuAuthor
			// 
			this.contextMenuAuthor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem1,
            this.изменитьToolStripMenuItem,
            this.добавToolStripMenuItem,
            this.удалитьToolStripMenuItem1});
			this.contextMenuAuthor.Name = "contextMenuAuthor";
			this.contextMenuAuthor.Size = new System.Drawing.Size(211, 92);
			// 
			// открытьToolStripMenuItem1
			// 
			this.открытьToolStripMenuItem1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.открытьToolStripMenuItem1.Name = "открытьToolStripMenuItem1";
			this.открытьToolStripMenuItem1.Size = new System.Drawing.Size(210, 22);
			this.открытьToolStripMenuItem1.Text = "Показать книги автора";
			// 
			// добавToolStripMenuItem
			// 
			this.добавToolStripMenuItem.Name = "добавToolStripMenuItem";
			this.добавToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.добавToolStripMenuItem.Text = "Добавить";
			this.добавToolStripMenuItem.Click += new System.EventHandler(this.добавToolStripMenuItem_Click);
			// 
			// удалитьToolStripMenuItem1
			// 
			this.удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
			this.удалитьToolStripMenuItem1.Size = new System.Drawing.Size(210, 22);
			this.удалитьToolStripMenuItem1.Text = "Удалить";
			this.удалитьToolStripMenuItem1.Click += new System.EventHandler(this.удалитьToolStripMenuItem1_Click);
			// 
			// backgroundWorkerAddBooks
			// 
			this.backgroundWorkerAddBooks.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerAddBooks_DoWork);
			this.backgroundWorkerAddBooks.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerAddBooks_RunWorkerCompleted);
			// 
			// backgroundWorkerDelBooks
			// 
			this.backgroundWorkerDelBooks.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDelBooks_DoWork);
			this.backgroundWorkerDelBooks.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDelBooks_RunWorkerCompleted);
			// 
			// изменитьToolStripMenuItem
			// 
			this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
			this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.изменитьToolStripMenuItem.Text = "Изменить";
			this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.изменитьToolStripMenuItem_Click);
			// 
			// добавитьАвтораToolStripMenuItem
			// 
			this.добавитьАвтораToolStripMenuItem.Name = "добавитьАвтораToolStripMenuItem";
			this.добавитьАвтораToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
			this.добавитьАвтораToolStripMenuItem.Text = "Добавить автора";
			this.добавитьАвтораToolStripMenuItem.Click += new System.EventHandler(this.добавитьАвтораToolStripMenuItem_Click);
			// 
			// управлениеКатегориямиToolStripMenuItem
			// 
			this.управлениеКатегориямиToolStripMenuItem.Name = "управлениеКатегориямиToolStripMenuItem";
			this.управлениеКатегориямиToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
			this.управлениеКатегориямиToolStripMenuItem.Text = "Управление категориями";
			this.управлениеКатегориямиToolStripMenuItem.Click += new System.EventHandler(this.управлениеКатегориямиToolStripMenuItem_Click);
			// 
			// ctlStatusProgress1
			// 
			this.ctlStatusProgress1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ctlStatusProgress1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ctlStatusProgress1.BackgroundImage")));
			this.ctlStatusProgress1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ctlStatusProgress1.Location = new System.Drawing.Point(513, 12);
			this.ctlStatusProgress1.MaximumSize = new System.Drawing.Size(500, 46);
			this.ctlStatusProgress1.MinimumSize = new System.Drawing.Size(500, 46);
			this.ctlStatusProgress1.Name = "ctlStatusProgress1";
			this.ctlStatusProgress1.Size = new System.Drawing.Size(500, 46);
			this.ctlStatusProgress1.TabIndex = 24;
			// 
			// ctlNavigator1
			// 
			this.ctlNavigator1.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctlNavigator1.Location = new System.Drawing.Point(0, 0);
			this.ctlNavigator1.Name = "ctlNavigator1";
			this.ctlNavigator1.Size = new System.Drawing.Size(1016, 50);
			this.ctlNavigator1.TabIndex = 25;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1016, 716);
			this.Controls.Add(this.ctlStatusProgress1);
			this.Controls.Add(this.splitContainer2);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FormMain";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabAuthor.ResumeLayout(false);
			this.tabGenre.ResumeLayout(false);
			this.tabBook.ResumeLayout(false);
			this.tabBook.PerformLayout();
			this.contextMenuBooks.ResumeLayout(false);
			this.contextMenuGenre.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.contextMenuAuthor.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn bookidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authoridDataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bookCatidDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn authoridDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fioDataGridViewTextBoxColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ContextMenuStrip contextMenuGenre;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem читатьToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuAuthor;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem добавToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabAuthor;
		private System.Windows.Forms.ListView listViewAuthors;
		private System.Windows.Forms.TabPage tabGenre;
		private System.Windows.Forms.TreeView treeViewGenres;
		private System.Windows.Forms.TabPage tabBook;
		private System.Windows.Forms.ListView listViewBooks;
		private ctlNavigator ctlNavigator1;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
		private System.Windows.Forms.ToolStripButton btnSettings;
		private System.Windows.Forms.ToolStripMenuItem добавитьКнигиИзКаталогаToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem добавитьКнигуВручнуюToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuBooks;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem читатьToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem свойстваToolStripMenuItem;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnAbout;
		private System.ComponentModel.BackgroundWorker backgroundWorkerAddBooks;
		private ctlStatusProgress ctlStatusProgress1;
		private System.ComponentModel.BackgroundWorker backgroundWorkerDelBooks;
		private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem добавитьАвтораToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem управлениеКатегориямиToolStripMenuItem;
     
    }
}

