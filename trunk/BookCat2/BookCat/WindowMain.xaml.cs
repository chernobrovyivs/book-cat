using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using BookCat.Controls;
using BookWriter3D;
using FluidKit.Controls;
using System.Data.EntityClient;
using System.Data;
using System.IO;
using Ookii.Dialogs.Wpf;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace BookCat
{
    /// <summary>
	/// Interaction logic for WindowMain.xaml
	/// </summary>
	public partial class WindowMain : Window
	{
		//private readonly CurrentViewModeManager currentViewModeManager = new CurrentViewModeManager();
		//public static readonly CurrentDataAccessManager currentDataAccessManager = new CurrentDataAccessManager();

        public ObservableCollection<CGenre> genres;


		public WindowMain()
		{
			InitializeComponent();
		}

        GenreViewModel m;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            lv.HorizontalAlignment = HorizontalAlignment.Stretch;
            GridView g = new GridView();
            g.Columns.Add(new GridViewColumn { Header = "Guid", Width = 100, DisplayMemberBinding = new Binding("Book_guid") });
            g.Columns.Add(new GridViewColumn { Header = "Name", Width = 100, DisplayMemberBinding = new Binding("Name") });
            lv.View = g;
            lv.ItemsSource = CurrentDataAccessManager.current.getAllBooks(); // c.Book.Take(5).ToList();

		    genres = CurrentDataAccessManager.current.getAllGenres();

		    cntAbout.DataContext = genres;

		    listBox1.ItemsSource = genres;

            /*
            treeView1.ItemsSource = new ListCollectionView(genres)
            {
                Filter = delegate(object p)
                {
                    return (p == null || ((CGenre)p).Top_guid == Guid.Empty);
                }
            };

			treeView1.SelectedItemChanged += SelectedChanged;
            */
            lbx.ItemsSource = CurrentDataAccessManager.current.getAllBooks();



            

            lv.PreviewMouseLeftButtonDown += DragSource_PreviewMouseLeftButtonDown;
            lv.PreviewMouseMove += DragSource_PreviewMouseMove;

            //treeView1.AllowDrop = true;
            //treeView1.Drop += DropTargetText_Drop;
            //treeView1.DragOver += treeView1_DragOver;

            treeView2.AllowDrop = true;
            treeView2.Drop += DropTargetText_Drop;
            treeView2.DragOver += treeView1_DragOver;


		    //gg = new CGenre();
            //gg.Children.Add(new CGenre() { Name = "as" });
            //gg.Children.Add(new CGenre() { Name = "as" });

            //foreach(CGenre cg in genres)
            //{
            //    gg.Children.Add(new CGenre() { Name = cg.Name, Genre_guid = cg.Genre_guid, Top_guid = cg.Top_guid, About = cg.About});
            //}

            //gg.fullGenres = genres;

		    //GenreViewModel.genres = genres;


            m = new GenreViewModel(genres);
            treeView2.DataContext = m;

		}

        void treeView1_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("Text"))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
               
            }

            // Verify that this is a valid drop and then store the drop target
            TreeViewItem item = GetNearestContainer(e.OriginalSource as UIElement);

            //TreeViewItem ti = e.Source as TreeViewItem;

            if (item==null)
            {
                return;
            }

            item.IsExpanded = true;
            item.IsSelected = true;

            //CGenre cg = (CGenre)item.Header; 

            //MessageBox.Show(cg.Name);

            //            MessageBox.Show(e.GetPosition(treeView1).X.ToString());
        }

        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }


        private Point _startPoint;
        private bool _isDragging;
        public bool IsDragging
        {
            get { return _isDragging; }
            set { _isDragging = value; }
        } 

        void DropTargetText_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = e.Data;

            if (data.GetDataPresent(DataFormats.Text))
            {
                string[] arr = ((string) data.GetData(DataFormats.Text)).Split(',');

                if (arr.Count() == 0 || treeView2.SelectedItem == null) return;

                var curGenre = ((GenreViewModel) treeView2.SelectedItem);

                if (MessageBox.Show(String.Format("Действительно перенести {0} книг(у,и) в категорию '{1}'?", arr.Count(), curGenre.Name), "Вопрос",MessageBoxButton.YesNo,MessageBoxImage.Information)==MessageBoxResult.Yes)
                {
                    List<Guid> bookGuids = arr.Select(sg => new Guid(sg)).ToList();

                    CurrentDataAccessManager.current.updateBookGenre(curGenre.Genre_guid, bookGuids);

                    lv.ItemsSource = CurrentDataAccessManager.current.getBooksByGenreGuid(curGenre.Genre_guid);
                }

            }

        }

        void DragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        
        void DragSource_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !IsDragging)
            {
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag(e);

                }
            }
        }



        private ListViewItem GetNearestContainerLv(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            ListViewItem container = element as ListViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as ListViewItem;
            }
            return container;
        }
        
        private void StartDrag(MouseEventArgs e)
        {
            if (lv.SelectedItems.Count==0) return;

            ListViewItem item = GetNearestContainerLv(e.OriginalSource as UIElement);
            // драг колонки
            if (item == null) return;

            IsDragging = true;

            var guids = (from Book bo in lv.SelectedItems select bo.Book_guid.ToString()).ToArray();
            string s = String.Join(",", guids);

            //DataObject data = new DataObject(System.Windows.DataFormats.Text.ToString(), "abcd");
            DataObject data = new DataObject(System.Windows.DataFormats.Text.ToString(), s /*.Book_guid.ToString()*/);
            DragDropEffects de = DragDrop.DoDragDrop(this.treeView2, data, DragDropEffects.Move);
            IsDragging = false;
        }
        










		void  CanCopyExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
 			//throw new NotImplementedException();
		}

		void  CopyCommandHandler(object sender, ExecutedRoutedEventArgs e)
		{
 			throw new NotImplementedException();
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "";
#if DEBUG
            //dialog.SelectedPath = @"C:\Other\Maggi\!!sorted";
#endif

            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            //if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
             //   MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");

            List<string> bp = new List<string>();

            if ((bool)dialog.ShowDialog(this))
            {
                //ProcessDirectory(dialog.SelectedPath, bp);
            }




                for (int x = 0; x < bp.Count; x++)
                {
                    string s = bp[x];
                    // Periodically check CancellationPending and abort the operation if required.
                    //if (_sampleProgressDialog.CancellationPending) return;
                    // ReportProgress can also modify the main text and description; pass null to leave them unchanged.
                    // If _sampleProgressDialog.ShowTimeRemaining is set to true, the time will automatically be calculated based on
                    // the frequency of the calls to ReportProgress.
                    Book b = new Book();
                    b.Book_guid = Guid.NewGuid();
                    b.Name = System.IO.Path.GetFileNameWithoutExtension(s);
                    b.File_path = s;
                    b.File_name = System.IO.Path.GetFileName(s);
                    b.Crc = Crc32.Compute(File.ReadAllBytes(s));

                    CurrentDataAccessManager.current.AddToBook(b);

                    //_sampleProgressDialog.ReportProgress(Convert.ToInt32((x / bp.Count) * 100), null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Processing: {0}%", Convert.ToInt32((x / bp.Count) * 100)));
                    //_sampleProgressDialog.ReportProgress(Convert.ToInt32((x / bp.Count) * 100), null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Processing: {0}%", s));
                    
					//label1.Content = Convert.ToInt32(((double)x / bp.Count) * 100) + "% " + System.IO.Path.GetFileNameWithoutExtension(s);

                    //WpfApplication.DoEvents();

                }


            /*
            int i = 0;
            foreach (string s in bp)
            {
                i++;
                //_sampleProgressDialog.ReportProgress(Convert.ToInt32((i / bp.Count) * 100), null, string.Format(System.Globalization.CultureInfo.CurrentCulture, "Processing: {0}%", (i / bp.Count) * 100));

                //_sampleProgressDialog.ReportProgress(
                //using (TextReader tr = new StreamReader(s))
                //{
                  //  tr.R
                //}

                //uint bFile_CRC = 0;
                //using (FileStream fs = new FileStream(s, FileMode.Open))
                //{

                //bFile_CRC = Crc32.Compute(File.ReadAllBytes(s));
                //}

                Book b = new Book();
                b.Book_guid = Guid.NewGuid();

                b.Name = System.IO.Path.GetFileNameWithoutExtension(s);
                b.File_path = s;
                b.File_name = System.IO.Path.GetFileName(s);
                b.Crc = Crc32.Compute(File.ReadAllBytes(s));

                c.AddToBook(b);



                //MessageBox.Show(bFile_CRC.ToString());
            }
            */
                int k = CurrentDataAccessManager.current.SaveChanges();
            MessageBox.Show(k.ToString() + " affected");

            /*
            FolderBrowserDialog fd = new FolderBrowserDialog { ShowNewFolderButton = false };

            ProcessArgs pa = new ProcessArgs();

            List<Pair<Book, string>> bp = new List<Pair<Book, string>>();


            if (DialogResult.OK == fd.ShowDialog())
            {
                // получаем список книг
                ProcessDirectory(fd.SelectedPath, pa, bp);

                // добавляем
                backgroundWorkerAddBooks.RunWorkerAsync(bp);
            }
            */






            ListView l = new ListView();
			l.HorizontalAlignment=HorizontalAlignment.Stretch;

			GridView g = new GridView();
			g.Columns.Add(new GridViewColumn { Header = "fdfdfdfdf", Width = 100 });
			g.Columns.Add(new GridViewColumn { Header = "fdfdfdfdf", Width = 100 });
			g.Columns.Add(new GridViewColumn { Header = "fdfdfdfdf", Width = 100 });

			l.View =g;




			//frame1.Content = l;
		}

		private void button3_Click(object sender, RoutedEventArgs e)
		{
			using (ProgressObject po = ProgressObject.Run())
			{
				Thread.Sleep(2000);
				po.AddMessage(10, 100, "111", false);
				Thread.Sleep(2000);
				po.AddMessage(10, 100, "222", false);
				Thread.Sleep(2000);
				po.AddMessage(10, 100, "333", false);
				Thread.Sleep(2000);
				po.AddMessage(10, 100, "444", false);
				Thread.Sleep(2000);
			}
			//po.ForceClose();
		}


		#region Commands
		private void cBookAddFromFiles(object sender, ExecutedRoutedEventArgs e)
    	{
    		throw new NotImplementedException();
    	}

    	private void cBookAddFromFolder(object sender, ExecutedRoutedEventArgs e)
    	{
			VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
			dialog.Description = "";
			dialog.UseDescriptionForTitle = true; 
			// This applies to the Vista style dialog only, not the old dialog.
			//if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
			//   MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");

			List<string> bp = new List<string>();

			if ((bool)dialog.ShowDialog(this))
			{
				Files.ProcessDirectory(dialog.SelectedPath, bp);

				for (int x = 0; x < bp.Count; x++)
				{
					string s = bp[x];
					// Periodically check CancellationPending and abort the operation if required.
					//if (_sampleProgressDialog.CancellationPending) return;
					// ReportProgress can also modify the main text and description; pass null to leave them unchanged.
					// If _sampleProgressDialog.ShowTimeRemaining is set to true, the time will automatically be calculated based on
					// the frequency of the calls to ReportProgress.
					Book b = new Book();
					b.Book_guid = Guid.NewGuid();
					b.Name = System.IO.Path.GetFileNameWithoutExtension(s);
					b.File_path = s;
					b.File_name = System.IO.Path.GetFileName(s);
					b.Crc = Crc32.Compute(File.ReadAllBytes(s));

					CurrentDataAccessManager.current.AddToBook(b);
				}

				int k = CurrentDataAccessManager.current.SaveChanges();
				MessageBox.Show(k.ToString() + " книг добавлено");
	
			}
		}

    	private void cBookAddManual(object sender, ExecutedRoutedEventArgs e)
    	{
    		throw new NotImplementedException();
		}
		#endregion

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			WindowBook wb = new WindowBook();
			wb.Show();
			//BookWriter3D.MainWindow mw = new MainWindow();
			//mw.Show();
				
			//WindowSearch ws = new WindowSearch();
			//ws.Show();
		}

        private void lv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowBook wb = new WindowBook();
            wb.book = (Book)lv.SelectedItem;
            wb.ShowDialog();
            //MessageBox.Show((.Book_guid.ToString());
        }

        public void add(CGenre parent_id)
        {
            if (parent_id != null) parent_id.IsExpanded = true;

            CGenre ci = new CGenre();
            ci.Genre_guid = Guid.NewGuid();
            ci.parent = genres;
            if (parent_id != null)
            {
                ci.Top_guid = parent_id.Genre_guid;
            }
            ci.IsExpanded = true;
            ci.IsSelected = true;

            Genres w = new Genres();
            w.SetForEdit(ci);
            if (w.ShowDialog() == true)
            {
                CurrentDataAccessManager.current.AddToGenre(ci);
                genres.Add(ci);
            }
            

            //genres.Add(ci);// = CurrentDataAccessManager.current.getAllGenres();

            //personRepositary.InsertCItem();
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            add(treeView2.SelectedItem as CGenre);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (treeView2.SelectedItem != null)
            {
                if (MessageBox.Show("Действительно удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    CGenre c = (CGenre)treeView2.SelectedItem;

                    if (genres.Any(k => k.Top_guid == c.Genre_guid))
                    {
                        MessageBox.Show("Невозможно удалить, т.к. существуют потомки", "Внимание", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }

                    genres.Remove((CGenre)treeView2.SelectedItem);
                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            CGenre ci = treeView2.SelectedItem as CGenre;
            if (ci != null)
            {
                foreach (var cItem in genres)
                {
                    if (cItem.Genre_guid == ci.Top_guid)
                    {
                        ci = cItem;
                        break;
                    }
                }

                if (ci.Top_guid == Guid.Empty) ci = null;
            }

            add(ci);
        }

        private void treeView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*
            if (treeView1.SelectedItem == null) return;

            CGenre cur = (CGenre)treeView1.SelectedItem;

            Genres w = new Genres();
            w.SetForEdit(cur);

            if (w.ShowDialog() == true)
            {
                CurrentDataAccessManager.current.UpdateCGenre(cur);
            }
            */
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            //if (treeView1.SelectedItem == null) return;

            //TreeViewItem item = GetNearestContainer(treeView1.SelectedItem as UIElement);

            //var k = treeView1.ItemContainerGenerator.ContainerFromItem(treeView1.SelectedItem);
            //var k = treeView2.ItemContainerGenerator.ContainerFromItem(treeView2.SelectedItem);

        }

        private void button6_Click_1(object sender, RoutedEventArgs e)
        {
            if (treeView2.SelectedItem == null) return;

            CGenre cur = ((GenreViewModel)treeView2.SelectedItem).Genre;

            Genres w = new Genres();
            w.SetForEdit(cur);

            if (w.ShowDialog() == true)
            {
                CurrentDataAccessManager.current.UpdateCGenre(cur);
            }
        }

        private void DoStandartGenres()
        {
            switch (lstStandartGenres.SelectedIndex)
            {
                case 0:
                    lv.ItemsSource = CurrentDataAccessManager.current.getAllBooks(); // c.Book.Take(5).ToList();
                    break;
                case 1:
                    lv.ItemsSource = CurrentDataAccessManager.current.getNewBooks(); // c.Book.Take(5).ToList();
                    break;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoStandartGenres();
        }

        private void treeView2_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null || IsDragging) return;

            lv.ItemsSource = CurrentDataAccessManager.current.getBooksByGenreGuid(((GenreViewModel)e.NewValue).Genre_guid);
        }

        private void treeView2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (treeView2.SelectedItem == null || IsDragging) return;

            lv.ItemsSource = CurrentDataAccessManager.current.getBooksByGenreGuid(((GenreViewModel)treeView2.SelectedItem).Genre_guid);
        }

        private void lstStandartGenres_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lstStandartGenres.SelectedItem == null || IsDragging) return;

            DoStandartGenres();
        }
	}
}
