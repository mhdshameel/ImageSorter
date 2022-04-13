using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Ookii.Dialogs.Wpf;
using SortImagesIntoFolders.Models;

namespace SortImagesIntoFolders.ViewModels
{
	public class ShellViewModel : Screen
	{
		private string _BrowsedPath = "";
		private ImageSource _firstImage;
		private ImageSource _secondImage;
		private PhotoCollectionModel photoModels;
		private PhotoModel selectedphotoModel;
		private Visibility _detailedListVisibility = Visibility.Collapsed;
		private Visibility _thumbnailListVisibility = Visibility.Visible;
		private bool _thumbnaillistview = true;
		private bool _detailedlistview = false;

		public string BrowsedPath
		{
			get { return _BrowsedPath; }
			set
			{
				_BrowsedPath = value;
				NotifyOfPropertyChange(() => BrowsedPath);
				PopulateFolderThumbnails();
			}
		}

		public PhotoCollectionModel Photos
		{
			get { return photoModels; }
			set { photoModels = value; }
		}

		public PhotoModel SelectedPhoto
		{
			get { return selectedphotoModel; }
			set 
			{
				selectedphotoModel = value;
				NotifyOfPropertyChange(() => SelectedPhoto);
			}
		}

		public ImageSource FirstImage
		{
			get { return _firstImage; }
			set
			{
				_firstImage = value;
				NotifyOfPropertyChange(() => FirstImage);
			}
		}

		public ImageSource SecondImage
		{
			get { return _secondImage; }
			set
			{
				_secondImage = value;
				NotifyOfPropertyChange(() => SecondImage);
			}
		}

		public bool ThumbnailListView
		{
			get { return _thumbnaillistview; }
			set 
			{ 
				_thumbnaillistview = value;
				if (_thumbnaillistview)
				{
					ThumbnailListVisibility = Visibility.Visible;
					DetailedListVisibility = Visibility.Collapsed;
				}
				NotifyOfPropertyChange();
			}
		}

		public bool DetailedListView
		{
			get { return _detailedlistview; }
			set 
			{ 
				_detailedlistview = value;
				if (_detailedlistview)
				{
					ThumbnailListVisibility = Visibility.Collapsed;
					DetailedListVisibility = Visibility.Visible;
				}
				NotifyOfPropertyChange();
			}
		}

		public Visibility DetailedListVisibility
		{
			get { return _detailedListVisibility; }
			set
			{
				_detailedListVisibility = value;
				NotifyOfPropertyChange();
			}
		}

		public Visibility ThumbnailListVisibility
		{
			get { return _thumbnailListVisibility; }
			set
			{
				_thumbnailListVisibility = value;
				NotifyOfPropertyChange();
			}
		}

		[Import]
		IWindowManager WindowManager { get; set; }

		public ShellViewModel()
		{
			Photos = new PhotoCollectionModel();
			Photos.Path = @"C:\Users\mohammed-4770\Pictures\Screenshots";

			WindowManager = new WindowManager();
		}

		public void OnPhotoClick()
		{
			var pvWindow = new PhotoViewerViewModel { SelectedPhoto = this.SelectedPhoto };
			WindowManager.ShowWindowAsync(pvWindow, null, null);
		}

		public void EditPhoto()
		{
			var pvWindow = new PhotoViewerViewModel { SelectedPhoto = this.SelectedPhoto };
			WindowManager.ShowWindowAsync(pvWindow, null, null);
			//pvWindow.Show();
		}

		public void OpenFolder()
		{
			var dialog = new VistaFolderBrowserDialog
			{
				Description = "Please select a folder.",
				UseDescriptionForTitle = true // This applies to the Vista style dialog only, not the old dialog.
			};

			if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
			{
				MessageBox.Show("Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
			}

			if ((bool)dialog.ShowDialog())
			{
				BrowsedPath = dialog.SelectedPath;
				//MessageBox.Show($"The selected folder was:{Environment.NewLine}{dialog.SelectedPath}", "Sample folder browser dialog");
			}
			else
			{
				return;
			}

			Photos.Path = BrowsedPath;
        }

		public void MoveToFolder()
		{

		}

		/*
		 * Huge issue here:
		 * Heavy operation for a basic functionality - fix
		 * See usage of EnumerateFiles instead of getfiles
		 */
		void PopulateFolderThumbnails()
		{
			string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff" };
			var files = Directory.GetFiles(BrowsedPath, "*.*").Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));

			//List<ImageDetailsModel> images = new List<ImageDetailsModel>();
			int count = 0;

			foreach (var file in files)
			{
				BitmapImage img = new BitmapImage();
				img.BeginInit();
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.UriSource = new Uri(file, UriKind.Absolute);
				img.EndInit();

				count++;

				if (count == 1)
				{
					FirstImage = img;
				}

				if (count == 2)
				{
					SecondImage = img;
					break;
				}
			}
		}
	}
}
