﻿namespace BookCat
{
    partial class fCategory
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCategory));
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.bookCatTree1 = new BookCat.BookCatTree();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(336, 438);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(144, 20);
			this.button2.TabIndex = 11;
			this.button2.Text = "OK";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(13, 393);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(467, 28);
			this.label1.TabIndex = 14;
			this.label1.Text = "Для добавления новых категорий, или удаления существующих, щелкните правой кнопко" +
				"й мышки на существующем заголовке.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// bookCatTree1
			// 
			this.bookCatTree1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.bookCatTree1.EditMode = true;
			this.bookCatTree1.Location = new System.Drawing.Point(12, 12);
			this.bookCatTree1.Name = "bookCatTree1";
			this.bookCatTree1.Size = new System.Drawing.Size(468, 378);
			this.bookCatTree1.TabIndex = 13;
			// 
			// fCategory
			// 
			this.AcceptButton = this.button2;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(492, 470);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bookCatTree1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "fCategory";
			this.Text = "Категории";
			this.Load += new System.EventHandler(this.fAddBook_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private BookCatTree bookCatTree1;
		private System.Windows.Forms.Label label1;
    }
}