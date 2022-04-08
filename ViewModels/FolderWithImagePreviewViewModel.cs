using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

namespace SortImagesIntoFolders.ViewModels
{
	public class FolderWithImagePreviewViewModel : Screen
	{
		private ImageSource _firstImage;

		public ImageSource FirstImage
		{
			get { return _firstImage; }
			set
			{
				_firstImage = value;
				NotifyOfPropertyChange(() => FirstImage);
			}
		}

		private ImageSource _secondImage;

		public ImageSource SecondImage
		{
			get { return _secondImage; }
			set
			{
				_secondImage = value;
				NotifyOfPropertyChange(() => SecondImage);
			}
		}

		private string _folderPath;

		public string FolderPath
		{
			get { return _folderPath; }
			set
			{
				_folderPath = value;
				SetFolderThumbnails();
			}
		}

		//public FolderWithImagePreviewViewModel(string folderPath)
		//{
		//	FolderPath = folderPath;
		//}

		private void SetFolderThumbnails()
		{
			if (string.IsNullOrEmpty(_folderPath) || string.IsNullOrWhiteSpace(_folderPath))
				return;

			DirectoryInfo di = new DirectoryInfo(_folderPath);
			int count = 0;
			string[] supportedExtensions = new[] { ".bmp", ".jpeg", ".jpg", ".png", ".tiff" };
			var files = di.EnumerateFiles().Where(s => supportedExtensions.Contains(s.Extension.ToLower()));
			foreach (var fi in files)
			{
				count++;

				if (count == 1)
				{
					BitmapImage firstImgBitmap = new BitmapImage();
					firstImgBitmap.BeginInit();
					firstImgBitmap.UriSource = new Uri(fi.FullName);
					firstImgBitmap.EndInit();
					FirstImage = firstImgBitmap;
				}

				if (count == 2)
				{
					BitmapImage secondImgBitmap = new BitmapImage();
					secondImgBitmap.BeginInit();
					secondImgBitmap.UriSource = new Uri(fi.FullName);
					secondImgBitmap.EndInit();
					SecondImage = secondImgBitmap;
					break;
				}
				//Console.WriteLine(fi.Name);
			}

			//FirstImage = new (@"C:\Users\mohammed-4770\Pictures\Camera Roll\WIN_20210812_16_56_29_Pro.jpg
		}

	}
}
