using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookCat
{
	public class StringCollection : ObservableCollection<string>
	{

	}

	/// <summary>
	/// Interaction logic for PageCover.xaml
	/// </summary>
	public partial class PageCover : UserControl
	{

		private StringCollection _dataSource;

		public PageCover()
		{
			InitializeComponent();
		}

		public class MyBook
		{
			public string FirstName { get; set; }
		}

		public void SetItemSource(ObservableCollection<Book> _book)
		{
			_dataSource.Clear();

			/*
		  * 
			ObservableCollection<string> s = new ObservableCollection<string>();
			foreach(Book st in _book)
			{
				s.Add(st.Book_guid.ToString());
			}

			_elementFlow.ItemsSource = _book;()*/
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			List<MyBook> s = new List<MyBook>();

			s.Add(new MyBook { FirstName = "fdfdf" });
			s.Add(new MyBook { FirstName = "gf" });


			//StringCollection 

			//_elementFlow.SelectedIndexChanged += EFSelectedIndexChanged;
			//_elementFlow.SelectedIndex = 0;

			_dataSource = FindResource("TestDataSource") as StringCollection;
			//_dataSource = new StringCollection();


			Button b = sender as Button;
			//int index = _randomizer.Next(_dataSource.Count);
			//if (b.Name == "_regular")
			//{
			_dataSource.Insert(0, "/ElementFlow/Images/01.jpg");
			_dataSource.Insert(0, "/ElementFlow/Images/01.jpg");
			_dataSource.Insert(0, "/ElementFlow/Images/01.jpg");
			_dataSource.Insert(0, "/ElementFlow/Images/01.jpg");
			_dataSource.Insert(0, "/ElementFlow/Images/01.jpg");
			_dataSource.Insert(0, "/ElementFlow/Images/01.jpg");
			//}
			//else
			//{
			//_dataSource.Insert(index, string.Format("/ElementFlow/Images/{0:00}", _randomizer.Next(1, 13)) + ".jpg");
			//}

			// Update selectedindex slider
			//_selectedIndexSlider.Maximum = _elementFlow.Items.Count - 1;

		}
	}
}
