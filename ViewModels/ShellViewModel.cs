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
		private BindableCollection<ImageDetailsModel> _ImageDetails = new BindableCollection<ImageDetailsModel>();
		private ImageSource _firstImage;
		private ImageSource _secondImage;
		private PhotoCollectionModel photoModels;
		private PhotoModel selectedphotoModel;

		public string BrowsedPath
		{
			get { return _BrowsedPath; }
			set
			{
				_BrowsedPath = value;
				NotifyOfPropertyChange(() => BrowsedPath);
			}
		}

		public BindableCollection<ImageDetailsModel> ImageList
		{
			get { return _ImageDetails; }
			set
			{
				_ImageDetails = value;
				NotifyOfPropertyChange(() => ImageList);
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
			//var pvWindow = new PhotoViewerViewModel { SelectedPhoto = this.SelectedPhoto };
			var pvWindow = new PhotoViewerViewModel();
			pvWindow.SelectedPhoto = this.SelectedPhoto;
			WindowManager.ShowWindowAsync(pvWindow, null, null);
			//pvWindow.Show();
		}

		public void EditPhoto()
		{
			var pvWindow = new PhotoViewerViewModel { SelectedPhoto = this.SelectedPhoto };
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

			string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff" };
            var files = Directory.GetFiles(BrowsedPath, "*.*").Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));

			//List<ImageDetailsModel> images = new List<ImageDetailsModel>();
			int count = 0;

			foreach (var file in files)
            {
                ImageDetailsModel id = new ImageDetailsModel()
                {
                    Path = file,
                    FileName = Path.GetFileName(file),
                    Extension = Path.GetExtension(file)
                };

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
				}

				id.Width = img.PixelWidth;
                id.Height = img.PixelHeight;

                // I couldn't find file size in BitmapImage
                FileInfo fi = new FileInfo(file);
                id.Size = fi.Length;
                ImageList.Add(id);
			}
        }
	}
}
