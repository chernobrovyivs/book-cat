using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
	public partial class fDialogParse : Form
	{
		public fDialogParse(string message)
		{
			InitializeComponent();

			label1.Text = message;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Ignore;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
