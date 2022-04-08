using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SortImagesIntoFolders.ViewModels;

namespace SortImagesIntoFolders.Views
{
	/// <summary>
	/// Interaction logic for FolderWithImagePreviewView.xaml
	/// </summary>
	public partial class FolderWithImagePreviewView : UserControl
	{
		private FolderWithImagePreviewViewModel vm;
		public FolderWithImagePreviewView()
		{
			InitializeComponent();
			this.vm = (FolderWithImagePreviewViewModel)this.rootGrid.DataContext;
		}

		//public string FolderPath
		//{
		//	get => this.vm.FolderPath;
		//	set => this.vm.FolderPath = value;
		//}



		public string FolderPath
		{
			get { return (string)GetValue(FolderPathProperty); }
			set
			{
				SetValue(FolderPathProperty, value);
				this.vm.FolderPath = FolderPath;
			}
		}

		// Using a DependencyProperty as the backing store for FolderPath.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FolderPathProperty =
			DependencyProperty.Register("FolderPath", typeof(string), typeof(FolderWithImagePreviewView), new PropertyMetadata(""));


	}
}
