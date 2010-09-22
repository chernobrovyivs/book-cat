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

namespace BookCat
{
    public class CatCommands
    {
		public static RoutedUICommand BookAddFromFiles { get; private set; }
		public static RoutedUICommand BookAddFromFolder { get; private set; }
		public static RoutedUICommand BookAddManual { get; private set; }
		public static RoutedUICommand Exit { get; private set; }

        static CatCommands()
        {
			BookAddFromFiles = new RoutedUICommand("Добавить книги, указав файлы...", "BookAddFromFiles", typeof(CatCommands), null);
			BookAddFromFolder = new RoutedUICommand("Добавить книги, указав каталог...", "BookAddFromFolder", typeof(CatCommands), null);
			BookAddManual = new RoutedUICommand("Добавить книгу вручную", "BookAddManual", typeof(CatCommands), null);
			
            Exit = new RoutedUICommand("E_xit", "Exit", typeof(CatCommands), new InputGestureCollection { new KeyGesture(Key.X, ModifierKeys.Control, "Ctrl+X") });
        }
    }
}
