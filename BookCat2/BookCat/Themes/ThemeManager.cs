using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BookCat.Themes
{
    public static class ThemeManager
    {
        public static void ApplyTheme(this ContentControl app, string theme)
        {
            ResourceDictionary dictionary = ThemeManager.GetThemeResourceDictionary(theme);

            if (dictionary != null)
            {
                app.Resources.MergedDictionaries.Clear();
                app.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        public static ResourceDictionary GetThemeResourceDictionary(string theme)
        {
            if (theme != null)
            {
                //Assembly assembly = Assembly.LoadFrom("WPF.Themes.dll");
                //string packUri = String.Format(@"/WPF.Themes;component/{0}/Theme.xaml", theme);
                string packUri = @"/BookCat;component/Themes/Theme.xaml";
                return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
            }
            return null;
        }

        public static void Set(DependencyObject d)
        {
            string theme = "ExpressionDark";
            ContentControl control = d as ContentControl;
            if (control != null)
            {
                control.ApplyTheme(theme);
            }            
        }
    }
}
