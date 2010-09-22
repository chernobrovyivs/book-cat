using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BookCat.Controls
{
	public class ProgressObjectManager
	{
		private static List<ProgressObject> lomg = new List<ProgressObject>();

		public static List<ProgressObject> List
		{
			get
			{
				return lomg;
			}
		}

		public static ProgressObject GetNew()
		{
			foreach (ProgressObject o in lomg)
			{
				o.Dispose();
			}
			lomg.Clear();

			ProgressObject po = new ProgressObject();
			lomg.Add(po);
			return po;
		}


		public static ProgressObject GetMy(ProgressForm pf)
		{
			//lomg[0].CleanMessage();
			return lomg[0];
		}

		public static void Add(ProgressObject po)
		{
			lomg.Add(po);
		}

		public static void Remove(ProgressObject po)
		{
			lomg.Remove(po);
		}

		public static void ShowBreakMessage()
		{
			MessageBox.Show("Формирование отчета прервано пользователем", "Внимание",MessageBoxButton.OK,MessageBoxImage.Information);
		}
	}



	// Класс  пишем/читаем сообщения
	/// <summary>
	/// Класс, в который пишем сообщения через AddMessage
	/// </summary>
	public class ProgressObject : IDisposable
	{
		private static object lockObj = new object();
		private PMes pm = new PMes();

		private bool Breaked = false;

		public static ProgressObject Run()
		{
			return ProgressForm.Run(100, "", false);
		}

		/// <summary>
		/// Нажал ли пользователь на кнопку "отмена".  Форма всё ещё должна быть закрыта позже через ForceClose()
		/// </summary>
		public bool isCancelPressed
		{
			get
			{
				lock (lockObj)
				{
					return Breaked;
				}
			}
		}

		public void ForceClose()
		{
			lock (lockObj)
			{
				pm.ForceClose = true;
			}
		}

		internal PMes GetMessage()
		{
			lock (lockObj)
			{
				return (PMes)pm.Clone();

			}
		}

		internal void CleanMessage()
		{
			lock (lockObj)
			{
				pm.Caption = "";
				pm.IsBreakable = false;
				pm.Step = 0;
				pm.Len = 0;
				pm.ForceClose = false;
			}
		}

		public void Cancel()
		{
			lock (lockObj)
			{
				Breaked = true;
			}
		}

		public void AddMessage(int _step)
		{
			AddMessage(_step, null, null, false);
		}

		public void AddMessage(int _step, int _len)
		{
			AddMessage(_step, _len, null, false);
		}

		public void AddMessage(int _step, int _len, bool _isbreakable)
		{
			AddMessage(_step, _len, null, _isbreakable);
		}

		public void AddMessage(int _step, int? _len, string _caption, bool? _isbreakable)
		{
			lock (lockObj)
			{
				pm.Step = _step;
				if (_len.HasValue) pm.Len = _len.Value;
				if (_isbreakable.HasValue) pm.IsBreakable = _isbreakable.Value;
				if (_caption != null) pm.Caption = _caption;
			}
		}

		public void Dispose()
		{
			ForceClose();
		}
	}

	// Сообщение
	internal class PMes : ICloneable
	{
		public int Step;
		public int Len;
		public string Caption = "";
		public bool IsBreakable = false;
		public bool ForceClose = false;

		public object Clone()
		{
			return new PMes()
			{
				Step = this.Step,
				Len = this.Len,
				Caption = this.Caption,
				IsBreakable = this.IsBreakable,
				ForceClose = this.ForceClose
			};
		}
	}
}
