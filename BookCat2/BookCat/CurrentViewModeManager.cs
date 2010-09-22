using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookCat
{
	public enum CurrentViewModeEnum
	{
		Список, Сетка, CoverFlow
	}

	public class CurrentViewModeManager
	{
		private WindowMain _window;

		public CurrentViewModeEnum Current
		{
			get;
			private set;
		}

		public void SetMainWindow(WindowMain windowMain)
		{
			_window = windowMain;
		}

		public virtual void Go(CurrentViewModeEnum _Current)
		{
			Current = _Current;
		}
	}
}
