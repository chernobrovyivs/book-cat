using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using BookCat.Properties;
using Path = System.IO.Path;

namespace BookCat
{
    /// <summary>
    /// Interaction logic for ctlSearchProviders.xaml
    /// </summary>
    public partial class CtlSearchProviders : UserControl
    {
		ObservableCollection<CheckedListItem> lstCheckedSites = new ObservableCollection<CheckedListItem>();

        public CtlSearchProviders()
        {
            InitializeComponent();

            //lstCheckedSites.Add(new CheckedListItem() { Id = 32, IsChecked = true, Name = "jhj" });
            //GetSearchDlls();

            lst.ItemsSource = lstCheckedSites;
        }

        public class CheckedListItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }

            public SitesDll Site { get; set; }
        }

        public void GetCheckedSites()
        {
            foreach (string str in Settings.Default.strSearchSites.Split(new char[] { Convert.ToChar("|") }))
            {
                lstCheckedSites.Add(new CheckedListItem(){Name= str});
            }
        }

        public IEnumerable<SitesDll> CheckedItems
        {
            get 
            {
                return 
                    from ch in lstCheckedSites where ch.IsChecked select ch.Site;
            }
        }

        public void GetSearchDlls()
        {
            //this.GetCheckedSites();
            DirectoryInfo info = new DirectoryInfo(Path.Combine(WpfApplication.StartupPath, "Search_Dll"));
            if (!info.Exists)
            {
                MessageBox.Show("StringResources.LoadDll_err", "StringResources.AEM_exception_title", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            foreach (FileInfo info2 in info.GetFiles("*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(info2.FullName);
                    string name = Path.GetFileNameWithoutExtension(info2.Name) + "." + Path.GetFileNameWithoutExtension(info2.Name);
                    Type type = assembly.GetType(name, true, true);
                    if (type != null)
                    {
                        type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                        string str2 = (string)type.GetMethod("GetDesription", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly).Invoke(null, null);
                        SitesDll item = new SitesDll(str2, info2.FullName);
                        //lstCheckedSites.Add(new CheckedListItem() { Site = item, Name = item.Name, IsChecked = Settings.Default.strSearchSites.Contains(str2) });
						lstCheckedSites.Add(new CheckedListItem() { Site = item, Name = item.Name, IsChecked = true });
					}
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "StringResources.AEM_exception_title", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }

            lst.ItemsSource = lstCheckedSites;
        }

        public void SetCheckedSites()
        {
            /*
            lstCheckedSites.Clear();
            foreach (CheckedListItem obj2 in lstCheckedSites)
            {
                this.lstCheckedSites.Add(obj2.ToString());
            }
            Settings.Default.strSearchSites = string.Empty;
            foreach (string str in this.lstCheckedSites)
            {
                Settings settings1 = Settings.Default;
                settings1.strSearchSites = settings1.strSearchSites + str + "|";
            }
            Settings.Default.Save();
             */ 
        }
    }
}
