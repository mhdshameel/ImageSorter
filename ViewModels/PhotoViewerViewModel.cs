using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using SortImagesIntoFolders.Models;

namespace SortImagesIntoFolders.ViewModels
{
	public class PhotoViewerViewModel : PropertyChangedBase
	{
		private PhotoModel _selectedPhoto;

		public PhotoModel SelectedPhoto
        {
			get { return _selectedPhoto; }
			set 
            { 
                _selectedPhoto = value;
                NotifyOfPropertyChange(() => SelectedPhoto);
            }
		}

		public void Rotate()
        {
            BitmapSource img = SelectedPhoto.Image;

            var cache = new CachedBitmap(img, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            SelectedPhoto.Image = BitmapFrame.Create(new TransformedBitmap(cache, new RotateTransform(90.0)));
        }

        public void Crop()
        {
            BitmapSource img = SelectedPhoto.Image;

            var halfWidth = img.PixelWidth / 2;
            var halfHeight = img.PixelHeight / 2;
            SelectedPhoto.Image =
                BitmapFrame.Create(new CroppedBitmap(img,
                    new Int32Rect((halfWidth - (halfWidth / 2)), (halfHeight - (halfHeight / 2)), halfWidth, halfHeight)));
        }

        public void BlackAndWhite()
        {
            BitmapSource img = SelectedPhoto.Image;
            SelectedPhoto.Image =
                BitmapFrame.Create(new FormatConvertedBitmap(img, PixelFormats.Gray8, BitmapPalettes.Gray256, 1.0));
        }
    }
}
