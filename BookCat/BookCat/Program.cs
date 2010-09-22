using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BaseControl.TRHtmlGridBox;

namespace BookCat
{
    static class Program
    {
		public static string connString = @"data source=" + Application.StartupPath + @"\db";
		public static string s_Path = Application.StartupPath + @"\Images\";
		public static UserSettings us;
    	public static string xmlFullPath = Application.StartupPath;
		
		/// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
