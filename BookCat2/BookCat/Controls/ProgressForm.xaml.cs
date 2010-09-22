using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace BookCat.Controls
{
	/// <summary>
	/// Interaction logic for ProgressForm.xaml
	/// </summary>
	public partial class ProgressForm : Window
	{
		private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

		public ProgressForm()
		{
			InitializeComponent();

			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0,0,0,0,500);
			dispatcherTimer.Start();
		}


		public StartupParams sp;
		private bool userPressedBreak = false;

		// Через это запускаемся
		public static ProgressObject Run(int _len, string _caption, bool _addTenPercent)
		{
			ProgressObject o = ProgressObjectManager.GetNew();

			StartupParams sp = new StartupParams
			                   	{
			                   		len = _len, 
									caption = _caption, 
									addTenPercent = _addTenPercent,
									progressObject = o
			                   	};

			System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(new NextPrimeDelegate(Init), sp);
			//ThreadPool.QueueUserWorkItem(Init, sp);

			return o;
		}

		public delegate void NextPrimeDelegate(object o);

		public static ProgressObject Run(int _len, bool _addTenPercent)
		{
			return Run(_len, "", _addTenPercent);
		}

		public static ProgressObject Run(int _len)
		{
			return Run(_len, "", false);
		}

		public static ProgressObject Run()
		{
			return Run(100, "", false);
		}

		public static ProgressObject Run(string _caption)
		{
			return Run(100, _caption, false);
		}

		// Стартовые параметры
		public class StartupParams
		{
			public int len;
			public string caption="";
			public bool addTenPercent = false;
			public ProgressObject progressObject;
		}

		// Здесь ициализируется новый поток с параметрами
		private static void Init(Object stateInfo)
		{
			StartupParams sp = (StartupParams)stateInfo;

			// Пауза полторы секунды перед показыванием диалога, либо если работа завершилась, не показываем диалог
			Thread.Sleep(1500);

			if (!sp.progressObject.GetMessage().ForceClose)
			{
				ProgressForm f = new ProgressForm {sp = sp};
				f.Show();
			}

			ProgressObjectManager.Remove(sp.progressObject);
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			// Forcing the CommandManager to raise the RequerySuggested event
			// CommandManager.InvalidateRequerySuggested();
			PMes pme = sp.progressObject.GetMessage();

			if (!userPressedBreak)
			{
				if (pme.Caption != "")
					baseLabel1.Content = pme.Caption;

				if (pme.IsBreakable == false) lnkCancel.Visibility = Visibility.Hidden;
				else lnkCancel.Visibility = Visibility.Visible;
			}

			if (sp.addTenPercent)
			{
				progressBar1.Maximum = pme.Len + Convert.ToInt32(pme.Len/10);
			}
			else
			{
				progressBar1.Maximum = pme.Len;
			}

			progressBar1.Value = pme.Step;

			if ((pme.Step > progressBar1.Maximum && progressBar1.Maximum!=0) || pme.ForceClose)
			{
				Close();
			}

			WpfApplication.DoEvents();
		}


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			progressBar1.Minimum = 0;
			if (sp.len != 0) progressBar1.Maximum = sp.len;
			else progressBar1.Maximum = 100;
			//progressBar1.Step = 1;
			lnkCancel.Visibility = Visibility.Hidden;

			baseLabel1.Content = sp.caption != "" ? sp.caption : "Пожалуйста, ждите...";
			
			Topmost = true;
		}

		private void lnkCancel_Click(object sender, RoutedEventArgs e)
		{
			baseLabel1.Content = "Прерывание...";

			sp.progressObject.Cancel();

			userPressedBreak = true;
			lnkCancel.Visibility = Visibility.Hidden;
		}
	}
}
