namespace BookCat
{
	partial class ctlStatusProgress
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlStatusProgress));
			this.pbStatus = new System.Windows.Forms.ProgressBar();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pbStatus
			// 
			this.pbStatus.Location = new System.Drawing.Point(32, 27);
			this.pbStatus.Name = "pbStatus";
			this.pbStatus.Size = new System.Drawing.Size(434, 13);
			this.pbStatus.TabIndex = 21;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblStatus.Location = new System.Drawing.Point(29, 11);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(25, 13);
			this.lblStatus.TabIndex = 22;
			this.lblStatus.Text = "      ";
			// 
			// ctlStatusProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.pbStatus);
			this.Controls.Add(this.lblStatus);
			this.MaximumSize = new System.Drawing.Size(500, 46);
			this.MinimumSize = new System.Drawing.Size(500, 46);
			this.Name = "ctlStatusProgress";
			this.Size = new System.Drawing.Size(500, 46);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbStatus;
		private System.Windows.Forms.Label lblStatus;
	}
}
