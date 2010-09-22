using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ookii.Dialogs.Wpf;

namespace BookCat
{
	/// <summary>
	/// Interaction logic for WindowBook.xaml
	/// </summary>
	public partial class WindowBook : Window
	{
        public Book book;

		public WindowBook()
		{
			InitializeComponent();
		}

        public WindowBook(Book _book) : this()
        {
            book = _book;
        }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            Themes.ThemeManager.Set(this);
            canvas3D.Child = new BookWriter3D.Book3D { Width = canvas3D.ActualWidth, Height = canvas3D.ActualHeight };

		    grdMain.DataContext = book;
		}

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            WindowSearch ws = new WindowSearch();
            ws.ShowDialog();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(book.File_path);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            CurrentDataAccessManager.current.SaveChanges();
            this.DialogResult = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CurrentDataAccessManager.current.Undo(book);
            this.DialogResult = false;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            VistaOpenFileDialog f = new VistaOpenFileDialog();
            if (f.ShowDialog() == true)
            {
                MessageBox.Show(f.FileName);
            }
        }
	}
}
