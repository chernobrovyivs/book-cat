using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BookCat
{
	public partial class ctlStatusProgress : UserControl
	{
		public ctlStatusProgress()
		{
			InitializeComponent();
		}

		public enum Actions
		{
			Добавление,
			Удаление
		}

		public class ReportParams
		{
			public ReportParams(string _Message, Actions _Action, int _Percent)
			{
				Message = _Message;
				Action = _Action;
				Percent = _Percent;
			}

			public string Message;
			public Actions Action;
			public int Percent;

		}

		public void DoReport(ReportParams rp)
		{
			lblStatus.Text = rp.Action + " : " + rp.Message;
			pbStatus.Value = rp.Percent;
		}
	}
}
