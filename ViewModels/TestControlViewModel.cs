using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace SortImagesIntoFolders.ViewModels
{
	public class TestControlViewModel : Screen
	{
		public void TryAction()
		{
			MessageBox.Show("An action");
		}
	}
}
