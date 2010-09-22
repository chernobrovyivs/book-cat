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
using System.Windows.Shapes;

namespace BookCat
{
    /// <summary>
    /// Interaction logic for Genres.xaml
    /// </summary>
    public partial class Genres : Window
    {
        public CGenre origGenre;
        public CGenre editGenre;

        public Genres()
        {
            InitializeComponent();
        }

        public void SetForEdit(CGenre _orig)
        {
            origGenre = _orig;
            editGenre = new CGenre();
            editGenre .CopyFrom(origGenre);

            grdMain.DataContext = editGenre;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            origGenre.CopyFrom(editGenre);
            this.DialogResult = true;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
