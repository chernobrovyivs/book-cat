using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using BookCat.Properties;
using Path = System.IO.Path;

namespace BookCat
{
    /// <summary>
    /// Interaction logic for WindowSearch.xaml
    /// </summary>
    public partial class WindowSearch : Window
    {
        private Thread t;

        private ObservableCollection<Result> lstResult = new ObservableCollection<Result>();


        public WindowSearch()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbSearchesDll1.GetSearchDlls();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
			if (Settings.Default.LocalDumbComputer)
			{
				XmlSerializer r = new XmlSerializer(typeof(List<Result>));
				using (FileStream fs = new FileStream(Path.Combine(WpfApplication.StartupPath, "resultList3.xml"), FileMode.Open))
				{
					lstResult = new ObservableCollection<Result>((List<Result>)r.Deserialize(fs));
				}				
			}
			else
			{
				StartSearch(txtQuery.Text);
			}

			ParseResults();
        }

		void ParseResults()
		{
			treeViewResults.Items.Clear();
			//treeViewResults.Items.Add(new TreeViewItem { Header = "Ozonchik", IsExpanded = true });

			foreach (Result result in lstResult)
			{
				//bool wasFound = false;
				TreeViewItem found = null;
				foreach (TreeViewItem tvi in treeViewResults.Items)
				{
					if (tvi.Header.ToString() == result.SourceSite)
					{
						found = tvi;
						break;
					}
				}

				if (found==null)
				{
					found = new TreeViewItem { Header = result.SourceSite, IsExpanded = true };
					treeViewResults.Items.Add(found);
				}

				int num = 0;
				foreach (Result result2 in lstResult)
				{
					if (result.SourceSite == result2.SourceSite)
					{
						num++;
					}
				}

				int imageIndex = 0;
				string sourceSite = result.SourceSite;
				if (sourceSite != null)
				{
					switch (sourceSite)
					{
						case "Amazon.com":
							imageIndex = 1;
							break;
						case "Barnes&Noble.com":
							imageIndex = 2;
							break;
						case "Books.ru":
							imageIndex = 3;
							break;
						case "Ozon.ru":
							imageIndex = 4;
							break;
						case "Safari.oreilly.com":
							imageIndex = 5;
							break;
						case "Books.Google.com":
							imageIndex = 6;
							break;
					}

				}

				//found.Items.Add(new TreeViewItem { Header = imageIndex + " " + result.ToString() + result.Genres.Any() });
				

				/*
				ScrollViewer myScrollViewer = new ScrollViewer() {Width = };
				myScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

				// Add Layout control
				StackPanel myStackPanel = new StackPanel();
				myStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
				myStackPanel.VerticalAlignment = VerticalAlignment.Top;

				TextBlock myTextBlock = new TextBlock();
				myTextBlock.TextWrapping = TextWrapping.Wrap;
				myTextBlock.Margin = new Thickness(0, 0, 0, 20);
				myTextBlock.Text = "Scrolling is enabled when it is necessary. Resize the Window, making it larger and smaller. Scrolling is enabled when it is necessary. Resize the Window, making it larger and smaller. Scrolling is enabled when it is necessary. Resize the Window, making it larger and smaller. Scrolling is enabled when it is necessary. Resize the Window, making it larger and smaller. Scrolling is enabled when it is necessary. Resize the Window, making it larger and smaller.";

				Rectangle myRectangle = new Rectangle();
				myRectangle.Fill = Brushes.Red;
				myRectangle.Width = 500;
				myRectangle.Height = 500;

				// Add child elements to the parent StackPanel
				myStackPanel.Children.Add(myTextBlock);
				myStackPanel.Children.Add(myRectangle);

				// Add the StackPanel as the lone Child of the Border
				myScrollViewer.Content = myStackPanel;

				found.Items.Add(myScrollViewer);

*/
				
				
				
				WrapPanel dp = new WrapPanel() { Width = 700 };


				if (!Settings.Default.LocalDumbComputer)
				{
                    if (result.LargeImageUrl != "")
                    {

                        Image i = new Image()
                                      {Source = new BitmapImage(new Uri(result.LargeImageUrl)) {DecodePixelWidth = 150}};


                        

                        dp.Children.Add(new Border() {Width = 150, Child = i});
                    }

				}

				TextBlock textBlock1 = new TextBlock {TextWrapping = TextWrapping.WrapWithOverflow, Margin = new Thickness(5,0,0,10)};
				textBlock1.Inlines.Add(new Bold(new Run(result.ToString())));
				textBlock1.Inlines.Add(new LineBreak());
				textBlock1.Inlines.Add(new Italic(new Run(result.Description)));

                dp.Children.Add(new Border() { Width = 500, Child = textBlock1 });


				//found.Items.Add(new TreeViewItem { Header = imageIndex + " " + result.ToString() + result.Genres.Any() });
				found.Items.Add(dp);
				
				/*
				    <DockPanel>
      <Image Source="data\cat.png"/>
      <TextBlock Margin="5" Foreground="Brown"
                 FontSize="12">Cat</TextBlock>
    </DockPanel>
*/

				
				//((TreeViewItem)treeViewResults.Items[0]).Items.Add(new TreeViewItem() { Header = result.ToString() });
				//treeViewResults.Nodes.Add(result.SourceSite, result.SourceSite + " [" + num.ToString() + "]", imageIndex, imageIndex);
				//treeViewResults.Nodes[result.SourceSite].Nodes.Add(result.ToString(), result.ToString()).Tag = result;
			}			
		}

        private void StartSearch(string searchString)
        {
            try
            {
                lstResult.Clear();

                foreach (SitesDll dll in lbSearchesDll1.CheckedItems)
                {
					foreach (XmlDocument document in SitesDll.GetSearchResults(searchString, dll.Path))
                    {
                        if (document.DocumentElement != null)
                        {
                            foreach(Result r in ProcessXml.XMLtoListResults(document, dll.Name))
                            {
                                lstResult.Add(r);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }




    }
}
