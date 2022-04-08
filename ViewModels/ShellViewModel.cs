using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Ookii.Dialogs.Wpf;
using SortImagesIntoFolders.Models;

namespace SortImagesIntoFolders.ViewModels
{
	public class ShellViewModel : Screen
	{
		private string _BrowsedPath = "";

		public string BrowsedPath
		{
			get { return _BrowsedPath; }
			set
			{
				_BrowsedPath = value;
				NotifyOfPropertyChange(() => BrowsedPath);
			}
		}

		private BindableCollection<ImageDetailsModel> _ImageDetails = new BindableCollection<ImageDetailsModel>();

		public BindableCollection<ImageDetailsModel> ImageList
		{
			get { return _ImageDetails; }
			set
			{
				_ImageDetails = value;
				NotifyOfPropertyChange(() => ImageList);
			}
		}

		public ShellViewModel()
		{
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

            string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff" };
            var files = Directory.GetFiles(BrowsedPath, "*.*").Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));

			//List<ImageDetailsModel> images = new List<ImageDetailsModel>();

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
