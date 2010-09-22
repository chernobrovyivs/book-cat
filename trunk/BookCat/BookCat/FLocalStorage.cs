using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BookCat
{
	public partial class FLocalStorage : Form
	{
		public FLocalStorage()
		{
			InitializeComponent();

			txtPath.Text = Program.us.txtLocalStore;
			txtExts.Text = String.Join("; ", Program.us.Exts.ToArray());
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fd = new FolderBrowserDialog();
			if (DialogResult.OK == fd.ShowDialog())
			{
				txtPath.Text = fd.SelectedPath;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(txtPath.Text))
			{
				MessageBox.Show("Ошибка. Указанный путь не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Program.us.txtLocalStore = txtPath.Text;

			string[] ss = txtExts.Text.Split(new[]{';'},StringSplitOptions.RemoveEmptyEntries);
			Program.us.Exts = new List<string>();

			foreach (string s in ss)
			{
				string s2 = s.Replace("\r", "").Replace("\n", "").Trim();
				Program.us.Exts.Add(s2);
			}

			Program.us.serializeToXml(Program.xmlFullPath);

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
