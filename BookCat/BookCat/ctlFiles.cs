using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
    public partial class ctlFiles : UserControl
    {
        public ctlFiles()
        {
            InitializeComponent();

            /*
             * File_id
             * Object_id
             * Object_mc
             * Name
             * Extension
             * Contents
             * Size
             * Crc
             * Description
             * WasPath             
             */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog {Multiselect = true};
            fd.ShowDialog();

            foreach(string s in fd.FileNames)
            {
                if (!listBox1.Items.Contains(Path.GetFileName(s)))
                listBox1.Items.Add(Path.GetFileName(s));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            listBox1.Items.Remove(listBox1.SelectedItem);
            if (i > 0) 
            {
                listBox1.SelectedIndex = i - 1;
            }
            else if (listBox1.Items.Count >0)
            {
                listBox1.SelectedIndex = 0;
            }
        }
    }
}
